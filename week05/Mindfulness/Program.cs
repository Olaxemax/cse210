using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Base class
    abstract class Activity
    {
        protected string name;
        protected string description;
        protected int duration;

        public Activity(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public void StartMessage()
        {
            Console.WriteLine($"Welcome to the {name}!");
            Console.WriteLine(description);
            Console.Write("Enter duration (seconds): ");
            duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Get ready...");
            PauseAnimation(3);
        }

        public void EndMessage()
        {
            Console.WriteLine($"Great job! You completed {duration} seconds of {name}.");
            PauseAnimation(3);
        }

        protected void PauseAnimation(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
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
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            while (DateTime.Now < endTime)
            {
                Console.WriteLine("Breathe in...");
                PauseAnimation(4);
                Console.WriteLine("Breathe out...");
                PauseAnimation(4);
            }

            EndMessage();
        }
    }

    // Reflection Activity
    class ReflectionActivity : Activity
    {
        private List<string> prompts = new List<string>
        {
            "Think of a time you felt proud.",
            "Recall a moment of kindness you experienced.",
            "Remember a challenge you overcame."
        };

        private List<string> questions = new List<string>
        {
            "Why was this meaningful?",
            "What did you learn from it?",
            "How can you apply this today?"
        };

        public ReflectionActivity() 
            : base("Reflection Activity", "This activity helps you reflect on meaningful experiences.") { }

        public override void Run()
        {
            StartMessage();
            Random rand = new Random();
            string prompt = prompts[rand.Next(prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");

            DateTime endTime = DateTime.Now.AddSeconds(duration);
            while (DateTime.Now < endTime)
            {
                string question = questions[rand.Next(questions.Count)];
                Console.WriteLine(question);
                PauseAnimation(5);
            }

            EndMessage();
        }
    }

    // Listing Activity
    class ListingActivity : Activity
    {
        private List<string> topics = new List<string>
        {
            "List people who inspire you.",
            "List your personal strengths.",
            "List things you are grateful for."
        };

        public ListingActivity() 
            : base("Listing Activity", "This activity helps you list positive aspects of your life.") { }

        public override void Run()
        {
            StartMessage();
            Random rand = new Random();
            string topic = topics[rand.Next(topics.Count)];
            Console.WriteLine($"Topic: {topic}");

            DateTime endTime = DateTime.Now.AddSeconds(duration);
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
                Console.WriteLine("4. Quit");
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