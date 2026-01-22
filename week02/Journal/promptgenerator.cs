using System;
using System.Collections.Generic;

class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "What was the best part of your day?",
        "What was the strongest emotion you felt today?",
        "What was the most significant thing that happened today?",
        "What did you learn today?",
        "Who did you interact with today and how did it go?"
    };

    public string GetRandomPrompt()
    {
        Random rand = new Random();
        int index = rand.Next(_prompts.Count);
        return _prompts[index];
    }
}