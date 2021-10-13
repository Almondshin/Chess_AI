using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
    public GameObject stateOBJ;
    public GameObject AIOBJ;

    public int moves = 0;

    public int State = 0; // 0,1  0 - player turn , 1 - enemy turn
    public int State2 = 1;  // 우리의 색 -- 0 - 흰색 1 - 검은색
    public bool move_updated = false; // for visual updating of all figures
    public bool first_time_updated = false; // move_updated의 보조 변수
    public figure[,] board = new figure[8, 8]; // 보드
    public figure buffer;   // 보조 buffer

    public GameObject[] del;
    public GameObject for_deleting_figure;

    public bool FirstActiveted = false; // 첫 번째 그림이 활성화되었는지 여부

    public int kings = 2; // 킹의 수

    public GameObject cell;     // 추가 시각화를 위한 객체 집합
    public Renderer for_cell;
    private bool black = true; // 셀에 대한 보조 변수
    public GameObject pawn;
    public Renderer pawn_r;
    public GameObject knight;
    public Renderer knight_r;
    public GameObject bishop;
    public Renderer bishop_r;
    public GameObject rook;
    public Renderer rook_r;
    public GameObject queen;
    public Renderer queen_r;
    public GameObject king;
    public Renderer king_r;
    public GameObject empty_obj;
    public Renderer empty_r;

    public GameObject king_alert;

    public int y;    // 활성화된 피스의 위치
    public int x;

    public int second_y;
    public int second_x;


    public Material white_mat;  // 흰색 material
    public Material buff_mat;
    public Material black_mat;  // 검은색 material

    void SetGameSettings() 
    {
        if (State2 == 0)
        {
            Debug.Log("흰색입니다");
            State = 0;
        }
        else
        {
            Debug.Log("검은색입니다");

            buff_mat = white_mat;
            white_mat = black_mat;
            black_mat = buff_mat;
            State = 1;
        }
    }

    void Awake()    
    {
        DontDestroyOnLoad(this.gameObject);

        AIOBJ = GameObject.Find("AI");
        stateOBJ = GameObject.Find("StateMessanger");
        SenderState scriptToAccessOBJ = stateOBJ.GetComponent<SenderState>();
        State2 = scriptToAccessOBJ.color;
        State = scriptToAccessOBJ.color;
        filing_table();


    }

// 보드 채우기
    void filing_table()    
    {
        for (int i = 0; i < 8; i++) // 전체 보드를 빈값으로 채운다.
        {
            for (int j = 0; j < 8; j++)
            {
                empty emp = new empty();
                emp.figure_name = emp.name;
                board[i, j] = emp;

            }
        }

        for (int i = 0; i < 8; i++) // 첫줄은 흰색 폰으로 채우는것
        {
            pawn pn = new pawn();
            pn.colors_of_figure = 0;
            pn.figure_name = pn.name;
            pn.direction = false;
            board[1, i] = pn;
        }


        rook rk = new rook();
        rk.colors_of_figure = 0;    // 흰색 룩
        rk.figure_name = rk.name;
        board[0, 0] = rk;
        board[0, 7] = rk;


        rook rk_black = new rook();
        rk_black.colors_of_figure = 1;    // 검은색 룩
        rk_black.figure_name = rk_black.name;
        board[7, 0] = rk_black;
        board[7, 7] = rk_black;



        knight kn = new knight();
        kn.colors_of_figure = 0;    // 흰색 나이트
        kn.figure_name = kn.name;
        board[0, 1] = kn;
        board[0, 6] = kn;

        knight kn_black = new knight();
        kn_black.colors_of_figure = 1;    // 검은색 나이트
        kn_black.figure_name = kn_black.name;
        board[7, 1] = kn_black;
        board[7, 6] = kn_black;



        bishop bs = new bishop();
        bs.colors_of_figure = 0;    // 흰색 비숍
        bs.figure_name = bs.name;
        board[0, 2] = bs;
        board[0, 5] = bs;

        bishop bs_black = new bishop();
        bs_black.colors_of_figure = 1;    // 검은색 비숍
        bs_black.figure_name = bs_black.name;
        board[7, 2] = bs_black;
        board[7, 5] = bs_black;

        queen q = new queen();   // 흰색 퀸
        q.colors_of_figure = 0;
        q.figure_name = q.name;
        board[0, 3] = q;

        queen q_black = new queen();     // 검은색 비숍
        q_black.colors_of_figure = 1;
        q_black.figure_name = q_black.name;
        board[7, 3] = q_black;

        king kg = new king();
        kg.colors_of_figure = 0;    // 흰색 킹
        kg.figure_name = kg.name;
        board[0, 4] = kg;

        king kg_black = new king();
        kg_black.colors_of_figure = 1;    // 검은색 킹
        kg_black.figure_name = kg_black.name;
        board[7, 4] = kg_black;


        for (int i = 0; i < 8; i++) // 7번째 줄을 검은색 폰으로 채운다.
        {
            pawn pn = new pawn();
            pn.colors_of_figure = 1;
            pn.figure_name = pn.name;
            pn.direction = true;
            board[6, i] = pn;
        }
    }

    // 모든 수치 설정 및 업데이트
    void Start()
    {
        SetGameSettings();
        UpdateFigures(); // 3D modeling에 들어가는 material 



        stateOBJ = GameObject.Find("StateMessanger");
        

        SenderState scriptToAccessOBJ = stateOBJ.GetComponent<SenderState>();
        

        if (scriptToAccessOBJ.color == 1)   // 원래 색이1이면 AI가 움직인다.
        {
            To_AI();
        }
 
    }


    // 3D modeling에 들어가는 material
    public void UpdateFigures()    
    {
        

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (y == 0 || y == 2 || y == 4 || y == 6)
                {
                    if (black)
                    {
                        for_cell.material = black_mat;
                        black = false;
                    }
                    else
                    {
                        for_cell.material = white_mat;
                        black = true;
                    }

                }
                else
                {

                    if (black)
                    {
                        for_cell.material = white_mat;
                        black = false;

                    }
                    else
                    {

                        for_cell.material = black_mat;
                        black = true;
                    }

                }

                Instantiate(cell, new Vector3(x, y, 0), Quaternion.identity);


                if (board[y, x].figure_name == "empty")
                {

                    Instantiate(empty_obj, new Vector3(x, y, 0), transform.rotation); //수정이 필요 하다?

                }

                if (board[y, x].figure_name == "pawn")
                {

                    if (board[y, x].colors_of_figure == 1)
                    {
                        pawn_r.material = black_mat;
                    }
                    else
                    {
                        pawn_r.material = white_mat;
                    }

                    Instantiate(pawn, new Vector3(x, y, 0), transform.rotation);
                    pawn.tag = "Figure";
                }

                if (board[y, x].figure_name == "knight")
                {

                    if (board[y, x].colors_of_figure == 1)
                    {
                        knight_r.material = black_mat;
                    }
                    else
                    {
                        knight_r.material = white_mat;
                    }

                    Instantiate(knight, new Vector3(x, y, 0), transform.rotation);
                    knight.tag = "Figure";
                }

                if (board[y, x].figure_name == "bishop")
                {

                    if (board[y, x].colors_of_figure == 1)
                    {
                        bishop_r.material = black_mat;
                    }
                    else
                    {
                        bishop_r.material = white_mat;
                    }

                    Instantiate(bishop, new Vector3(x, y, 0), transform.rotation);
                    bishop.tag = "Figure";
                }

                if (board[y, x].figure_name == "rook")
                {

                    if (board[y, x].colors_of_figure == 1)
                    {
                        rook_r.material = black_mat;
                    }
                    else
                    {
                        rook_r.material = white_mat;
                    }

                    Instantiate(rook, new Vector3(x, y, 0), transform.rotation);
                    rook.tag = "Figure";
                }

                if (board[y, x].figure_name == "queen")
                {

                    if (board[y, x].colors_of_figure == 1)
                    {
                        queen_r.material = black_mat;
                    }
                    else
                    {
                        queen_r.material = white_mat;
                    }

                    Instantiate(queen, new Vector3(x, y, 0), transform.rotation);
                    queen.tag = "Figure";
                }

                if (board[y, x].figure_name == "king")
                {

                    if (board[y, x].colors_of_figure == 1)
                    {
                        king_r.material = black_mat;
                    }
                    else
                    {
                        king_r.material = white_mat;
                    }

                    Instantiate(king, new Vector3(x, y, 0), transform.rotation);
                    king.tag = "Figure";
                }
            }
        } 
    }

    // 모든 수치 제거
    public void DeleteFigures()   
    {
        del = GameObject.FindGameObjectsWithTag("Figure");
        for (int i = 0; i < del.Length; i++)
        {
            Destroy(del[i]);
        }
    }

    public void MoveFigure()    
    {
        moves++;
        
        if (board[second_y, second_x].figure_name == "empty")    // Just Move
        {
            buffer = board[y, x];
            board[y, x] = board[second_y, second_x];
            board[second_y, second_x] = buffer;
        }

        DeleteFigures();

        UpdateFigures();

        board[y, x].isSecondActive = false;
        board[y, x].active = false;

        board[second_y, second_x].isSecondActive = false;
        board[second_y, second_x].active = false;
       
        if (State == 0)
        {
            To_AI();
        }


    }

    public void RokirovkaMove() 
    {
        
        if (board[y, x].figure_name == "king")
        {
            if (y == 0 & x == 4)
            {
                if(second_y == 0 & second_x == 2){

                    buffer = board[0, 0];
                    board[0, 0] = board[0, 3];
                    board[0, 3] = buffer;
                 
                }
            }
        }


        if (board[y, x].figure_name == "king")
        {
            if (y == 0 & x == 4)
            {
                if (second_y == 0 & second_x == 6)
                {

                    buffer = board[0, 7];
                    board[0, 7] = board[0, 5];
                    board[0, 5] = buffer;

                }
            }
        }


        if (board[y, x].figure_name == "king")
        {
            if (y == 7 & x == 4)
            {
                if (second_y == 7 & second_x == 2)
                {

                    buffer = board[7, 7];
                    board[7, 7] = board[7, 3];
                    board[7, 3] = buffer;

                }
            }
        }


        if (board[y, x].figure_name == "king")
        {
            if (y == 7 & x == 4)
            {
                if (second_y == 7 & second_x == 6)
                {

                    buffer = board[7, 7];
                    board[7, 7] = board[7, 5];
                    board[7, 5] = buffer;

                }
            }
        }

    }


    // Attack Figure
    public void AttackFigure()  
    {
        empty empt = new empty();

        if (board[second_y, second_x].figure_name == "king")
        {
            SceneManager.LoadScene("End");
        }

        board[second_y, second_x] = empt;
        MoveFigure();
    }

    // 만약 2개의 피스가 이미 활성화되어 있고 그것들로 무엇을 해야할지 체크했다면 호출된다.
    // <param name="name"> Figure name </param>
    // <param name="color"> Shape color </param>
    public void isMoveCanBe(string name, int color)   
    {
        if (State == 0)
        {
            if (name == "pawn")
            {
                if (color == 0)
                {
                    pawn pn = new pawn();
                    if (board[y, x].first_move == false)
                    {
                        pn.first_move = false;
                    }

                    pn.PossibleMoves(y, x);

                    for (int i = 0; i < pn.P_Moves.Count; i++)
                    {

                        if (pn.P_Moves[i].z == second_y & pn.P_Moves[i].x == second_x)
                        {
                            board[y, x].first_move = false;
                            MoveFigure();
                        }

                    }
                }
                else // color == 1
                {
                    pawn pn = new pawn();
                    pn.PossibleMoves(y, x);

                    for (int i = 0; i < pn.Attack_Moves.Count; i++)
                    {


                        if (pn.Attack_Moves[i].z == second_y & pn.Attack_Moves[i].x == second_x)
                        {
                            AttackFigure();
                        }
                    }
                }
            }


            if (name == "queen")
            {
                queen q = new queen();

                q.PossibleMoves(y, x);

                for (int i = 0; i < q.P_Moves_Up.Count; i++)
                {
                    if (q.P_Moves_Up[i].z == second_y & q.P_Moves_Up[i].x == second_x)
                    {
                        if (color == 0)
                        {

                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }

                    }
                }

                for (int i = 0; i < q.P_Moves_Down.Count; i++)
                {
                    if (q.P_Moves_Down[i].z == second_y & q.P_Moves_Down[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < q.P_Moves_Left.Count; i++)
                {
                    if (q.P_Moves_Left[i].z == second_y & q.P_Moves_Left[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < q.P_Moves_Right.Count; i++)
                {

                    if (q.P_Moves_Right[i].z == second_y & q.P_Moves_Right[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < q.P_Moves_LeftUp.Count; i++)
                {
                    if (q.P_Moves_LeftUp[i].z == second_y & q.P_Moves_LeftUp[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < q.P_Moves_RightUp.Count; i++)
                {
                    if (q.P_Moves_RightUp[i].z == second_y & q.P_Moves_RightUp[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }


                for (int i = 0; i < q.P_Moves_RightDown.Count; i++)
                {
                    if (q.P_Moves_RightDown[i].z == second_y & q.P_Moves_RightDown[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < q.P_Moves_LeftDown.Count; i++)
                {
                    if (q.P_Moves_LeftDown[i].z == second_y & q.P_Moves_LeftDown[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }
            }

            if (name == "king")
            {
                king k = new king();

                k.PossibleMoves(y, x);

                for (int i = 0; i < k.P_Moves.Count; i++)
                {
                    if (k.P_Moves[i].z == second_y & k.P_Moves[i].x == second_x)
                    {
                        if (color == 0)
                        {

                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < k.P_Moves_r.Count; i++)
                {
                    if (k.P_Moves_r[i].z == second_y & k.P_Moves_r[i].x == second_x)
                    {
                        RokirovkaMove();
                        MoveFigure();
                    }
                }
            }


            if (name == "rook")
            {
                rook rk = new rook();

                rk.PossibleMoves(y, x);

                for (int i = 0; i < rk.P_Moves_Up.Count; i++)
                {
                    if (rk.P_Moves_Up[i].z == second_y & rk.P_Moves_Up[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < rk.P_Moves_Down.Count; i++)
                {
                    if (rk.P_Moves_Down[i].z == second_y & rk.P_Moves_Down[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < rk.P_Moves_Left.Count; i++)
                {
                    if (rk.P_Moves_Left[i].z == second_y & rk.P_Moves_Left[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < rk.P_Moves_Right.Count; i++)
                {

                    if (rk.P_Moves_Right[i].z == second_y & rk.P_Moves_Right[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }
            }


            if (name == "bishop")
            {
                bishop q = new bishop();

                q.PossibleMoves(y, x);

                for (int i = 0; i < q.P_Moves_LeftUp.Count; i++)
                {
                    if (q.P_Moves_LeftUp[i].z == second_y & q.P_Moves_LeftUp[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < q.P_Moves_RightUp.Count; i++)
                {
                    if (q.P_Moves_RightUp[i].z == second_y & q.P_Moves_RightUp[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }


                for (int i = 0; i < q.P_Moves_RightDown.Count; i++)
                {
                    if (q.P_Moves_RightDown[i].z == second_y & q.P_Moves_RightDown[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }

                for (int i = 0; i < q.P_Moves_LeftDown.Count; i++)
                {
                    if (q.P_Moves_LeftDown[i].z == second_y & q.P_Moves_LeftDown[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            MoveFigure();
                        }
                        else
                        {
                            AttackFigure();
                        }
                    }
                }
            }


            if (name == "knight")
            {
                knight q = new knight();

                q.PossibleMoves(y, x);

                for (int i = 0; i < q.P_Moves.Count; i++)
                {
                    if (q.P_Moves[i].z == second_y & q.P_Moves[i].x == second_x)
                    {
                        if (color == 0)
                        {
                            if (color == 0)
                            {
                                MoveFigure();
                            }
                            else
                            {
                                AttackFigure();
                            }
                        }
                        else
                        {
                            if (color == 0)
                            {
                                MoveFigure();
                            }
                            else
                            {
                                AttackFigure();
                            }
                        }
                    }
                }
            }
        }
    }

    public void ActivateFigure(float z, float x)    
    {
        if (State == 0) 
        {
            int first_number = (int)z;
            int second_number = (int)x;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    board[i, j].active = false;

                }
            }

            board[first_number, second_number].active = true;

        }
    }
    public void CheckFirstActive()
    {
        FirstActiveted = false;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {

                if (board[i, j].active == true)
                {
                    FirstActiveted = true;
                }
            }
        }

    }
    
    public void SecondActivateFigure(float z, float x)
    {
        if (State == 0)
        {
            int first_number = (int)z;
            int second_number = (int)x;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    board[i, j].isSecondActive = false;

                }
            }

            board[first_number, second_number].isSecondActive = true;
        }
    }

    public void DebuggingStats(int z, int x)    // 그림의 좌표를 보내고 모든 상태를 확인합니다.
    {
        for (int i = 0; i < 8; i++)
        {
            Debug.Log(board[7, i].figure_name);
        }
    }

    public void setGlobalPlayerColor(int color)
    {
        State2 = color;

    }

    public void To_AI() 
    {
        AI script_AI = AIOBJ.GetComponent<AI>();
        State = 1;
        script_AI.StartThinking();
    }

   
}
    
