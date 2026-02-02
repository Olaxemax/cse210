using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }

    public void Display()
    {
        Console.WriteLine($"{CommenterName}: {Text}");
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthSeconds { get; set; }
    private List<Comment> comments = new List<Comment>();

    public Video(string title, string author, int lengthSeconds)
    {
        Title = title;
        Author = author;
        LengthSeconds = lengthSeconds;
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public void Display()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Length: {LengthSeconds} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");
        foreach (Comment c in comments)
        {
            c.Display();
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Create videos
        Video video1 = new Video("Intro to Abstraction", "Alice", 300);
        video1.AddComment(new Comment("Bob", "Great explanation!"));
        video1.AddComment(new Comment("Charlie", "Very clear and helpful."));
        video1.AddComment(new Comment("Dana", "Thanks for sharing!"));

        Video video2 = new Video("OOP Basics", "Eve", 450);
        video2.AddComment(new Comment("Frank", "Loved the examples."));
        video2.AddComment(new Comment("Grace", "Can you cover inheritance next?"));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2 };

        // Display each video and its comments
        foreach (Video v in videos)
        {
            v.Display();
        }
    }
}
