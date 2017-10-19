using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class SpriteStretch : MonoBehaviour
{
    public enum StretchType { Horizontal, Vertical, Both }
    public StretchType stretchDirection = StretchType.Horizontal;
    public Vector2 offset = new Vector2(0f, 0f);
    public bool half;//only for horizontal now

    SpriteRenderer _sprite;
    Transform _thisTransform;
    Camera _mainCam;

    void Awake()
    {
        _thisTransform = transform;
        _sprite = GetComponent<SpriteRenderer>();
        _mainCam = Camera.main;
        StartCoroutine("Stretch");
    }
#if UNITY_EDITOR
    void Update()
    {
        _mainCam = Camera.main;
        Scale();
    }
#endif
    IEnumerator Stretch()
    {
        yield return new WaitForEndOfFrame();
        Scale();
    }
    void Scale()
    {
        float worldScreenHeight = _mainCam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        float ratioScale = worldScreenWidth / _sprite.sprite.bounds.size.x;
        if (half)
            ratioScale /= 2.0f;
        ratioScale += offset.x;
        float h = worldScreenHeight / _sprite.sprite.bounds.size.y;
        h += offset.y;
        switch (stretchDirection)
        {
            case StretchType.Horizontal:
                _thisTransform.localScale = new Vector3(ratioScale, _thisTransform.localScale.y, _thisTransform.localScale.z);
                break;
            case StretchType.Vertical:
                _thisTransform.localScale = new Vector3(_thisTransform.localScale.x, h, _thisTransform.localScale.z);
                break;
            case StretchType.Both:
                _thisTransform.localScale = new Vector3(ratioScale, h, _thisTransform.localScale.z);
                break;
            default: break;
        }
    }
}