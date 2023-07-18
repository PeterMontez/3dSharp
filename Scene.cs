namespace _3dSharp;

public static class Scene
{
    public static List<Triangle> objects { get; set; } = new List<Triangle>();

    public static List<Triangle2d> rendered { get; set; } = new List<Triangle2d>();

    public static void AddObject(Triangle triangle)
    {
        objects.Add(triangle);
    }

    public static List<Triangle2d> BruteRender(Camera camera)
    {
        BruteRenderer renderer = new BruteRenderer(objects);

        rendered = renderer.Render(camera);

        return rendered;
    }

}

