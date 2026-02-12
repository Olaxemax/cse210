using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace MindfulnessApp
{
    // Logger class with statistics
    static class Logger
    {
        private static string _logFile = "activity_log.txt";

        public static void Log(string activityName, int duration)
        {
            string entry = $"{DateTime.Now}: Completed {activityName} for {duration} seconds.";
            File.AppendAllText(_logFile, entry + Environment.NewLine);
        }

        public static void ShowLogs()
        {
            if (File.Exists(_logFile))
            {
                Console.WriteLine("\n--- Activity History ---");
                string[] logs = File.ReadAllLines(_logFile);
                foreach (string log in logs)
                {
                    Console.WriteLine(log);
                }
                Console.WriteLine("------------------------\n");

                ShowStatistics(logs);
            }
            else
            {
                Console.WriteLine("\nNo activity history found.\n");
            }
        }

        private static void ShowStatistics(string[] logs)
        {
            Console.WriteLine("📊 Statistics Summary:");

            int totalTime = 0;
            Dictionary<string, int> activityCounts = new Dictionary<string, int>();
            Dictionary<string, int> activityDurations = new Dictionary<string, int>();

            foreach (string log in logs)
            {
                // Example log: "2026-02-12 04:00:10: Completed Breathing Activity for 30 seconds."
                string[] parts = log.Split(' ');
                if (parts.Length >= 8)
                {
                    string activityName = parts[3] + " " + parts[4]; // e.g., "Breathing Activity"
                    int duration = int.Parse(parts[6]); // duration in seconds

                    totalTime += duration;

                    if (!activityCounts.ContainsKey(activityName))
                        activityCounts[activityName] = 0;
                    activityCounts[activityName]++;

                    if (!activityDurations.ContainsKey(activityName))
                        activityDurations[activityName] = 0;
                    activityDurations[activityName] += duration;
                }
            }

            Console.WriteLine($"Total time spent: {totalTime} seconds");

            if (activityCounts.Count > 0)
            {
                string mostFrequent = activityCounts.OrderByDescending(x => x.Value).First().Key;
                Console.WriteLine($"Most frequent activity: {mostFrequent}");

                foreach (var kvp in activityDurations)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value} seconds total");
                }
            }

            Console.WriteLine("------------------------\n");
        }
    }

    // Base class
    abstract class Activity
    {
        protected string _name;
        protected string _description;
        protected int _duration;

        public Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void StartMessage()
        {
            Console.WriteLine($"\nWelcome to the {_name}!");
            Console.WriteLine(_description);
            Console.Write("Enter duration (seconds): ");
            _duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Get ready...");
            SpinnerAnimation(3);
        }

        public void EndMessage()
        {
            Console.WriteLine($"Great job! You completed {_duration} seconds of {_name}.");
            Logger.Log(_name, _duration);
            SpinnerAnimation(3);
        }

        // Spinner animation using backspaces
        protected void SpinnerAnimation(int seconds)
        {
            char[] spinner = { '|', '/', '-', '\\' };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);

            int i = 0;
            while (DateTime.Now < endTime)
            {
                Console.Write(spinner[i]);
                Thread.Sleep(200);
                Console.Write("\b"); // backspace to overwrite
                i = (i + 1) % spinner.Length;
            }
            Console.WriteLine();
        }

        public abstract void Run();
    }

    // Breathing Activity
    class BreathingActivity : Activity
    {
        public BreathingActivity() 
            : base("Breathing Activity", "This activity helps you relax by guiding your breathing.") { }

        public override void Run()
        {
            StartMessage();
            DateTime endTime = DateTime.Now.AddSeconds(_duration);

            while (DateTime.Now < endTime)
            {
                Console.WriteLine("Breathe in...");
                SpinnerAnimation(4);
                Console.WriteLine("Breathe out...");
                SpinnerAnimation(4);
            }

            EndMessage();
        }
    }

    // Reflection Activity
    class ReflectionActivity : Activity
    {
        private Queue<string> _prompts = new Queue<string>(new[]
        {
            "Think of a time you felt proud.",
            "Recall a moment of kindness you experienced.",
            "Remember a challenge you overcame."
        });

        private Queue<string> _questions = new Queue<string>(new[]
        {
            "Why was this meaningful?",
            "What did you learn from it?",
            "How can you apply this today?"
        });

        public ReflectionActivity() 
            : base("Reflection Activity", "This activity helps you reflect on meaningful experiences.") { }

        private string GetNextPrompt()
        {
            if (_prompts.Count == 0)
            {
                _prompts = new Queue<string>(new[]
                {
                    "Think of a time you felt proud.",
                    "Recall a moment of kindness you experienced.",
                    "Remember a challenge you overcame."
                });
            }
            return _prompts.Dequeue();
        }

        private string GetNextQuestion()
        {
            if (_questions.Count == 0)
            {
                _questions = new Queue<string>(new[]
                {
                    "Why was this meaningful?",
                    "What did you learn from it?",
                    "How can you apply this today?"
                });
            }
            return _questions.Dequeue();
        }

        public override void Run()
        {
            StartMessage();
            Console.WriteLine($"Prompt: {GetNextPrompt()}");

            DateTime endTime = DateTime.Now.AddSeconds(_duration);
            while (DateTime.Now < endTime)
            {
                Console.WriteLine(GetNextQuestion());
                SpinnerAnimation(5);
            }

            EndMessage();
        }
    }

    // Listing Activity
    class ListingActivity : Activity
    {
        private Queue<string> _topics = new Queue<string>(new[]
        {
            "List people who inspire you.",
            "List your personal strengths.",
            "List things you are grateful for."
        });

        public ListingActivity() 
            : base("Listing Activity", "This activity helps you list positive aspects of your life.") { }

        private string GetNextTopic()
        {
            if (_topics.Count == 0)
            {
                _topics = new Queue<string>(new[]
                {
                    "List people who inspire you.",
                    "List your personal strengths.",
                    "List things you are grateful for."
                });
            }
            return _topics.Dequeue();
        }

        public override void Run()
        {
            StartMessage();
            Console.WriteLine($"Topic: {GetNextTopic()}");

            DateTime endTime = DateTime.Now.AddSeconds(_duration);
            while (DateTime.Now < endTime)
            {
                Console.Write("Enter an item: ");
                string item = Console.ReadLine();
                Console.WriteLine($"You listed: {item}");
            }

            EndMessage();
        }
    }

    // Program entry point
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nMindfulness App Menu:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. View Activity History");
                Console.WriteLine("5. Quit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        Logger.ShowLogs();
                        continue;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        continue;
                }

                activity.Run();
            }
        }
    }
}