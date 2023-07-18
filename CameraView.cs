using System.Drawing;

namespace _3dSharp;

public class CameraView
{
    public Point3d[] points { get; set; }= new Point3d[4];

    public Point3d position { get; set; }

    public Point3d FOVpoint { get; set; } = new Point3d(0, 0, 0);

    public Ratio ratio { get; set; }

    public Plane plane { get; set; }

    public Angle angle { get; set; }

    public Angle screenAngle { get; set; } = new Angle(0, 0, 0);

    public double FOV { get; set; }

    public double ratioScale { get; set; }

    public double screenDist { get; set; }

    public CameraView(Point3d position, double FOV, Angle angle, Ratio ratio, double ratioScale)
    {
        this.ratio = ratio;
        this.FOV = FOV;
        this.angle = angle;
        this.ratioScale = ratioScale;
        this.FOVpoint = GetCenter(position, FOV, angle);
        this.position = position;
        this.plane = PlaneFinder(this.FOVpoint, this.position);
        (this.screenAngle, this.screenDist) = GetScreenAngles(this.ratio, this.FOV, this.angle, this.ratioScale);
        this.points = GetViewScreen(this.FOVpoint, angle, this.screenAngle);
    }

    public Point3d GetCenter(Point3d position, double FOV, Angle angle)
    {
        double X = FOV * Math.Cos(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));
        double Y = FOV * Math.Sin(AMath.DegToRad(angle.pitch));
        double Z = FOV * Math.Sin(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));

        return new Point3d(X, Y, Z);
    }

    public Point3d[] GetViewScreen(Point3d center, Angle angle, Angle screenAngle)
    {
        Point3d[] points = new Point3d[4];

        Angle crrAngle = new Angle(angle.yaw - screenAngle.yaw, angle.pitch - screenAngle.pitch, angle.roll);
        points[0] = GetPoint(this.position, this.screenDist, crrAngle);

        crrAngle = new Angle(angle.yaw - screenAngle.yaw, angle.pitch + screenAngle.pitch, angle.roll);
        points[1] = GetPoint(this.position, this.screenDist, crrAngle);

        crrAngle = new Angle(angle.yaw + screenAngle.yaw, angle.pitch + screenAngle.pitch, angle.roll);
        points[2] = GetPoint(this.position, this.screenDist, crrAngle);

        crrAngle = new Angle(angle.yaw + screenAngle.yaw, angle.pitch - screenAngle.pitch, angle.roll);
        points[3] = GetPoint(this.position, this.screenDist, crrAngle);

        return points;
    }

    public Point3d GetPoint(Point3d position, double FOV, Angle angle)
    {
        double X = FOV * Math.Cos(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));
        double Y = FOV * Math.Sin(AMath.DegToRad(angle.pitch));
        double Z = FOV * Math.Sin(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));

        return new Point3d(X, Y, Z);
    }

    public Plane PlaneFinder(Point3d center, Point3d position)
    {
        double[] directionVector = new double[3];
        double result;

        directionVector[0] = position.X - center.X;
        directionVector[1] = position.Y - center.Y;
        directionVector[2] = position.Z - center.Z;

        result = (directionVector[0] * (-center.X)) + (directionVector[1] * (-center.Y)) + (directionVector[2] * (-center.Z));

        return new Plane(-directionVector[0], -directionVector[1], -directionVector[2], result);
    }

    public (Angle, double) GetScreenAngles(Ratio ratio, double FOV, Angle camAngle, double ratioScale)
    {
        Angle angle = new Angle(AMath.RadToDeg(Math.Atan(ratio.width*ratioScale/2/FOV)), AMath.RadToDeg(Math.Atan(ratio.height*ratioScale/2/FOV)), camAngle.roll);
        double newDist = Math.Sqrt((ratio.width*ratioScale/2)*(ratio.width*ratioScale/2) + (ratio.height*ratioScale/2)*(ratio.height*ratioScale/2) + FOV*FOV);

        return (angle, newDist);
    }

}

