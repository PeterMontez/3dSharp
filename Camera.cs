using System.Drawing;

namespace _3dSharp;

public class Camera
{
    public Point3d Position { get; set; }
    public double FOV { get; set; }
    public Angle Angle { get; set; }
    public CameraView CameraView { get; set; }

    public Camera(Point3d position, double FOV, Angle angle, Ratio ratio, double ratioScale)
    {
        this.Position = position;
        this.FOV = FOV;
        this.Angle = angle;
        this.CameraView = new CameraView(this.Position, this.FOV, this.Angle, ratio, ratioScale);
    }

    public void YawAdd(double yaw)
    {
        CameraView.Angle.YawAdd(yaw);
        CameraView.Refresh();
    }

    public void PitchAdd(double pitch)
    {
        CameraView.Angle.PitchAdd(pitch);
        CameraView.Refresh();
    }

    public void RollAdd(double roll)
    {
        CameraView.Angle.RollAdd(roll);
        CameraView.Refresh();
    }

    public void Translate(double X, double Y, double Z)
    {
        this.Position.X += X;
        this.Position.Y += Y;
        this.Position.Z += Z;
        CameraView.Refresh();
    }

    public void MoveTo(Point3d position) => this.Position = position;

}
