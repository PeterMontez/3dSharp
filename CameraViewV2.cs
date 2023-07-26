using System.Drawing;

namespace _3dSharp;

public class CameraView2
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

    public CameraView2(Point3d position, double FOV, Angle angle, Ratio ratio, double ratioScale)
    {
        this.Ratio = ratio;
        this.FOV = FOV;
        this.Angle = angle;
        this.RatioScale = ratioScale;
        this.FOVpoint = GetCenter(position, FOV, angle);
        this.Position = position;
        this.Plane = PlaneFinder(this.FOVpoint, this.Position);

        this.MainPoints = GetPoints();
        this.Points = MainPoints;
        this.MainFOV = FOVpoint;
    }

    public void Refresh()
    {
        (FOVpoint, Points) = GetViewScreenV2(Angle, MainPoints, Position, MainFOV);
        Plane = PlaneFinder(FOVpoint, Position);
    }

    public Point3d GetCenter(Point3d position, double FOV, Angle angle)
    {
        double X = FOV * Math.Cos(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));
        double Y = FOV * Math.Sin(AMath.DegToRad(angle.pitch));
        double Z = FOV * Math.Sin(AMath.DegToRad(angle.yaw)) * Math.Cos(AMath.DegToRad(angle.pitch));

        return new Point3d(X+position.X, Y+position.Y, Z+position.Z);
    }

    public Point3d[] GetPoints()
    {
        Point3d[] points = new Point3d[4];

        points[0] = new Point3d(FOV, -Ratio.height/2, -Ratio.width/2);
        points[1] = new Point3d(FOV, Ratio.height/2, -Ratio.width/2);
        points[2] = new Point3d(FOV, Ratio.height/2, Ratio.width/2);
        points[3] = new Point3d(FOV,- Ratio.height/2, Ratio.width/2);

        return points;
    }

    public (Point3d, Point3d[]) GetViewScreenV2(Angle angle, Point3d[] mainPoints, Point3d position, Point3d mainFOV)
    {
        Point3d[] points = new Point3d[4];

        Point3d[] temp = {mainPoints[0], mainPoints[1], mainPoints[2], mainPoints[3], mainFOV};

        double rRotation = angle.roll;

        for (int i = 0; i < temp.Length; i++)
        {
            double Z = temp[i].Z * AMath.DgCos(rRotation) - temp[i].Y * AMath.DgSin(rRotation);
            double Y = temp[i].Z * AMath.DgSin(rRotation) + temp[i].Y * AMath.DgCos(rRotation);

            temp[i] = new Point3d(temp[i].X, Y, Z);
        }


        Angle hRotation = new Angle(0,0,angle.pitch);

        for (int i = 0; i < temp.Length; i++)
        {
            double X = AMath.DgCos(hRotation.roll) * AMath.DgCos(hRotation.yaw) * temp[i].X + 
            (-AMath.DgSin(hRotation.roll)) * temp[i].Y +
            AMath.DgCos(hRotation.roll) * AMath.DgSin(hRotation.yaw) * temp[i].Z;

            double Y = (AMath.DgCos(hRotation.pitch) * AMath.DgSin(hRotation.roll) * AMath.DgCos(hRotation.yaw) + AMath.DgSin(hRotation.pitch) * AMath.DgSin(hRotation.yaw)) * temp[i].X +
            AMath.DgCos(hRotation.pitch) * AMath.DgCos(hRotation.roll) * temp[i].Y +
            (AMath.DgCos(hRotation.pitch) * AMath.DgSin(hRotation.roll) * AMath.DgSin(hRotation.yaw) - AMath.DgSin(hRotation.pitch) * AMath.DgCos(hRotation.yaw)) * temp[i].Z;

            double Z = (AMath.DgSin(hRotation.pitch) * AMath.DgSin(hRotation.roll) * AMath.DgCos(hRotation.yaw) + AMath.DgCos(hRotation.pitch) * AMath.DgSin(hRotation.yaw)) * temp[i].X +
            AMath.DgSin(hRotation.pitch) * AMath.DgCos(hRotation.roll) * temp[i].Y +
            (AMath.DgSin(hRotation.pitch) * AMath.DgSin(hRotation.roll) * AMath.DgSin(hRotation.yaw) - AMath.DgCos(hRotation.pitch) * AMath.DgCos(hRotation.yaw)) * temp[i].Z;

            temp[i] = new Point3d(X, Y, Z);
        }

        Angle vRotation = new Angle(angle.yaw,0,0);

        for (int i = 0; i < temp.Length; i++)
        {
            double X = AMath.DgCos(vRotation.roll) * AMath.DgCos(vRotation.yaw) * temp[i].X + 
            (-AMath.DgSin(vRotation.roll)) * temp[i].Y +
            AMath.DgCos(vRotation.roll) * AMath.DgSin(vRotation.yaw) * temp[i].Z;

            double Y = (AMath.DgCos(vRotation.pitch) * AMath.DgSin(vRotation.roll) * AMath.DgCos(vRotation.yaw) + AMath.DgSin(vRotation.pitch) * AMath.DgSin(vRotation.yaw)) * temp[i].X +
            AMath.DgCos(vRotation.pitch) * AMath.DgCos(vRotation.roll) * temp[i].Y +
            (AMath.DgCos(vRotation.pitch) * AMath.DgSin(vRotation.roll) * AMath.DgSin(vRotation.yaw) - AMath.DgSin(vRotation.pitch) * AMath.DgCos(vRotation.yaw)) * temp[i].Z;

            double Z = (AMath.DgSin(vRotation.pitch) * AMath.DgSin(vRotation.roll) * AMath.DgCos(vRotation.yaw) + AMath.DgCos(vRotation.pitch) * AMath.DgSin(vRotation.yaw)) * temp[i].X +
            AMath.DgSin(vRotation.pitch) * AMath.DgCos(vRotation.roll) * temp[i].Y +
            (AMath.DgSin(vRotation.pitch) * AMath.DgSin(vRotation.roll) * AMath.DgSin(vRotation.yaw) - AMath.DgCos(vRotation.pitch) * AMath.DgCos(vRotation.yaw)) * temp[i].Z;

            temp[i] = new Point3d(X+position.X, Y+position.Y, Z+position.Z);
        }

        points = new Point3d[] {temp[0], temp[1], temp[2], temp[3]};

        return (temp[4], points);
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

}

