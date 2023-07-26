using System.Drawing;
using System.Runtime.CompilerServices;

namespace _3dSharp;

public class BruteRenderer
{
    public List<Triangle> Triangles { get; set; } = new List<Triangle>();

    public List<Triangle2d> Rendered { get; set; } = new List<Triangle2d>();

    public BruteRenderer(List<Triangle> triangles)
    {
        this.Triangles = triangles;
    }

    public List<Triangle2d> Render(Camera camera)
    {
        Point3d position = camera.Position;
        Plane plane = camera.CameraView.Plane;
        Point3d[] points = camera.CameraView.Points;
        double maxDist = camera.CameraView.RatioScale * camera.CameraView.Ratio.width;
        double ratioScale = camera.CameraView.RatioScale;
        Ratio ratio = camera.CameraView.Ratio;

        foreach (Triangle triangle in Triangles)
        {

            Point[] RightRender()
            {
                Point[] fixedPoints = new Point[3];

                Point3d intersec = LinePlaneIntersec(new Line(triangle.points[0], position), plane);
                fixedPoints[0] = PointFixer(0, intersec, points, ratio, ratioScale);

                intersec = LinePlaneIntersec(new Line(triangle.points[1], position), plane);
                fixedPoints[1] = PointFixer(0, intersec, points, ratio, ratioScale);

                intersec = LinePlaneIntersec(new Line(triangle.points[2], position), plane);
                fixedPoints[2] = PointFixer(0, intersec, points, ratio, ratioScale);

                return fixedPoints;
            }

            Point[] fixedPoints = RightRender();

            Triangle2d result = new Triangle2d(fixedPoints[0], fixedPoints[1], fixedPoints[2]);


            if (ValidateTriangle(result, ratio))
            {
                Rendered.Add(result);
            }
            
        }

        return this.Rendered;
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

    public Point PointFixer(byte side, Point3d point3d, Point3d[] points, Ratio ratio, double ratioScale)
    {
        Point RightSide()
        {
            double dist1 = (PointDist(point3d, points[1]));
            double dist2 = (PointDist(point3d, points[2]));
            double dist3 = (PointDist(point3d, points[3]));

            double rHeight = (PointDist(points[2], points[3]));
            double rWidth = (PointDist(points[1], points[2]));

            double p23Cos = ((dist3*dist3) - (dist2*dist2) - (rHeight*rHeight)) / (-2 * dist2 * rHeight);

            double p12cos = ((dist2*dist2) - (rWidth*rWidth) - (dist1*dist1)) / (-2 * rWidth * dist1);

            double Y = p23Cos * dist2;

            double Y2 = Math.Sin(Math.Acos(p12cos)) * dist1;

            double X = Math.Sin(Math.Acos(p23Cos)) * dist2;

            double X2 = p12cos * dist1;

            X = (X2*0.98 > rWidth - X) ? rWidth + X : rWidth - X;

            return new Point((int)X*ratio.scrHeight/ratio.height, (int)Y*ratio.scrHeight/ratio.height);
        }

        Point LeftSide(Point3d point3d, Point3d[] points, Ratio ratio, double ratioScale)
        {
            double dist1 = (PointDist(point3d, points[1]));
            double dist2 = (PointDist(point3d, points[2]));
            double dist3 = (PointDist(point3d, points[3]));

            double rHeight = (PointDist(points[2], points[3]));
            double rWidth = (PointDist(points[1], points[2]));

            double p23Cos = ((dist3*dist3) - (dist2*dist2) - (rHeight*rHeight)) / (-2 * dist2 * rHeight);

            double p12cos = ((dist2*dist2) - (rWidth*rWidth) - (dist1*dist1)) / (-2 * rWidth * dist1);

            double Y = p23Cos * dist2;

            double Y2 = Math.Sin(Math.Acos(p12cos)) * dist1;

            double X = Math.Sin(Math.Acos(p23Cos)) * dist2;

            double X2 = p12cos * dist1;

            X = (X2*0.98 > rWidth - X) ? rWidth + X : rWidth - X;

            return new Point((int)X*ratio.scrHeight/ratio.height, (int)Y*ratio.scrHeight/ratio.height);
        }

        return RightSide();

    }

    public bool ValidateTriangle(Triangle2d triangle, Ratio ratio)
    {
        double GetDist(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Abs((p1.X-p2.X)*(p1.X-p2.X) + (p1.Y-p2.Y)*(p1.Y-p2.Y)));
        }

        for (int i = 0; i < 3; i++)
        {
            if (GetDist(triangle.points[i], triangle.points[i+1 > 2 ? 0 : i+1]) > ratio.scrHeight * 1.5)
            {
                return false;
            }
        }

        return true;
    }

}