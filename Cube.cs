using System.Drawing;

namespace _3dSharp;

public class Cube
{
    public Panel[] panels = new Panel[6];

    public Cube(Point3d p1, Point3d p2)
    {
        panels[0] = new Panel(p1, new Point3d(p2.X, p2.Y, p1.Z));
        panels[1] = new Panel(new Point3d(p2.X, p1.Y, p1.Z), p2);
        panels[2] = new Panel(p2, new Point3d(p1.X, p1.Y, p2.Z));
        panels[3] = new Panel(p1, new Point3d(p1.X, p2.Y, p2.Z));
        panels[4] = new Panel(new Point3d(p1.X, p2.Y, p1.Z), p2);
        panels[5] = new Panel(p1, new Point3d(p2.X, p1.Y, p2.Z));
    }

}

