using UnityEngine;
using System.Collections;

public class BlockManager_Order : MonoBehaviour
{
    public int block_count;
    public GameObject block;
    public Transform block_holder;
    public Transform arrow;

    [SerializeField]
    float offset_y=0.0f;
    [SerializeField]
    Block_Order[] block_mas;
    Line_Order line;
    float block_size;
    float window_size;
    //int active_block_count;
    int current_block;
    SpriteRenderer arrow_rend;
    
    void Awake()
    {
        line = GetComponent<Line_Order>();
        arrow_rend = arrow.gameObject.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        //active_block_count = block_count;
        EventManager.StartListening("BallColorChanged", ColorChanged);
    }

    void OnDisable()
    {
        EventManager.StopListening("BallColorChanged", ColorChanged);
    }

    void ColorChanged()
    {
        if (!line.finished)
        {
            if ( (Ball.ball.GetPosition().y > line.prev_edge) &&
                    (Ball.ball.GetColor() == block_mas[current_block].GetColor()) )
            {
                block_mas[current_block].Disable();
                current_block++;

                if (current_block >= block_count)
                {
                    line.finished = true;
                    BallMove.ball_move.ResumeSpeed();
                    arrow.gameObject.SetActive(false);
                    return;
                }

                arrow.position = block_mas[current_block].GetPosition() + 
                    new Vector3(0.0f, line.GetHeight()+ offset_y, 0.0f);
            }
            else
            {
                foreach (Block_Order item in block_mas)
                {
                    item.Enable();
                }

                current_block = 0;
                arrow.position = block_mas[0].GetPosition() + 
                    new Vector3(0.0f, line.GetHeight() + offset_y, 0.0f);
            }

            arrow_rend.color = block_mas[current_block].GetColor();
        }
    }
    public void InitBlocks()
    {
        current_block = 0;
        window_size = Edges.rightEdge - Edges.leftEdge;
        block_size = window_size / (float)block_count;
        Vector3 spawn_position;
        GameObject obj;

        for (int i = 0; i < block_count; i++)
        {
            spawn_position = new Vector3
                (Edges.leftEdge + block_size / 2.0f + block_size * i, transform.position.y);
            obj = block_mas[i].gameObject;
            obj.transform.position = spawn_position;
            obj.transform.localScale = new Vector3(block_size, obj.transform.localScale.y, 1.0f);
            obj.transform.SetParent(block_holder);
            obj.SetActive(true);
        }
        
        SetColors();
        arrow.gameObject.SetActive(true);
        if (block_mas != null)
            arrow.position = block_mas[0].GetPosition() + 
                new Vector3(0.0f, line.GetHeight(), 0.0f);
        arrow_rend.color = block_mas[0].GetColor();
    }

    public void SetDefault()
    {
        current_block = 0;
        arrow.gameObject.SetActive(true);
        arrow.position = block_mas[0].GetPosition() +
                new Vector3(0.0f, line.GetHeight() + offset_y, 0.0f);
        arrow_rend.color = block_mas[0].GetColor();
    }

    void SetColors()
    {
        Color[] new_colors;
        float full_length =Mathf.Abs( Edges.center_x - line.left.GetComponent<Renderer>().bounds.size.x);
        float visible_lenght= Mathf.Abs(Edges.center_x - Edges.leftEdge);
        
        float unused_part = ((full_length - visible_lenght) / full_length)/2.0f;
        Texture2D texture = line.texture_handler.CreateTexture(SkinManager.skin_manager.GetCurrentSkin().colors
            , block_count, unused_part, out new_colors);

        for (int i = 0; i < block_count; i++)
        {
            block_mas[i].SetColor(new_colors[i]);
        }

        line.SetTexture(texture);
    }


    void SetRandomColors()
    {
        Color[] colors = new Color[block_count];
        colors[0] = SkinManager.skin_manager.GetCurrentSkin().colors
       [UnityEngine.Random.Range(0, SkinManager.skin_manager.GetCurrentSkin().colors.Length)];
        for (int i = 1; i < block_count; i++)
        {
            Color color = Color.black;
            for (int j = 0; j < SkinManager.skin_manager.GetCurrentSkin().colors.Length; j++)
            {
                bool cond1 = (SkinManager.skin_manager.GetCurrentSkin().colors[j] != colors[i - 1]);
                if (cond1)
                {
                    color = SkinManager.skin_manager.GetCurrentSkin().colors[j];
                    break;
                }
            }
            colors[i] = color;
        }

        for (int i = 0; i < block_count; i++)
        {
            block_mas[i].SetColor(colors[i]);
        }
    }
}
