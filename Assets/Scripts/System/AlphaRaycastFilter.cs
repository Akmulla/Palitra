﻿using UnityEngine;
using UnityEngine.UI;

public class AlphaRaycastFilter : MonoBehaviour, ICanvasRaycastFilter
{
    private RectTransform _rectTransform;
    private Image _image;

    void Awake()
    {
        _rectTransform = transform as RectTransform;
        _image = GetComponent<Image>();
    }

    #region ICanvasRaycastFilter implementation

    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        // Get normalized hit point within rectangle (aka UV coordinates originating from bottom-left)
        Vector2 rectPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPoint, eventCamera, out rectPoint);
        Vector2 normPoint = (rectPoint - _rectTransform.rect.min);
        normPoint.x /= _rectTransform.rect.width;
        normPoint.y /= _rectTransform.rect.height;

        // Read pixel color at normalized hit point
        Texture2D texture = _image.sprite.texture;
        Color color = texture.GetPixel((int)(normPoint.x * texture.width), (int)(normPoint.y * texture.height));

        // Filter away hits on transparent pixels
        return color.a > 0f;
    }

    #endregion
}