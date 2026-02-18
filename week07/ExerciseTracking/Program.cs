using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Fitness Tracker ---");
            Console.WriteLine("1. Log Running");
            Console.WriteLine("2. Log Cycling");
            Console.WriteLine("3. Log Swimming");
            Console.WriteLine("4. Show All Summaries");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DateTime runDate = ReadDate("Enter date (yyyy-mm-dd): ");
                    int runMinutes = ReadInt("Enter duration (minutes): ");
                    double runDistance = ReadDouble("Enter distance (miles): ");

                    activities.Add(new Running(runDate, runMinutes, runDistance));
                    Console.WriteLine("✅ Running activity logged!");
                    break;

                case "2":
                    DateTime cycleDate = ReadDate("Enter date (yyyy-mm-dd): ");
                    int cycleMinutes = ReadInt("Enter duration (minutes): ");
                    double cycleSpeed = ReadDouble("Enter speed (mph): ");

                    activities.Add(new Cycling(cycleDate, cycleMinutes, cycleSpeed));
                    Console.WriteLine("✅ Cycling activity logged!");
                    break;

                case "3":
                    DateTime swimDate = ReadDate("Enter date (yyyy-mm-dd): ");
                    int swimMinutes = ReadInt("Enter duration (minutes): ");
                    int swimLaps = ReadInt("Enter laps: ");

                    activities.Add(new Swimming(swimDate, swimMinutes, swimLaps));
                    Console.WriteLine("✅ Swimming activity logged!");
                    break;

                case "4":
                    Console.WriteLine("\n--- Activity Summaries ---");
                    if (activities.Count == 0)
                    {
                        Console.WriteLine("No activities logged yet.");
                    }
                    else
                    {
                        foreach (var activity in activities)
                        {
                            Console.WriteLine(activity.GetSummary());
                        }
                    }
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("👋 Goodbye! Keep exercising!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    // Helper methods for validation
    static DateTime ReadDate(string prompt)
    {
        DateTime value;
        Console.Write(prompt);
        while (!DateTime.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("❌ Invalid date. Try again (yyyy-mm-dd): ");
        }
        return value;
    }

    static int ReadInt(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.Write("❌ Invalid number. Enter a positive integer: ");
        }
        return value;
    }

    static double ReadDouble(string prompt)
    {
        double value;
        Console.Write(prompt);
        while (!double.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.Write("❌ Invalid number. Enter a positive value: ");
        }
        return value;
    }
}