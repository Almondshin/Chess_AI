using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rook : figure
{    


    public string name = "rook";


    public List<move> All_moves = new List<move>();

    public List<move> P_Moves_Up = new List<move>();    // 방향에 대한 배열
    public List<move> P_Moves_Down = new List<move>();
    public List<move> P_Moves_Left = new List<move>();
    public List<move> P_Moves_Right = new List<move>();


    public GameObject Core_object;
    public bool Can_add = true;

    public void PossibleMoves(int z, int x) // 28가지의 가능한 움직임
    {
        Core_object = GameObject.Find("Core");
        Core scriptToAccess = Core_object.GetComponent<Core>();
        int for_z;
        int for_x;

        for_z = z;
        for_x = x;

        int myColor = 0;



        for (int i = 1; i < 8; i++)    
        {
            move mv = new move();
            mv.x = x;
            mv.z = z + i;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)    
            {


                if (Can_add)
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name != "empty")
                    {
                        Can_add = false;
                        if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != myColor & scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].figure_name != "empty")
                        {   
                            P_Moves_Up.Add(mv);
                        }
                    }
                }

                if (Can_add)
                {

                    P_Moves_Up.Add(mv);
                }
            }
        }

        z = for_z;
        x = for_x;

        Can_add = true;

        for (int i = 1; i < 8; i++)    
        {
            move mv = new move();
            mv.x = x;
            mv.z = z - i;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)  
            {
                if (Can_add)
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name != "empty")
                    {
                        Can_add = false;
                        if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != myColor & scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].figure_name != "empty")
                        {  
                           
                            P_Moves_Down.Add(mv);
                        }
                    }
                }

                if (Can_add)
                {
                    P_Moves_Down.Add(mv);
                }
            }
        }

        z = for_z;
        x = for_x;

        Can_add = true;

        for (int i = 1; i < 8; i++)    
        {
            move mv = new move();
            mv.x = x - i;
            mv.z = z;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)   
            {
                if (Can_add)
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name != "empty")
                    {
                        Can_add = false;
                        if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != myColor & scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].figure_name != "empty")
                        {  
                            P_Moves_Left.Add(mv);
                        }
                    }
                }

                if (Can_add)
                {
                    P_Moves_Left.Add(mv);
                }
            }
        }

        z = for_z;
        x = for_x;

        Can_add = true;

        for (int i = 1; i < 8; i++)   
        {
            move mv = new move();
            mv.x = x + i;
            mv.z = z;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)   
            {
                if (Can_add)
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name != "empty")
                    {
                        Can_add = false;
                        if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != myColor & scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].figure_name != "empty")
                        {   
                            P_Moves_Right.Add(mv);
                        }
                    }
                }

                if (Can_add)
                {
                    P_Moves_Right.Add(mv);
                }
            }
        }

        z = for_z;
        x = for_x;

        Can_add = true;

        for (int i = 0; i < P_Moves_Up.Count; i++)
        {
            All_moves.Add(P_Moves_Up[i]);
        }
        for (int i = 0; i < P_Moves_Down.Count; i++)
        {
            All_moves.Add(P_Moves_Down[i]);
        }
        for (int i = 0; i < P_Moves_Left.Count; i++)
        {
            All_moves.Add(P_Moves_Left[i]);
        }
        for (int i = 0; i < P_Moves_Right.Count; i++)
        {
            All_moves.Add(P_Moves_Right[i]);
        }


        for (int i = 0; i < All_moves.Count; i++)
        {
            All_moves[i].started_x = for_x;
            All_moves[i].started_z = for_z;
        }

    }

}