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
                    Console.Write("Enter date (yyyy-mm-dd): ");
                    DateTime runDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter duration (minutes): ");
                    int runMinutes = int.Parse(Console.ReadLine());
                    Console.Write("Enter distance (miles): ");
                    double runDistance = double.Parse(Console.ReadLine());

                    activities.Add(new Running(runDate, runMinutes, runDistance));
                    Console.WriteLine("✅ Running activity logged!");
                    break;

                case "2":
                    Console.Write("Enter date (yyyy-mm-dd): ");
                    DateTime cycleDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter duration (minutes): ");
                    int cycleMinutes = int.Parse(Console.ReadLine());
                    Console.Write("Enter speed (mph): ");
                    double cycleSpeed = double.Parse(Console.ReadLine());

                    activities.Add(new Cycling(cycleDate, cycleMinutes, cycleSpeed));
                    Console.WriteLine("✅ Cycling activity logged!");
                    break;

                case "3":
                    Console.Write("Enter date (yyyy-mm-dd): ");
                    DateTime swimDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter duration (minutes): ");
                    int swimMinutes = int.Parse(Console.ReadLine());
                    Console.Write("Enter laps: ");
                    int swimLaps = int.Parse(Console.ReadLine());

                    activities.Add(new Swimming(swimDate, swimMinutes, swimLaps));
                    Console.WriteLine("✅ Swimming activity logged!");
                    break;

                case "4":
                    Console.WriteLine("\n--- Activity Summaries ---");
                    foreach (var activity in activities)
                    {
                        Console.WriteLine(activity.GetSummary());
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
}