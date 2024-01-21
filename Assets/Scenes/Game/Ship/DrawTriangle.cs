using UnityEngine;


public class DrawTriangle : MonoBehaviour
{
    private Material lineMaterial;
    public Color lineColor = Color.white;

    void Start()
    {
        lineMaterial = new Material(Shader.Find("Sprites/Default"));
        // Create a new GameObject with LineRenderer component
        GameObject triangleObject = new GameObject("TriangleObject");
        LineRenderer lineRenderer = triangleObject.AddComponent<LineRenderer>();

        // Set LineRenderer properties
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 4; // A triangle has four points (closing the loop)

        // Define the vertices of the triangle
        Vector3[] triangleVertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0.6f, 0, 0),
            new Vector3(0.3f, 0.8f, 0),
            new Vector3(0, 0, 0) // Closing the loop
        };

        // Set the vertices in the LineRenderer
        lineRenderer.SetPositions(triangleVertices);
    }
}
