using System.Drawing;

namespace _3dSharp;

public class Triangle2d
{
    public Point[] points = new Point[3];

    public Triangle2d(Point p1, Point p2, Point p3)
    {
        this.points[0] = p1;
        this.points[1] = p2;
        this.points[2] = p3;
    }

}

