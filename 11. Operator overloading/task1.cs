using System;

class Square
{
    public int A { get; set; }

    public Square()
    {
        A = 0;
    }

    public Square(int a)
    {
        A = a;
    }

    public override string ToString()
    {
        return $"Square: A = {A}";
    }

    public static Square operator ++(Square s)
    {
        s.A++;
        return s;
    }

    public static Square operator --(Square s)
    {
        s.A--;
        return s;
    }

    public static Square operator +(Square s, int n)
    {
        return new Square(s.A + n);
    }

    public static Square operator -(Square s, int n)
    {
        return new Square(Math.Max(0, s.A - n));
    }

    public static Square operator *(Square s, int n)
    {
        return new Square(s.A * n);
    }

    public static Square operator /(Square s, int n)
    {
        return new Square(s.A / n);
    }

    public static bool operator >(Square s1, Square s2)
    {
        return s1.A > s2.A;
    }

    public static bool operator <(Square s1, Square s2)
    {
        return s1.A < s2.A;
    }

    public static bool operator >=(Square s1, Square s2)
    {
        return s1.A >= s2.A;
    }

    public static bool operator <=(Square s1, Square s2)
    {
        return s1.A <= s2.A;
    }

    public static bool operator ==(Square s1, Square s2)
    {
        return s1.A == s2.A;
    }

    public static bool operator !=(Square s1, Square s2)
    {
        return s1.A != s2.A;
    }

    public override bool Equals(object obj)
    {
        Square s = obj as Square;

        if (s == null)
            return false;

        return A == s.A;
    }

    public override int GetHashCode()
    {
        return A.GetHashCode();
    }

    public static bool operator true(Square s)
    {
        return s.A != 0;
    }

    public static bool operator false(Square s)
    {
        return s.A == 0;
    }

    public static implicit operator Rectangle(Square s)
    {
        return new Rectangle(s.A, s.A);
    }

    public static implicit operator int(Square s)
    {
        return s.A;
    }
}

class Rectangle
{
    public int A { get; set; }
    public int B { get; set; }

    public Rectangle()
    {
        A = 0;
        B = 0;
    }

    public Rectangle(int a, int b)
    {
        A = a;
        B = b;
    }

    public override string ToString()
    {
        return $"Rectangle: A = {A}, B = {B}";
    }

    public static Rectangle operator ++(Rectangle r)
    {
        r.A++;
        r.B++;
        return r;
    }

    public static Rectangle operator --(Rectangle r)
    {
        r.A--;
        r.B--;
        return r;
    }

    public static Rectangle operator +(Rectangle r, int n)
    {
        return new Rectangle(r.A + n, r.B + n);
    }

    public static Rectangle operator -(Rectangle r, int n)
    {
        return new Rectangle(
            Math.Max(0, r.A - n),
            Math.Max(0, r.B - n));
    }

    public static Rectangle operator *(Rectangle r, int n)
    {
        return new Rectangle(r.A * n, r.B * n);
    }

    public static Rectangle operator /(Rectangle r, int n)
    {
        return new Rectangle(r.A / n, r.B / n);
    }

    public static bool operator >(Rectangle r1, Rectangle r2)
    {
        return r1.A > r2.A && r1.B > r2.B;
    }

    public static bool operator <(Rectangle r1, Rectangle r2)
    {
        return r1.A < r2.A && r1.B < r2.B;
    }

    public static bool operator >=(Rectangle r1, Rectangle r2)
    {
        return r1.A >= r2.A && r1.B >= r2.B;
    }

    public static bool operator <=(Rectangle r1, Rectangle r2)
    {
        return r1.A <= r2.A && r1.B <= r2.B;
    }

    public static bool operator ==(Rectangle r1, Rectangle r2)
    {
        return r1.A == r2.A && r1.B == r2.B;
    }

    public static bool operator !=(Rectangle r1, Rectangle r2)
    {
        return !(r1 == r2);
    }

    public override bool Equals(object obj)
    {
        Rectangle r = obj as Rectangle;

        if (r == null)
            return false;

        return A == r.A && B == r.B;
    }

    public override int GetHashCode()
    {
        return A.GetHashCode() ^ B.GetHashCode();
    }

    public static bool operator true(Rectangle r)
    {
        return r.A != 0 && r.B != 0;
    }

    public static bool operator false(Rectangle r)
    {
        return r.A == 0 || r.B == 0;
    }

    public static explicit operator Square(Rectangle r)
    {
        return new Square(Math.Min(r.A, r.B));
    }

    public static explicit operator int(Rectangle r)
    {
        return r.A * r.B;
    }
}

class Program
{
    static void Main()
    {
        Square s1 = new Square(5);
        Square s2 = new Square(7);

        Console.WriteLine(s1);
        Console.WriteLine(s2);

        s1++;

        Console.WriteLine(s1);
        Console.WriteLine(s1 < s2);

        Rectangle r1 = new Rectangle(4, 6);

        Console.WriteLine(r1);

        r1 = r1 + 2;

        Console.WriteLine(r1);

        Rectangle r2 = s1;

        Console.WriteLine(r2);

        Square s3 = (Square)r1;

        Console.WriteLine(s3);

        int x = s1;

        Console.WriteLine(x);

        int area = (int)r1;

        Console.WriteLine(area);

        if (s1)
            Console.WriteLine("Square істинний");

        if (r1)
            Console.WriteLine("Rectangle істинний");
    }
}