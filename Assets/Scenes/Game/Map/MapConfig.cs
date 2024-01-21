using UnityEngine;

public class MapConfig : MonoBehaviour
{
    public static float XMin { get; private set; }
    public static float XMax { get; private set; }
    public static float YMin { get; private set; }
    public static float YMax { get; private set; }

    void Start()
    {
        Renderer objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null)
        {
            Debug.LogError("Object does not have a Renderer component.");
            return;
        }

        Bounds objectBounds = objectRenderer.bounds;

        float top = objectBounds.max.y;
        float left = objectBounds.min.x;
        float bottom = objectBounds.min.y;
        float right = objectBounds.max.x;

        XMin = left;
        XMax = right;
        YMin = bottom;
        YMax = top;
    }


}
