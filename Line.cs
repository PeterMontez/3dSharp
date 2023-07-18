namespace _3dSharp;

public class Line
{
    public double[] X { get; set; } = new double[2];
    public double[] Y { get; set; } = new double[2];
    public double[] Z { get; set; } = new double[2];

    public Line(Point3d p1, Point3d p2)
    {
        SetLine(p1, p2);
    }

    public void SetLine(Point3d p1, Point3d p2)
    {
        X[0] = p1.X;
        Y[1] = p1.Y;
        Z[0] = p1.Z;

        X[1] = p2.X - p1.X;
        Y[1] = p2.Y - p1.Y;
        Z[1] = p2.Z - p1.Z;
    }

}

