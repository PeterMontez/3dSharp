using System.Drawing;

namespace _3dSharp;

public class CameraView
{
    public Point3d[] points { get; set; }= new Point3d[4];

    public Point3d position { get; set; }

    public Point3d FOVpoint { get; set; } = new Point3d(0, 0, 0);

    public Ratio ratio { get; set; }

    public Plane plane { get; set; }

    public CameraView(Point3d position, double FOV, Angle angle, Ratio ratio)
    {
        this.ratio = ratio;
        this.FOVpoint = GetCenter(position, FOV, angle);
        // this.points = GetViewScreen(this.FOVpoint, angle);
        this.position = position;
        this.plane = PlaneFinder(this.FOVpoint, this.position);
    }

    public Point3d GetCenter(Point3d position, double FOV, Angle angle)
    {
        double X = FOV * Math.Cos(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));
        double Y = FOV * Math.Sin(AMath.DegToRad(angle.pitch));
        double Z = FOV * Math.Sin(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));

        // X = AMath.Round(X, 2);
        // Y = AMath.Round(Y, 2);
        // Z = AMath.Round(Z, 2);

        return new Point3d(X, Y, Z);
    }

    // public Point3d[] GetViewScreen(Point3d center, Angle angle)
    // {

    // }

    public Plane PlaneFinder(Point3d center, Point3d position)
    {
        double[] directionVector = new double[3];
        double result;

        directionVector[0] = position.X - center.X;
        directionVector[1] = position.Y - center.Y;
        directionVector[2] = position.Z - center.Z;

        result = ((directionVector[0] * (-center.X)) + (directionVector[1] * (-center.Y)) + (directionVector[2] * (-center.Z)));

        return new Plane(-directionVector[0], -directionVector[1], -directionVector[2], result);
    }

}

