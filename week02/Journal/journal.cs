using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void Display()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nNo entries found.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                // ✅ Use properties instead of old fields
                outputFile.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Text}|{entry.Mood}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 4)
            {
                Entry entry = new Entry(parts[0], parts[1], parts[2], parts[3]);
                _entries.Add(entry);
            }
        }
    }
};l