using UnityEngine;

public static class Extention
{
    public static Quaternion Vector2ToRotation(this Vector2 vector2)
    {
        if (vector2 == Vector2.up)
        {
            return Quaternion.Euler(Vector2.zero);
        }
        else if (vector2 == Vector2.right)
        {
            return Quaternion.Euler(Vector3.forward * 90);
        }
        else if (vector2 == Vector2.down)
        {
            return Quaternion.Euler(Vector3.forward * 180);
        }
        else if (vector2 == Vector2.left)
        {
            return Quaternion.Euler(Vector3.forward * 270);
        }
        else
        {
            return Quaternion.Euler(Vector2.zero);
        }
    }
}
