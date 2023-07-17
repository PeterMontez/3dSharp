using System.Drawing;

namespace _3dSharp;

public class Camera
{
    public Point3d position { get; set; }
    public double FOV { get; set; }
    public Angle angle { get; set; }
    public CameraView cameraView { get; set; }

    public Camera(Point3d position, double FOV, Angle angle)
    {
        this.position = position;
        this.FOV = FOV;
        this.angle = angle;
    }

    public void YawAdd(double yaw) => angle.YawAdd(yaw);

    public void PitchAdd(double pitch) => angle.PitchAdd(pitch);

    public void Roll(double roll) => angle.RollAdd(roll);
}

public class Angle
{
    public double yaw { get; set; }
    public double pitch { get; set; }
    public double roll { get; set; }

    public Angle(double yaw, double pitch, double roll)
    {
        this.yaw = AngleFixer(yaw);
        this.pitch = AngleFixer(pitch);
        this.roll = AngleFixer(roll);
    }

    public void YawMoveTo(double yaw) => this.yaw = (yaw - ((yaw % 360)*360));

    public void PitchMoveTo(double pitch) => this.pitch = (pitch - ((pitch % 360)*360));

    public void RollMoveTo(double roll) => this.roll = (roll - ((roll % 360)*360));

    public void YawAdd(double yaw)
    {
        this.yaw = yaw;
        this.yaw = AngleFixer(this.yaw);
    }

    public void PitchAdd(double pitch)
    {
        this.pitch += pitch;
        this.pitch = AngleFixer(this.pitch);
    }

    public void RollAdd(double roll)
    {
        this.roll += roll;
        this.roll = AngleFixer(this.roll);
    }

    public double AngleFixer(double angle)
    {
        if (angle < 0)
            angle = 360 - (-angle % 360);

        if (angle >= 360)
            angle %= 360;

        return angle;
    }

}
