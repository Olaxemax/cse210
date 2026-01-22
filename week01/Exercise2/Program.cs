using System;

class Program
{
    static void Main()
    {
       
        Console.Write("Enter your grade percentage: ");
        int gradePercentage = int.Parse(Console.ReadLine());


        string letter;
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        
        string sign = "";
        int lastDigit = gradePercentage % 10;

        if (gradePercentage == 100)
        {
            // Special case: 100% is A+
            letter = "A";
            sign = "+";
        }
        else if (letter != "A" && letter != "F") // No A+ or F+/F-
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }
        else if (letter == "A" && lastDigit < 3)
        {
            sign = "-"; // A- is valid
        }
       
        Console.WriteLine($"Your grade is: {letter}{sign}");

       
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Better luck next time. Keep working hard!");
        }
    }
}