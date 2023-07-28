using System.Drawing;

namespace _3dSharp;

public class FloorTriangle
{
    public Point3d[] points = new Point3d[3];

    public FloorTriangle(Point3d p1, Point3d p2, Point3d p3)
    {
        this.points[0] = p1;
        this.points[1] = p2;
        this.points[2] = p3;
        Scene.AddFloor(this);
    }

}

