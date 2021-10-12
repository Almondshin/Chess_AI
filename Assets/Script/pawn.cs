using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pawn : figure
{   

    public bool direction; // true - white to black else reverse
    public string name = "pawn";
    // public bool first_move = true; // 폰이 1칸씩 이동가능하다
    public GameObject Core_object;

    public List<move> P_Moves = new List<move>();   // 이동이 가능한 수치
    public List<move> Attack_Moves = new List<move>();   

    public int for_z;
    public int for_x;


    public void PossibleMoves(int z, int x) // 28가지의 가능한 움직임
    {
        for_z = z;
        for_x = x;


        Core_object = GameObject.Find("Core");
        Core scriptToAccess = Core_object.GetComponent<Core>();


        if (scriptToAccess.State == 0)
        {
            move mv = new move();

            mv.x = x;      // 이동
            mv.z = z + 1;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)  // line for limiting the speed at 8x8 borders
            {
                if (scriptToAccess.board[mv.z, mv.x].figure_name == "empty")
                {
                    P_Moves.Add(mv);
                }
            }

            z = for_z;
            x = for_x;



            move mv1 = new move();

            mv1.x = x + 1;  // 공격
            mv1.z = z + 1;
            if (mv1.x >= 0 & mv1.x < 8 & mv1.z >= 0 & mv1.z < 8)   // line for limiting the speed at 8x8 borders
            {
                if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != 0 & scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].figure_name != "empty")
                {
                    Attack_Moves.Add(mv1);
                }
            }

            z = for_z;
            x = for_x;


            move mv2 = new move();

            mv2.x = x - 1;  // 공격
            mv2.z = z + 1;
            if (mv2.x >= 0 & mv2.x < 8 & mv2.z >= 0 & mv2.z < 8)   // line for limiting the speed at 8x8 borders
            {

                if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != 0 & scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].figure_name != "empty")
                {
                    Attack_Moves.Add(mv2);
                }
            }

            z = for_z;
            x = for_x;

    

            move mv3 = new move();

            if (first_move)
            {
                mv3.x = x;  // 이동
                mv3.z = z + 2;
                if (mv3.x >= 0 & mv3.x < 8 & mv3.z >= 0 & mv3.z < 8)   // line for limiting the speed at 8x8 borders
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name == "empty")
                    {
                        P_Moves.Add(mv3);
                    }
                }
                z = for_z;
                x = for_x;
            }
        }


        for (int i = 0; i < P_Moves.Count; i++)
        {
            P_Moves[i].name = "pawn";
        }

        for (int i = 0; i < Attack_Moves.Count; i++)
        {
            Attack_Moves[i].name = "pawn";
        }
        

    }
        


    public void PossibleMovesAI(int z, int x)
    {


        int for_z, for_x;
        for_z = z;
        for_x = x;

        Core_object = GameObject.Find("Core");
        Core scriptToAccess = Core_object.GetComponent<Core>();

        int myColor = 0;

        if (scriptToAccess.State == 1)
        {

            myColor = 1;
        }
        else
        {
            myColor = 0;
        }

        if (scriptToAccess.State == 1)
        {

            move mv4 = new move();
            mv4.x = x;      
            mv4.z = z - 1;
            if (mv4.x >= 0 & mv4.x < 8 & mv4.z >= 0 & mv4.z < 8)   
            {
                if (scriptToAccess.board[mv4.z, mv4.x].figure_name == "empty")
                {
                    P_Moves.Add(mv4);
                }
            }

            z = for_z;
            x = for_x;

            move mv5 = new move();

            mv5.x = x + 1;  
            mv5.z = z - 1;
            if (mv5.x >= 0 & mv5.x < 8 & mv5.z >= 0 & mv5.z < 8)   
            {
                if (scriptToAccess.board[mv5.z, mv5.x].colors_of_figure != myColor & scriptToAccess.board[mv5.z, mv5.x].figure_name != "empty")
                {
                    if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != 1)
                    {
                        Attack_Moves.Add(mv5);
                        Debug.Log("ATTAPAWN");
                    }
                }
            }

            z = for_z;
            x = for_x;

            move mv6 = new move();

            mv6.x = x - 1;  
            mv6.z = z - 1;
            if (mv6.x >= 0 & mv6.x < 8 & mv6.z >= 0 & mv6.z < 8)   
            {
                if (scriptToAccess.board[mv6.z, mv6.x].colors_of_figure != myColor & scriptToAccess.board[mv6.z, mv6.x].figure_name != "empty")
                {
                    if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != 1)
                    {
                        Attack_Moves.Add(mv6);
                        Debug.Log("ATTAPAWN");
                    }
                }


                z = for_z;
                x = for_x;

                if (first_move)
                {
                    move mv7 = new move();

                    mv7.x = x;  
                    mv7.z = z - 2;
                    if (mv7.x >= 0 & mv7.x < 8 & mv7.z >= 0 & mv7.z < 8)    
                    {
                        if (mv7.z == 6)
                        {
                            P_Moves.Add(mv7);
                        }
                    }
                }

                z = for_z;
                x = for_x;


            }


            for (int i = 0; i < P_Moves.Count; i++)
            {
                P_Moves[i].name = "pawn";
                P_Moves[i].started_z = for_z;
                P_Moves[i].started_x = for_x;
            }

            for (int i = 0; i < Attack_Moves.Count; i++)
            {
                Attack_Moves[i].name = "pawn";
                Attack_Moves[i].started_z = for_z;
                Attack_Moves[i].started_x = for_x;
                
            }
            Debug.Log("ATTACOUNT");
            Debug.Log(Attack_Moves.Count);
        }
    }
}
