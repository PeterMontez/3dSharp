using System.Drawing;

namespace _3dSharp;

public class CameraView
{
    public Point3d[] Points { get; set; }= new Point3d[4];
    public Point3d Position { get; set; }
    public Point3d FOVpoint { get; set; } = new Point3d(0, 0, 0);
    public Ratio Ratio { get; set; }
    public Plane Plane { get; set; }
    public Angle Angle { get; set; }
    public Angle ScreenAngle { get; set; } = new Angle(0, 0, 0);
    public double FOV { get; set; }
    public double RatioScale { get; set; }
    public double ScreenDist { get; set; }

    public Point3d[] MainPoints { get; set; } = new Point3d[4];
    public Point3d MainFOV { get; set; } = new Point3d(0, 0, 0);

    public CameraView(Point3d position, double FOV, Angle angle, Ratio ratio, double ratioScale)
    {
        this.Ratio = ratio;
        this.FOV = FOV;
        this.Angle = angle;
        this.RatioScale = ratioScale;
        this.FOVpoint = GetCenter(position, FOV, angle);
        this.Position = position;
        this.Plane = PlaneFinder(this.FOVpoint, this.Position);
        (this.ScreenAngle, this.ScreenDist) = GetScreenAngles(this.Ratio, this.FOV, this.Angle, this.RatioScale);
        // this.Points = GetViewScreen(this.FOVpoint, angle, this.ScreenAngle, this.Plane);

        // this.MainPoints = Points;

        this.MainPoints = TempPoints();
        this.Points = MainPoints;
        this.MainFOV = FOVpoint;
    }

    public void Refresh()
    {
        FOVpoint = GetCenter(Position, FOV, Angle);
        Plane = PlaneFinder(FOVpoint, Position);
        (ScreenAngle, ScreenDist) = GetScreenAngles(Ratio, FOV, Angle, RatioScale);
        // Points = GetViewScreen(FOVpoint, Angle, ScreenAngle, Plane);
        
        (FOVpoint, Points) = GetViewScreenV2(Angle, MainPoints, Position, MainFOV);
    }

    public Point3d GetCenter(Point3d position, double FOV, Angle angle)
    {
        double X = FOV * Math.Cos(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));
        double Y = FOV * Math.Sin(AMath.DegToRad(angle.pitch));
        double Z = FOV * Math.Sin(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));

        return new Point3d(X+position.X, Y+position.Y, Z+position.Z);
    }

    public Point3d[] TempPoints()
    {
        Point3d[] points = new Point3d[4];

        points[0] = new Point3d(800, -450, -800);
        points[1] = new Point3d(800, 450, -800);
        points[2] = new Point3d(800, 450, 800);
        points[3] = new Point3d(800,- 450, 800);

        return points;
    }

    public Point3d[] GetViewScreen(Point3d center, Angle angle, Angle screenAngle, Plane plane)
    {
        Point3d[] points = new Point3d[4];

        Angle crrAngle = new Angle(angle.AngleFixer(angle.yaw - screenAngle.yaw), angle.AngleFixer(angle.pitch - screenAngle.pitch), angle.roll);
        // points[0] = GetPoint(this.position, this.screenDist, crrAngle);
        points[0] = LinePlaneIntersec(new Line(Position, GetPoint(this.Position, FOV*10, crrAngle)), plane);

        crrAngle = new Angle(angle.AngleFixer(angle.yaw - screenAngle.yaw), angle.AngleFixer(angle.pitch + screenAngle.pitch), angle.roll);
        // points[1] = GetPoint(this.position, this.screenDist, crrAngle);
        points[1] = LinePlaneIntersec(new Line(Position, GetPoint(this.Position, FOV*10, crrAngle)), plane);

        crrAngle = new Angle(angle.AngleFixer(angle.yaw + screenAngle.yaw), angle.AngleFixer(angle.pitch + screenAngle.pitch), angle.roll);
        // points[2] = GetPoint(this.position, this.screenDist, crrAngle);
        points[2] = LinePlaneIntersec(new Line(Position, GetPoint(this.Position, FOV*10, crrAngle)), plane);

        crrAngle = new Angle(angle.AngleFixer(angle.yaw + screenAngle.yaw), angle.AngleFixer(angle.pitch - screenAngle.pitch), angle.roll);
        // points[3] = GetPoint(this.position, this.screenDist, crrAngle);
        points[3] = LinePlaneIntersec(new Line(Position, GetPoint(this.Position, FOV*10, crrAngle)), plane);

        return points;
    }

    public (Point3d, Point3d[]) GetViewScreenV2(Angle angle, Point3d[] mainPoints, Point3d position, Point3d mainFOV)
    {
        Point3d[] points = new Point3d[4];

        Point3d[] temp = {mainPoints[0], mainPoints[1], mainPoints[2], mainPoints[3], mainFOV};

        for (int i = 0; i < mainPoints.Length; i++)
        {
            double X = AMath.DgCos(angle.roll) * AMath.DgCos(angle.yaw) * temp[i].X + 
            (-AMath.DgSin(angle.roll)) * temp[i].Y +
            AMath.DgCos(angle.roll) * AMath.DgSin(angle.yaw) * temp[i].Z;

            double Y = (AMath.DgCos(angle.pitch) * AMath.DgSin(angle.roll) * AMath.DgCos(angle.yaw) + AMath.DgSin(angle.pitch) * AMath.DgSin(angle.yaw)) * temp[i].X +
            AMath.DgCos(angle.pitch) * AMath.DgCos(angle.roll) * temp[i].Y +
            (AMath.DgCos(angle.pitch) * AMath.DgSin(angle.roll) * AMath.DgSin(angle.yaw) - AMath.DgSin(angle.pitch) * AMath.DgCos(angle.yaw)) * temp[i].Z;

            double Z = (AMath.DgSin(angle.pitch) * AMath.DgSin(angle.roll) * AMath.DgCos(angle.yaw) + AMath.DgCos(angle.pitch) * AMath.DgSin(angle.yaw)) * temp[i].X +
            AMath.DgSin(angle.pitch) * AMath.DgCos(angle.roll) * temp[i].Y +
            (AMath.DgSin(angle.pitch) * AMath.DgSin(angle.roll) * AMath.DgSin(angle.yaw) - AMath.DgCos(angle.pitch) * AMath.DgCos(angle.yaw)) * temp[i].Z;

            points[i] = new Point3d(X+position.X, Y+position.Y, Z+position.Z);
        }

        return points;
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

    public Point3d GetPoint(Point3d position, double FOV, Angle angle)
    {
        double X = FOV * Math.Cos(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));
        double Y = FOV * Math.Sin(AMath.DegToRad(angle.pitch));
        double Z = FOV * Math.Sin(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));

        return new Point3d(X+position.X, Y+position.Y, Z+position.Z);
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

