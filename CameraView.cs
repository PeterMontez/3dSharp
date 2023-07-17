using System.Drawing;

namespace _3dSharp;

public class CameraView
{
    public Point3d[] points = new Point3d[4];

    public Point3d FOVpoint { get; set; } = new Point3d(0, 0, 0);

    public CameraView(Point3d position, double FOV, Angle angle)
    {
        this.FOVpoint = getCenter(position, FOV, angle);
    }

    public Point3d getCenter(Point3d position, double FOV, Angle angle)
    {
        double X = FOV * Math.Cos(AMath.DegToRad(angle.pitch)) * Math.Cos(AMath.DegToRad(-angle.yaw));
        double Z = FOV * Math.Sin(AMath.DegToRad(-angle.yaw));
        double Y = FOV * Math.Sin(AMath.DegToRad(angle.pitch)) * Math.Cos(AMath.DegToRad(-angle.yaw));

        X = AMath.Round(X, 2);
        Y = AMath.Round(Y, 2);
        Z = AMath.Round(Z, 2);

        return new Point3d(X, Y, Z);
    }

}

