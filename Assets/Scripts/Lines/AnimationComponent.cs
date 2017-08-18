using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationComponent : MonoBehaviour
{
    [SerializeField]
    MeshFilter mesh_filter_right;
    [SerializeField]
    MeshFilter mesh_filter_left;
    Line line;
    public MeshData mesh_data;
    int current_mesh = 0;
    
    int start_mesh = 2;
    int end_mesh = 11;

    void Awake()
    {
        line = GetComponent<Line>();
    }
   
    public void BeginAnimation()
    {
        if (GameController.game_controller.GetState()==GameState.Game)
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
        float ball_start = Ball.ball.GetCollisionPosition().y;
        float height = line.GetHeight();
        current_mesh = start_mesh;
        float mesh_dist = (float)(end_mesh - start_mesh);
        float dist = height * 2.0f;
        float cell = (float)dist / mesh_dist;

        SetMesh(mesh_data.meshes[current_mesh]);

        //во время прохождения линии
         while ((current_mesh < mesh_data.meshes.Length - start_mesh - 1)&&
            (BallMove.ball_move.Speed > 4.0f))
        {
            float position = Ball.ball.GetCollisionPosition().y - ball_start;
            current_mesh = (int)(position / cell);
            SetMesh(mesh_data.meshes[current_mesh + start_mesh]);
            yield return new WaitForEndOfFrame();
        }
        current_mesh++;
        //после того как прошли линию
        while (current_mesh < mesh_data.meshes.Length - start_mesh)
        {
            SetMesh(mesh_data.meshes[current_mesh + start_mesh]);
            current_mesh++;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void SetMesh(Mesh mesh)
    {
        mesh_filter_right.mesh = mesh;
        mesh_filter_left.mesh = mesh;
    }

    public void ResetAnimation()
    {
        current_mesh = 0;
        SetMesh(mesh_data.meshes[current_mesh]);
    }
}
