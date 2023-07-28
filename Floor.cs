namespace _3dSharp;

public class Floor
{
    public FloorTriangle[] triangles = new FloorTriangle[2];

    public Floor(Point3d p1, Point3d p2)
    {
        switch (p1.FindAxis(p2))
        {
            case 0:
                triangles[0] = new FloorTriangle(p1, p2, new Point3d(p1.X, p2.Y, p1.Z));
                triangles[1] = new FloorTriangle(p1, p2, new Point3d(p1.X, p1.Y, p2.Z));
                break;
            case 1:
                triangles[0] = new FloorTriangle(p1, p2, new Point3d(p1.X, p1.Y, p2.Z));
                triangles[1] = new FloorTriangle(p1, p2, new Point3d(p2.X, p1.Y, p1.Z));
                break;
            case 2:
                triangles[0] = new FloorTriangle(p1, p2, new Point3d(p1.X, p2.Y, p1.Z));
                triangles[1] = new FloorTriangle(p1, p2, new Point3d(p2.X, p1.Y, p1.Z));
                break;
            default: break;
        }
    }

}