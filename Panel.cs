namespace _3dSharp;

public class Panel
{
    public Point3d[] points = new Point3d[4];

    public Panel(Point3d p1, Point3d p2)
    {
        points[0] = p1;
        points[2] = p2;
        switch (p1.FindAxis(p2))
        {
            case 0:
                points[1] = new Point3d(p1.X, p1.Y, p2.Z);
                points[3] = new Point3d(p1.X, p2.Y, p1.Z);
                break;
            case 1:
                points[1] = new Point3d(p2.X, p1.Y, p1.Z);
                points[3] = new Point3d(p1.X, p1.Y, p2.Z);
                break;
            case 2:
                points[1] = new Point3d(p2.X, p1.Y, p1.Z);
                points[3] = new Point3d(p1.X, p2.Y, p1.Z);
                break;
            default: break;
        }
    }

}

