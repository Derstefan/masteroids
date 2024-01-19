using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform shipTransform;
    public Vector3 offset;


    void Update()
    {
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Camera.main.aspect;
        Vector3 pos = shipTransform.position;
        if (shipTransform.position.x - width / 2 < MapConfig.XMin)
        {
            pos.x = MapConfig.XMin + width / 2;
        }
        else if (shipTransform.position.x + width / 2 > MapConfig.XMax)
        {
            pos.x = MapConfig.XMax - width / 2;
        }

        if (shipTransform.position.y - height / 2 < MapConfig.YMin)
        {
            pos.y = MapConfig.YMin + height / 2;
        }
        else if (shipTransform.position.y + height / 2 > MapConfig.YMax)
        {
            pos.y = MapConfig.YMax - height / 2;
        }

        transform.position = pos + offset;
    }
}

