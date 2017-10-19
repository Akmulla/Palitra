using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class MeshResize : MonoBehaviour
{
    [SerializeField]
    bool _left;
    Mesh _mesh;
    Transform _thisTransform;
    Camera _mainCam;

    void Awake()
    {
        _thisTransform = transform;
        _mesh = GetComponent<MeshFilter>().sharedMesh;
        _mainCam = Camera.main;
        //StartCoroutine("stretch");
    }
//#if UNITY_EDITOR
//    void Update()
//    {
//        mainCam = Camera.main;
//        scale();
//    }
//#endif
    IEnumerator Stretch()
    {
        yield return new WaitForEndOfFrame();
        Scale();
    }
    public void Scale()
    {
        float worldScreenHeight = _mainCam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        float ratioScale = worldScreenWidth / _mesh.bounds.size.x;
        ratioScale /= 2.0f;
       // float h = worldScreenHeight / mesh.bounds.size.y;

        float leftEdge = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        float rightEdge = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

        float k = _left ? -1.0f : 1.0f;

        _thisTransform.localScale = new Vector3
            (-k, _thisTransform.localScale.y, _thisTransform.localScale.z);


        _thisTransform.position = new Vector3(-k* (((rightEdge + leftEdge) / 2.0f)- _mesh.bounds.extents.x),
            _thisTransform.position.y, _thisTransform.position.z);

        //_thisTransform.localScale = new Vector3
        //    (-k*ratioScale, _thisTransform.localScale.y, _thisTransform.localScale.z);


        //_thisTransform.position = new Vector3(k*(rightEdge-leftEdge)/4.0f,
        //    _thisTransform.position.y, _thisTransform.position.z);


    }
}
