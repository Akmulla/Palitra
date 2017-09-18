using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class MeshResize : MonoBehaviour
{
    [SerializeField]
    bool left;
    Mesh mesh;
    Transform _thisTransform;
    Camera mainCam;

    void Awake()
    {
        _thisTransform = transform;
        mesh = GetComponent<MeshFilter>().sharedMesh;
        mainCam = Camera.main;
        //StartCoroutine("stretch");
    }
//#if UNITY_EDITOR
//    void Update()
//    {
//        mainCam = Camera.main;
//        scale();
//    }
//#endif
    IEnumerator stretch()
    {
        yield return new WaitForEndOfFrame();
        scale();
    }
    public void scale()
    {
        float worldScreenHeight = mainCam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        float ratioScale = worldScreenWidth / mesh.bounds.size.x;
        ratioScale /= 2.0f;
       // float h = worldScreenHeight / mesh.bounds.size.y;

        float leftEdge = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        float rightEdge = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

        float k = left ? -1.0f : 1.0f;

        _thisTransform.localScale = new Vector3
            (-k, _thisTransform.localScale.y, _thisTransform.localScale.z);


        _thisTransform.position = new Vector3(-k* (((rightEdge + leftEdge) / 2.0f)- mesh.bounds.extents.x),
            _thisTransform.position.y, _thisTransform.position.z);

        //_thisTransform.localScale = new Vector3
        //    (-k*ratioScale, _thisTransform.localScale.y, _thisTransform.localScale.z);


        //_thisTransform.position = new Vector3(k*(rightEdge-leftEdge)/4.0f,
        //    _thisTransform.position.y, _thisTransform.position.z);


    }
}
