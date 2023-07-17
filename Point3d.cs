using System.Drawing;

namespace _3dSharp;

public class Point3d
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Point3d(double X, double Y, double Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }

    public byte FindAxis(Point3d point)
    {
        if (this.X == point.X)
            return 0;
        else if (this.Y == point.Y)
            return 1;
        else if (this.Z == point.Z)
            return 2;
        
        return 4;
    }   

}

