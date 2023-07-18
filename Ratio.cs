namespace _3dSharp;

public class Ratio
{
    public int width { get; set; }
    public int height { get; set; }

    public int scrWidth { get; set; }
    public int scrHeight { get; set; }

    public Ratio(int width, int height, int resolution)
    {
        this.width = width;
        this.height = height;
        this.scrHeight = resolution;
        this.scrWidth = resolution/height*width;
    }

}

