using System.Drawing;
using System.Runtime.CompilerServices;

namespace _3dSharp;

public class BruteRenderer
{
    public List<Triangle> triangles { get; set; } = new List<Triangle>();

    public List<Triangle2d> rendered { get; set; } = new List<Triangle2d>();

    public BruteRenderer(List<Triangle> triangles)
    {
        this.triangles = triangles;
    }

    public List<Triangle2d> Render(Camera camera)
    {
        Point3d position = camera.position;
        Plane plane = camera.cameraView.plane;
        Point3d[] points = camera.cameraView.points;
        double maxDist = camera.cameraView.ratioScale * camera.cameraView.ratio.width;
        double ratioScale = camera.cameraView.ratioScale;
        Ratio ratio = camera.cameraView.ratio;

        foreach (Triangle triangle in triangles)
        {
            Point[] fixedPoints = new Point[3];

            Point3d intersec = LinePlaneIntersec(new Line(triangle.points[0], position), plane);
            fixedPoints[0] = PointFixer(intersec, points, ratio, ratioScale);

            intersec = LinePlaneIntersec(new Line(triangle.points[1], position), plane);
            fixedPoints[1] = PointFixer(intersec, points, ratio, ratioScale);

            intersec = LinePlaneIntersec(new Line(triangle.points[2], position), plane);
            fixedPoints[2] = PointFixer(intersec, points, ratio, ratioScale);

            Triangle2d result = new Triangle2d(fixedPoints[0], fixedPoints[1], fixedPoints[2]);

            rendered.Add(result);
        }

        return new List<Triangle2d>();
    }

    public Point3d LinePlaneIntersec(Line line, Plane plane)
    {
        double n = plane.result - (plane.X * line.X[0] + plane.Y * line.Y[0] + plane.Z * line.Z[0]);
        double t = n / (plane.X * line.X[1] + plane.Y * line.Y[1] + plane.Z * line.Z[1]);

        double X = line.X[0] + line.X[1] * t;
        double Y = line.Y[0] + line.Y[1] * t;
        double Z = line.Z[0] + line.Z[1] * t;

        return new Point3d(X, Y, Z);
    }

    public double PointDist(Point3d p1, Point3d p2)
    {
        return Math.Sqrt((p1.X-p2.X)*(p1.X-p2.X) + (p1.Y-p2.Y)*(p1.Y-p2.Y) + (p1.Z-p2.Z)*(p1.Z-p2.Z));
    }

    public Point PointFixer(Point3d point3d, Point3d[] points, Ratio ratio, double ratioScale)
    {
        Point point = new Point();

        double dist2 = ratio.scrWidth/ratio.width * (PointDist(point3d, points[2]));
        double dist3 = ratio.scrWidth/ratio.width * (PointDist(point3d, points[3])); 

        double p2Cos = ((ratio.scrHeight * ratio.scrHeight) + (dist2 * dist2) + (dist3 * dist3)) / 2 * ratio.scrHeight * dist2;

        double X = ratio.scrWidth - (dist2 * p2Cos);

        double Y = ratio.scrHeight - (Math.Sin(Math.Acos(p2Cos)) * dist2);

        return new Point((int)X, (int)Y);

    }
}