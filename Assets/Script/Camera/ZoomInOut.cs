using UnityEngine;
using System.Collections;

public class ZoomInOut : MonoBehaviour
{

    public float ROTSpeed = 10f;
    public float min = -10.0f;
    public float max = 10.0f;
    public bool invert = true;

    public void Start()
    {
        min = Camera.main.orthographicSize + min;
        max = Camera.main.orthographicSize + max;
    }

    public void Update()
    {
        if (Camera.main.orthographicSize <= min)
            Camera.main.orthographicSize = min;
        if (Camera.main.orthographicSize >= max)
            Camera.main.orthographicSize = max;
        Camera.main.orthographicSize += (invert ? -1 : 1) * Input.GetAxis("Mouse ScrollWheel") * ROTSpeed;
    }
}
