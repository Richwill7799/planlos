
using UnityEngine;

public static class Extensions
{
    public static Vector2 Xy(this Vector3 vec3) => new Vector2(vec3.x, vec3.y);
    public static Vector3 Div(this Vector3 vec, Vector3 v) => new Vector3(vec.x / v.x, vec.y / v.y, vec.z / v.z);

    public static (int, int) ToPixel(this Vector2 v, int w, int h) =>
        (Mathf.RoundToInt(w - v.x * w), Mathf.RoundToInt(h - v.y * h));
}