﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bishop : figure
{  

    public string name = "bishop";

    public GameObject Core_object;



    public bool Can_add = true;

    public List<move> All_moves = new List<move>();

    public List<move> P_Moves_LeftUp = new List<move>();    
    public List<move> P_Moves_RightUp = new List<move>();
    public List<move> P_Moves_LeftDown = new List<move>();
    public List<move> P_Moves_RightDown = new List<move>();

    public void PossibleMoves(int z, int x) 
    {
        Core_object = GameObject.Find("Core");
        Core scriptToAccess = Core_object.GetComponent<Core>();

        int myColor = 0;


        int for_z;
        int for_x;

        for_z = z;
        for_x = x;

        for (int i = 1; i < 8; i++)    // RightUp
        {
            move mv = new move();
            mv.x = x + i;
            mv.z = z + i;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)    
            {


                if (Can_add)
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name != "empty")
                    {
                        Can_add = false;
                        if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != myColor)
                        {   
                            P_Moves_RightUp.Add(mv);
                        }
                    }
                }

                if (Can_add)
                {
                    P_Moves_RightUp.Add(mv);
                }
            }
        }

        z = for_z;
        x = for_x;

        Can_add = true;

        for (int i = 1; i < 8; i++)    // RightDown
        {
            move mv = new move();
            mv.x = x + i;
            mv.z = z - i;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)     
            {


                if (Can_add)
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name != "empty")
                    {
                        Can_add = false;
                        if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != myColor)
                        {   
                            P_Moves_RightDown.Add(mv);
                        }
                    }
                }

                if (Can_add)
                {
                    P_Moves_RightDown.Add(mv);
                }
            }
        }

        z = for_z;
        x = for_x;

        Can_add = true;

        for (int i = 1; i < 8; i++)    // LeftDown
        {
            move mv = new move();
            mv.x = x - i;
            mv.z = z - i;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)    
            {


                if (Can_add)
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name != "empty")
                    {
                        Can_add = false;
                        if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != myColor)
                        {   
                            P_Moves_LeftDown.Add(mv);
                        }
                    }
                }

                if (Can_add)
                {
                    P_Moves_LeftDown.Add(mv);
                }
            }
        }

        z = for_z;
        x = for_x;

        Can_add = true;

        for (int i = 1; i < 8; i++)    // LefUp
        {
            move mv = new move();
            mv.x = x - i;
            mv.z = z + i;
            if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)    
            {


                if (Can_add)
                {
                    if (scriptToAccess.board[mv.z, mv.x].figure_name != "empty")
                    {
                        Can_add = false;
                        if (scriptToAccess.board[scriptToAccess.second_y, scriptToAccess.second_x].colors_of_figure != myColor)
                        {   
                            P_Moves_LeftUp.Add(mv);
                        }
                    }
                }

                if (Can_add)
                {
                    P_Moves_LeftUp.Add(mv);
                }
            }
        }

       
        z = for_z;
        x = for_x;

        Can_add = true;

        for (int i = 0; i < P_Moves_LeftUp.Count; i++)
        {
            All_moves.Add(P_Moves_LeftUp[i]);
        }

        z = for_z;
        x = for_x;

        for (int i = 0; i < P_Moves_LeftDown.Count; i++)
        {
            All_moves.Add(P_Moves_LeftDown[i]);
        }

        z = for_z;
        x = for_x;

        for (int i = 0; i < P_Moves_RightDown.Count; i++)
        {
            All_moves.Add(P_Moves_RightDown[i]);
        }

        z = for_z;
        x = for_x;

        for (int i = 0; i < P_Moves_RightUp.Count; i++)
        {
            All_moves.Add(P_Moves_RightUp[i]);
        }

        z = for_z;
        x = for_x;


        for (int i = 0; i < All_moves.Count; i++)
        {
            All_moves[i].name = "bishop";
            All_moves[i].started_z = for_z;
            All_moves[i].started_x = for_x;

        }

    }
}
