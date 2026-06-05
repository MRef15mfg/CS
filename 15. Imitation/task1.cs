using System;

abstract class GeometricFigure
{
    public abstract double GetArea();
    public abstract double GetPerimeter();
}

class Triangle : GeometricFigure
{
    private double a;
    private double b;
    private double c;

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public override double GetArea()
    {
        double p = GetPerimeter() / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public override double GetPerimeter()
    {
        return a + b + c;
    }
}

class Square : GeometricFigure
{
    private double side;

    public Square(double side)
    {
        this.side = side;
    }

    public override double GetArea()
    {
        return side * side;
    }

    public override double GetPerimeter()
    {
        return 4 * side;
    }
}

class Rhombus : GeometricFigure
{
    private double side;
    private double height;

    public Rhombus(double side, double height)
    {
        this.side = side;
        this.height = height;
    }

    public override double GetArea()
    {
        return side * height;
    }

    public override double GetPerimeter()
    {
        return 4 * side;
    }
}

class Rectangle : GeometricFigure
{
    private double width;
    private double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public override double GetArea()
    {
        return width * height;
    }

    public override double GetPerimeter()
    {
        return 2 * (width + height);
    }
}

class Parallelogram : GeometricFigure
{
    private double a;
    private double b;
    private double height;

    public Parallelogram(double a, double b, double height)
    {
        this.a = a;
        this.b = b;
        this.height = height;
    }

    public override double GetArea()
    {
        return a * height;
    }

    public override double GetPerimeter()
    {
        return 2 * (a + b);
    }
}

class Trapezoid : GeometricFigure
{
    private double a;
    private double b;
    private double c;
    private double d;
    private double height;

    public Trapezoid(double a, double b, double c, double d, double height)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
        this.height = height;
    }

    public override double GetArea()
    {
        return (a + b) * height / 2;
    }

    public override double GetPerimeter()
    {
        return a + b + c + d;
    }
}

class Circle : GeometricFigure
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * radius * radius;
    }

    public override double GetPerimeter()
    {
        return 2 * Math.PI * radius;
    }
}

class Ellipse : GeometricFigure
{
    private double a;
    private double b;

    public Ellipse(double a, double b)
    {
        this.a = a;
        this.b = b;
    }

    public override double GetArea()
    {
        return Math.PI * a * b;
    }

    public override double GetPerimeter()
    {
        return Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b)));
    }
}

class CompositeFigure : GeometricFigure
{
    private GeometricFigure[] figures;

    public CompositeFigure(params GeometricFigure[] figures)
    {
        this.figures = figures;
    }

    public override double GetArea()
    {
        double area = 0;

        foreach (GeometricFigure figure in figures)
        {
            area += figure.GetArea();
        }

        return area;
    }

    public override double GetPerimeter()
    {
        double perimeter = 0;

        foreach (GeometricFigure figure in figures)
        {
            perimeter += figure.GetPerimeter();
        }

        return perimeter;
    }
}

class Program
{
    static void Main()
    {
        GeometricFigure square = new Square(5);
        GeometricFigure circle = new Circle(3);
        GeometricFigure rectangle = new Rectangle(4, 6);

        CompositeFigure composite = new CompositeFigure(
            square,
            circle,
            rectangle
        );

        Console.WriteLine("Площа квадрата: " + square.GetArea());
        Console.WriteLine("Периметр квадрата: " + square.GetPerimeter());

        Console.WriteLine();

        Console.WriteLine("Площа складеної фігури: " + composite.GetArea());
        Console.WriteLine("Периметр складеної фігури: " + composite.GetPerimeter());
    }
}