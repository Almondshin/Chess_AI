using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject Core_object;
    public List<move> All_mv = new List<move>(); // AI 이동
    public List<move> P_All_mv = new List<move>(); // Player 이동
    public List<move> Atck_mv = new List<move>();
    public List<move> Move_mv = new List<move>();

    int cost_of_eaten = 0;
    int from_x;
    int from_z;
    int to_x;
    int to_z;


    public int[,] board = new int[8, 8]; // 보드

    private int cost_pawn = 10;    // 각 피스의 가치
    private int cost_knight = 30;
    private int cost_bishop = 30;
    private int cost_rook = 45;
    private int queen = 95;
    private int king = 1000000;

    private int cost_m_pawn = 20;    // value for the promotion of figures
    private int cost_m_knight = 30;
    private int cost_m_bishop = 15;
    private int cost_m_rook = 15;
    private int m_queen = 15;
    private int m_king = 5;

    int cost_f; // 수치값
    int started_cost_f;
    int cost_m; // 이동값
    int started_cost_m;

    bool danger = false; // king이 위험에 처해있는가?

    public List<figure> first = new List<figure>();

    /// <summary>
    /// 이동해야 할 때 호출 됨
    /// </summary>
    public void StartThinking()
    {
        from_z = 0;
        from_x = 0;
        to_z = 0;
        to_x = 0;

        checkForMovement();    // 움직임을 수집한다.
        set_weight_of_board();
        calculate();

    }

    /// <summary>
    /// 값이 변화 할 때 호출됨
    /// </summary>

    public void calculate()
    {
        danger = false;
        cost_of_eaten = 0;

        Core scriptToAccess = Core_object.GetComponent<Core>();

        for (int j = 0; j < P_All_mv.Count; j++)
        {
            if (scriptToAccess.board[P_All_mv[j].z, P_All_mv[j].x].figure_name == "king" & scriptToAccess.board[P_All_mv[j].z, P_All_mv[j].x].colors_of_figure == 1)
            {


                Debug.Log("KingInDanger!!");
                Debug.Log(P_All_mv[j].z);
                Debug.Log(P_All_mv[j].x);
                Debug.Log(P_All_mv[j].started_z);
                Debug.Log(P_All_mv[j].started_x);

                danger = true;

            }
        }
        for (int i = 0; i < All_mv.Count; i++)
        {
            if (danger)
            {

            }
            else
            {
                if (scriptToAccess.board[All_mv[i].z, All_mv[i].x].figure_name != "empty")
                {
                    Atck_mv.Add(All_mv[i]);    // add someone we can schematic into an array
                    Debug.Log("added_to_attack");
                }
                else
                {
                    Move_mv.Add(All_mv[i]);    // 차례
                    Debug.Log("added_to_move");
                }
            }
        }

        if (Atck_mv.Count > 0) 
        {
            for (int i = 0; i < Atck_mv.Count; i++)
            {
                if (scriptToAccess.board[Atck_mv[i].z, Atck_mv[i].x].figure_name == "pawn")
                {
                    if (cost_of_eaten <= cost_pawn)
                    {
                        cost_of_eaten = cost_pawn;
                        from_x = Atck_mv[i].started_x;
                        from_z = Atck_mv[i].started_z;

                        to_x = Atck_mv[i].x;
                        to_z = Atck_mv[i].z;
                    }
                }

                if (scriptToAccess.board[Atck_mv[i].z, Atck_mv[i].x].figure_name == "knight")
                {
                    if (cost_of_eaten <= cost_knight)
                    {
                        cost_of_eaten = cost_knight;
                        from_x = Atck_mv[i].started_x;
                        from_z = Atck_mv[i].started_z;

                        to_x = Atck_mv[i].x;
                        to_z = Atck_mv[i].z;
                    }
                }


                if (scriptToAccess.board[Atck_mv[i].z, Atck_mv[i].x].figure_name == "king")
                {

                    from_x = Atck_mv[i].started_x;
                    from_z = Atck_mv[i].started_z;

                    to_x = Atck_mv[i].x;
                    to_z = Atck_mv[i].z;

                    break;

                }

                if (scriptToAccess.board[Atck_mv[i].z, Atck_mv[i].x].figure_name == "bishop")
                {
                    if (cost_of_eaten <= cost_bishop)
                    {
                        cost_of_eaten = cost_bishop;
                        from_x = Atck_mv[i].started_x;
                        from_z = Atck_mv[i].started_z;

                        to_x = Atck_mv[i].x;
                        to_z = Atck_mv[i].z;
                    }
                }

                if (scriptToAccess.board[Atck_mv[i].z, Atck_mv[i].x].figure_name == "queen")
                {
                    if (cost_of_eaten <= queen)
                    {
                        cost_of_eaten = queen;
                        from_x = Atck_mv[i].started_x;
                        from_z = Atck_mv[i].started_z;

                        to_x = Atck_mv[i].x;
                        to_z = Atck_mv[i].z;
                    }
                }

                if (scriptToAccess.board[Atck_mv[i].z, Atck_mv[i].x].figure_name == "rook")
                {
                    if (cost_of_eaten <= cost_rook)
                    {
                        cost_of_eaten = cost_rook;
                        from_x = Atck_mv[i].started_x;
                        from_z = Atck_mv[i].started_z;

                        to_x = Atck_mv[i].x;
                        to_z = Atck_mv[i].z;
                    }
                }
            }
            Debug.Log("from and to");
            Debug.Log(from_z);
            Debug.Log(from_x);
            Debug.Log(to_z);
            Debug.Log(to_x);
            Debug.Log("trying to eat");
            MOVEForAI(from_z, from_x, to_z, to_x); 

        }

        else  // иначе идем своей дорогой :)
        {

            Debug.Log(All_mv.Count);

            for (int i = 0; i < All_mv.Count; i++)
            {
                started_cost_f = 0;
                started_cost_m = 0;

                if (scriptToAccess.board[Move_mv[i].started_z, Move_mv[i].started_x].figure_name == "pawn")
                {
                    started_cost_m = cost_m_pawn + board[Move_mv[i].z, Move_mv[i].x];
                }

                if (scriptToAccess.board[Move_mv[i].started_z, Move_mv[i].started_x].figure_name == "knight")
                {
                    started_cost_m = cost_m_knight + board[Move_mv[i].z, Move_mv[i].x];
                }

                if (scriptToAccess.board[Move_mv[i].started_z, Move_mv[i].started_x].figure_name == "king")
                {
                    started_cost_m = m_king + board[Move_mv[i].z, Move_mv[i].x];
                }

                if (scriptToAccess.board[Move_mv[i].started_z, Move_mv[i].started_x].figure_name == "bishop")
                {
                    started_cost_m = cost_m_bishop + board[Move_mv[i].z, Move_mv[i].x];
                }

                if (scriptToAccess.board[Move_mv[i].started_z, Move_mv[i].started_x].figure_name == "bishop")
                {
                    started_cost_m = cost_m_bishop + board[Move_mv[i].z, Move_mv[i].x];
                }

                if (scriptToAccess.board[Move_mv[i].started_z, Move_mv[i].started_x].figure_name == "queen")
                {
                    started_cost_m = m_queen + board[Move_mv[i].z, Move_mv[i].x];
                }

                if (scriptToAccess.board[Move_mv[i].started_z, Move_mv[i].started_x].figure_name == "rook")
                {
                    started_cost_m = m_queen + board[Move_mv[i].z, Move_mv[i].x];
                }

                if (cost_m <= started_cost_m)
                {
                    cost_m = started_cost_m;
                    from_x = Move_mv[i].started_x;
                    from_z = Move_mv[i].started_z;

                    to_x = Move_mv[i].x;
                    to_z = Move_mv[i].z;

                }
            }
            Debug.Log("from and to");
            Debug.Log(from_z);
            Debug.Log(from_x);
            Debug.Log(to_z);
            Debug.Log(to_x);


            Debug.Log("trying to move");
            if (from_z == 0 & from_x == 0 & to_z == 0 & to_x == 0)
            {
                Debug.Log("cant do this bro");
                cost_of_eaten = 0;
                from_x = 0;
                from_z = 0;
                to_x = 0;
                to_z = 0;

                cost_f = 0;
                started_cost_f = 0;
                cost_m = 0;
                started_cost_m = 0;

                All_mv.Clear();
                P_All_mv.Clear();
                Atck_mv.Clear();
                Move_mv.Clear();

                StartThinking();


            }
            else
            {
                MOVEForAI(from_z, from_x, to_z, to_x);

            }
        }

        /// <summary>
        /// 보드의 우선순위 결정
        /// </summary>
    }
    public void set_weight_of_board()
    {
        Core scriptToAccess = Core_object.GetComponent<Core>();

        if (scriptToAccess.moves < 5)
        {
            for (int i = 0; i < 8; i++)
            {
                board[7, i] = 10;
                board[6, i] = 10;
                board[5, i] = 20;
                board[4, i] = 25;
                board[3, i] = 10;
                board[2, i] = 10;
                board[1, i] = 10;
                board[0, i] = 10;
            }
        }

        if (scriptToAccess.moves > 5)
        {
            for (int i = 0; i < 8; i++)
            {
                board[7, i] = 10;
                board[6, i] = 10;
                board[5, i] = 20;
                board[4, i] = 20;
                board[3, i] = 30;
                board[2, i] = 40;
                board[1, i] = 50;
                board[0, i] = 60;
            }
        }
        Debug.Log("settled");
    }
    /// <summary>
    /// Assembly of all moves
    /// </summary>

    public void checkForMovement()         
    {
        Core scriptToAccess = Core_object.GetComponent<Core>();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {


                if (scriptToAccess.board[i, j].figure_name == "bishop")
                {
                    if (scriptToAccess.board[i, j].colors_of_figure == 1)
                    {

                        bishop b = new bishop();
                        b.colors_of_figure = 1;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.All_moves.Count; x++)
                        {
                            Debug.Log("bishops");

                            All_mv.Add(b.All_moves[x]);


                        }
                    }
                    else   
                    {
                        bishop b = new bishop();
                        b.colors_of_figure = 0;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.All_moves.Count; x++)
                        {
                        }
                    }
                }

                if (scriptToAccess.board[i, j].figure_name == "king")
                {
                    if (scriptToAccess.board[i, j].colors_of_figure == 1)
                    {

                        king b = new king();
                        b.colors_of_figure = 1;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.P_Moves.Count; x++)
                        {
                            Debug.Log("king");

                            All_mv.Add(b.P_Moves[x]);

                        }
                    }
                    else   
                    {
                        king b = new king();
                        b.colors_of_figure = 1;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.P_Moves.Count; x++)
                        {
                        }
                    }
                }

                if (scriptToAccess.board[i, j].figure_name == "knight")
                {
                    if (scriptToAccess.board[i, j].colors_of_figure == 1)
                    {

                        knight b = new knight();
                        b.colors_of_figure = 1;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.P_Moves.Count; x++)
                        {
                            Debug.Log("knight");
                            Debug.Log(b.P_Moves[x].z);
                            Debug.Log(b.P_Moves[x].x);

                            All_mv.Add(b.P_Moves[x]);
                        }
                    }
                    else
                    {
                        knight b = new knight();
                        b.colors_of_figure = 0;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.P_Moves.Count; x++)
                        {
                        }

                    }
                }

                if (scriptToAccess.board[i, j].figure_name == "pawn")
                {
                    if (scriptToAccess.board[i, j].colors_of_figure == 1)
                    {

                        pawn b = new pawn();
                        b.colors_of_figure = 1;
                        b.PossibleMovesAI(i, j);
                        for (int x = 0; x < b.P_Moves.Count; x++)
                        {
                            Debug.Log("pawn");
                            All_mv.Add(b.P_Moves[x]);

                        }

                        for (int x = 0; x < b.Attack_Moves.Count; x++)
                        {
                            Debug.Log("pawn");
                            All_mv.Add(b.Attack_Moves[x]);

                        }

                    }
                    else
                    {
                        pawn b = new pawn();
                        b.colors_of_figure = 0;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.P_Moves.Count; x++)
                        {
                        }

                        for (int x = 0; x < b.Attack_Moves.Count; x++)
                        {
                        }


                    }

                }

                if (scriptToAccess.board[i, j].figure_name == "queen")
                {
                    if (scriptToAccess.board[i, j].colors_of_figure == 1)
                    {

                        queen b = new queen();
                        b.colors_of_figure = 1;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.All_moves.Count; x++)
                        {
                            Debug.Log("queen");

                            All_mv.Add(b.All_moves[x]);

                        }
                    }
                    else  
                    {
                        queen b = new queen();
                        b.colors_of_figure = 0;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.All_moves.Count; x++)
                        {
                        }
                    }
                }


                if (scriptToAccess.board[i, j].figure_name == "rook")
                {
                    if (scriptToAccess.board[i, j].colors_of_figure == 1)
                    {

                        rook b = new rook();
                        b.colors_of_figure = 1;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.All_moves.Count; x++)
                        {
                            Debug.Log("rook");

                            All_mv.Add(b.All_moves[x]);

                        }
                    }
                    else  
                    {
                        rook b = new rook();
                        b.colors_of_figure = 0;
                        b.PossibleMoves(i, j);
                        for (int x = 0; x < b.All_moves.Count; x++)
                        {
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// 이동 또는 공격 요청
    /// </summary>
    /// <param name="z">초기 좌표 z</param>
    /// <param name="x">초기 좌표 x</param>
    /// <param name="second_z"> 유한 좌표 z</param>
    /// <param name="second_x"> 유한 좌표 x</param>

    public void MOVEForAI(int z, int x, int second_z, int second_x)    // z x 첫 번째 도형의 좌표, s_z s_x 두 번째 도형의 좌표
    {
        All_mv.Clear();
        P_All_mv.Clear();
        Atck_mv.Clear();
        Move_mv.Clear();


        Core scriptToAccess = Core_object.GetComponent<Core>();

        if (scriptToAccess.State == 1)   
        {
            scriptToAccess.y = z;  // 이동하기 위한 첫 번째 활성 도형의 좌표
            scriptToAccess.x = x;
            scriptToAccess.second_y = second_z;    // 이동하기 위한 두 번째 활성 도형의 좌표
            scriptToAccess.second_x = second_x;
            if (scriptToAccess.board[second_z, second_x].colors_of_figure == 0)
            {
                scriptToAccess.AttackFigure();
            }
            else
            {
                scriptToAccess.MoveFigure();
            }

            scriptToAccess.State = 0;  // 1 - AI 0 - 플레이어 이동

            Debug.Log(All_mv.Count);

        }
    }
}
