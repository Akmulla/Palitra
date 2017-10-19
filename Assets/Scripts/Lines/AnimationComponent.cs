using System.Collections;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    [SerializeField]
    MeshFilter _meshFilterRight;
    [SerializeField]
    MeshFilter _meshFilterLeft;
    Line _line;
    public MeshData meshData;
    int _currentMesh;
    
    int _startMesh = 2;
    int _endMesh = 11;

    void Awake()
    {
        _line = GetComponent<Line>();
    }
   
    public void BeginAnimation()
    {
        if (GameController.gameController.GetState()==GameState.Game)
        {
            StartCoroutine(AnimationCoroutine());
        }
    }

    void OnEnable()
    {
        StopAllCoroutines();
        ResetAnimation();
    }
  
    void OnDisable()
    {
        ResetAnimation();
    }
    IEnumerator AnimationCoroutine()
    {
        float ballStart = Ball.ball.GetCollisionPosition().y;
        float height = _line.GetHeight();
        _currentMesh = _startMesh;
        float meshDist = _endMesh - _startMesh;
        float dist = height * 2.0f;
        float cell = dist / meshDist;

        SetMesh(meshData.meshes[_currentMesh]);

        //если скорость выше определенного занчения
         while ((_currentMesh < meshData.meshes.Length - _startMesh - 1)&&
            (BallMove.ballMove.Speed > 4.5f))
        {
            float position = Ball.ball.GetCollisionPosition().y - ballStart;
            _currentMesh = (int)(position / cell);
            SetMesh(meshData.meshes[_currentMesh + _startMesh]);
            yield return new WaitForEndOfFrame();
        }
        _currentMesh++;
        //если нет
        while (_currentMesh < meshData.meshes.Length - _startMesh)
        {
            SetMesh(meshData.meshes[_currentMesh + _startMesh]);
            _currentMesh++;
            yield return new WaitForSeconds(0.025f);
        }
    }

    void SetMesh(Mesh mesh)
    {
        _meshFilterRight.mesh = mesh;
        _meshFilterLeft.mesh = mesh;
    }

    public void ResetAnimation()
    {
        _currentMesh = 0;
        SetMesh(meshData.meshes[_currentMesh]);
    }
}
