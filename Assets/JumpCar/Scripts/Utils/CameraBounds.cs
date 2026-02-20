using UnityEngine;

public static class CameraBounds
{
    public static void CalculateBounds(Camera camera, 
        out float leftBound,
        out float rightBound,
        out float bottomBound,
        out float topBound)
    {
        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        leftBound = bottomLeft.x;
        rightBound = topRight.x;
        bottomBound = bottomLeft.y;
        topBound = topRight.y;
    }
}
