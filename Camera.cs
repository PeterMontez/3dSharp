using System.Drawing;

namespace _3dSharp;

public class Camera
{
    public Point3d position { get; set; }
    public double FOV { get; set; }
    public Angle angle { get; set; }
    public CameraView cameraView { get; set; }

    public Camera(Point3d position, double FOV, Angle angle, Ratio ratio, double ratioScale)
    {
        this.position = position;
        this.FOV = FOV;
        this.angle = angle;
        this.cameraView = new CameraView(position, FOV, angle, ratio, ratioScale);
    }

    public void YawAdd(double yaw)
    {
        angle.YawAdd(yaw);
        cameraView.angle.YawAdd(yaw);
        cameraView.Refresh();
    }

    public void PitchAdd(double pitch)
    {
        angle.PitchAdd(pitch);
        cameraView.angle.PitchAdd(pitch);
        cameraView.Refresh();
    }

    public void RollAdd(double roll)
    {
        angle.RollAdd(roll);
        cameraView.angle.RollAdd(roll);
        cameraView.Refresh();
    }

    public void Translate(double X, double Y, double Z)
    {
        this.position.X += X;
        this.position.Y += Y;
        this.position.Z += Z;
        cameraView.Refresh();
    }

    public void MoveTo(Point3d position) => this.position = position;

}
