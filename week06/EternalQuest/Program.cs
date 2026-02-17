using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string name;
    protected string description;
    protected int points;
    protected bool isComplete;

    public Goal(string name, string description, int points)
    {
        this.name = name;
        this.description = description;
        this.points = points;
        this.isComplete = false;
    }

    public abstract int RecordEvent(); // returns points earned

    public virtual string GetStatus()
    {
        string checkbox = isComplete ? "[X]" : "[ ]";
        return $"{checkbox} {name} ({description})";
    }

    public abstract string GetStringRepresentation();

    public static Goal FromString(string data)
    {
        string[] parts = data.Split('|');
        string type = parts[0];

        if (type == "SimpleGoal")
        {
            return new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4]));
        }
        else if (type == "EternalGoal")
        {
            return new EternalGoal(parts[1], parts[2], int.Parse(parts[3]));
        }
        else if (type == "ChecklistGoal")
        {
            return new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]),
                                     int.Parse(parts[4]), int.Parse(parts[5]),
                                     int.Parse(parts[6]), bool.Parse(parts[7]));
        }
        return null;
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points, bool complete = false)
        : base(name, description, points)
    {
        this.isComplete = complete;
    }

    public override int RecordEvent()
    {
        if (!isComplete)
        {
            isComplete = true;
            return points;
        }
        return 0;
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{name}|{description}|{points}|{isComplete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        return points; // never completes
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{name}|{description}|{points}";
    }
}

class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints, int currentCount = 0, bool complete = false)
        : base(name, description, points)
    {
        this.targetCount = targetCount;
        this.bonusPoints = bonusPoints;
        this.currentCount = currentCount;
        this.isComplete = complete;
    }

    public override int RecordEvent()
    {
        currentCount++;
        if (currentCount >= targetCount)
        {
            isComplete = true;
            return points + bonusPoints;
        }
        return points;
    }

    public override string GetStatus()
    {
        string checkbox = isComplete ? "[X]" : "[ ]";
        return $"{checkbox} {name} ({description}) -- Completed {currentCount}/{targetCount}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{name}|{description}|{points}|{targetCount}|{bonusPoints}|{currentCount}|{isComplete}";
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalScore = 0;

    static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n--- Eternal Quest Menu ---");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordEvent(); break;
                case "4": Console.WriteLine($"Total Score: {totalScore}"); break;
                case "5": SaveGoals(); break;
                case "6": LoadGoals(); break;
                case "7": running = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Select goal type: 1=Simple, 2=Eternal, 3=Checklist");
        string type = Console.ReadLine();

        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
        {
            goals.Add(new SimpleGoal(name, desc, points));
        }
        else if (type == "2")
        {
            goals.Add(new EternalGoal(name, desc, points));
        }
        else if (type == "3")
        {
            Console.Write("Enter target count: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Enter bonus points: ");
            int bonus = int.Parse(Console.ReadLine());
            goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    static void ListGoals()
    {
        Console.WriteLine("\n--- Goals ---");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
    }

    static void RecordEvent()
    {
        ListGoals();
        Console.Write("Select goal number to record: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < goals.Count)
        {
            int earned = goals[index].RecordEvent();
            totalScore += earned;
            Console.WriteLine($"You earned {earned} points!");
        }
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(totalScore);
            foreach (var goal in goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals saved.");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            string[] lines = File.ReadAllLines("goals.txt");
            totalScore = int.Parse(lines[0]);
            goals.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                goals.Add(Goal.FromString(lines[i]));
            }
            Console.WriteLine("Goals loaded.");
        }
        else
        {
            Console.WriteLine("No save file found.");
        }
    }
}