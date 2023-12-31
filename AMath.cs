public static class AMath
{
    public static double DegToRad(double deg)
    {
        return deg * Math.PI / 180;
    }

    public static double RadToDeg(double rad)
    {
        return rad * 180/Math.PI;
    }

    public static double Round(double value, int decimalPlaces)
    {
        return Math.Round(value, decimalPlaces, MidpointRounding.AwayFromZero);
    }

    public static double DgCos(double deg)
    {
        return Math.Cos(AMath.DegToRad(deg));
    }

    public static double DgSin(double deg)
    {
        return Math.Sin(AMath.DegToRad(deg));
    }

    public static double DgTan(double deg)
    {
        return Math.Tan(AMath.DegToRad(deg));
    }
}