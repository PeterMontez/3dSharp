namespace _3dSharp;

public class Plane
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double result { get; set; }

    public Plane(double x, double y, double z, double result)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.result = result;
    }

}

