using System;

class Program
{
    static void Main(string[] args)
    {
        Journal theJournal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        bool running = true;
        while (running)
        {
            Console.WriteLine("Please choose one of the following options:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = promptGenerator.GetRandomPrompt();
                    Console.WriteLine(prompt);
                    string response = Console.ReadLine();

                    Console.WriteLine("How are you feeling today? (e.g., Happy, Sad, Excited, Stressed)");
                    string mood = Console.ReadLine();

                    Entry newEntry = new Entry(DateTime.Now.ToString(), prompt, response, mood);
                    theJournal.AddEntry(newEntry);
                    break;

                case "2":
                    theJournal.Display();
                    break;

                case "3":
                    Console.Write("Enter filename: ");
                    string saveFile = Console.ReadLine();
                    theJournal.SaveToFile(saveFile);
                    break;

                case "4":
                    Console.Write("Enter filename: ");
                    string loadFile = Console.ReadLine();
                    theJournal.LoadFromFile(loadFile);
                    break;

                case "5":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}