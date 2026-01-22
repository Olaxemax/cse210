class Entry
{
    public string Date { get; }
    public string Prompt { get; }
    public string Text { get; }
    public string Mood { get; }

    public Entry(string date, string prompt, string text, string mood)
    {
        Date = date;
        Prompt = prompt;
        Text = text;
        Mood = mood;
    }

    public void Display()
    {
        Console.WriteLine($"{Date} - Prompt: {Prompt}");
        Console.WriteLine($"Mood: {Mood}");
        Console.WriteLine($"Response: {Text}\n");
    }
}