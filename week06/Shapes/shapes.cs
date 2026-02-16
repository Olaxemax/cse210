public class Shape
{
    private string color;

    public Shape(string color)
    {
        this.color = color;
    }

    public string GetColor()
    {
        return color;
    }

    // Virtual method to be overridden
    public virtual double GetArea()
    {
        return 0;
    }
}