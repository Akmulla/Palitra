using UnityEngine;
using System.Collections;

public class Edges : MonoBehaviour
{
    public static float leftEdge, rightEdge, botEdge, topEdge,pix_size,center_x;

    private Camera _camera;

    void Awake ()
    {
        _camera = Camera.main;
        leftEdge = _camera.ScreenToWorldPoint(new Vector2(0, 0)).x;
        rightEdge = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        topEdge = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        botEdge = _camera.ScreenToWorldPoint(new Vector2(0, 0)).y;
        center_x = (rightEdge + leftEdge) / 2.0f;
        pix_size = (topEdge - botEdge) / Screen.height;
    }

	void Update ()
    {
        if (!_camera)
        {
            _camera = Camera.main;
        }
        topEdge = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        botEdge = _camera.ScreenToWorldPoint(new Vector2(0, 0)).y;
    }
}
