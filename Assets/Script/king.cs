using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class king : figure 
{   
    public GameObject Core_object;

    public bool isCheck = false;
    public bool isMate = false;
    public string name = "king";

    public List<move> P_Moves = new List<move>();    
    public List<move> P_Moves_r = new List<move>();   



    public void PossibleMoves(int z, int x) 
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


        if(scriptToAccess.board[0,4].colors_of_figure == 0){    
            if (z == 0 & x == 4)
            {
                if (scriptToAccess.board[0, 2].figure_name == "empty" & scriptToAccess.board[0, 3].figure_name == "empty" & scriptToAccess.board[0, 0].figure_name == "rook")//long
                {
                    z = for_z;
                    x = for_x;


                    move lv = new move();
                    lv.x = x - 2;
                    lv.z = z;
                    P_Moves_r.Add(lv);
                }

                z = for_z;
                x = for_x;

                if (scriptToAccess.board[0, 5].figure_name == "empty" & scriptToAccess.board[0, 6].figure_name == "empty" & scriptToAccess.board[0, 7].figure_name == "rook")//short
                {
                    move sv = new move();
                    sv.x = x + 2;
                    sv.z = z;
                    P_Moves_r.Add(sv);
                }

                z = for_z;
                x = for_x;
            }
        }

        if (scriptToAccess.board[0, 4].colors_of_figure == 1)  
        {
            if (z == 7 & x == 4)
            {
                if (scriptToAccess.board[7, 2].figure_name == "empty" & scriptToAccess.board[7, 3].figure_name == "empty")//long
                {
                    z = for_z;
                    x = for_x;

                    move lv = new move();
                    lv.x = x - 2;
                    lv.z = z;
                    P_Moves_r.Add(lv);
                }

                if (scriptToAccess.board[7, 5].figure_name == "empty" & scriptToAccess.board[7, 6].figure_name == "empty")//short
                {
                    z = for_z;
                    x = for_x;

                    move sv = new move();
                    sv.x = x + 2;
                    sv.z = z;
                    P_Moves_r.Add(sv);
                }
            }
        }

        z = for_z;
        x = for_x;

        move mv = new move();
        mv.x = x + 1;       
        mv.z = z + 1;
        if (mv.x >= 0 & mv.x < 8 & mv.z >= 0 & mv.z < 8)   
        {
            if (scriptToAccess.board[mv.z, mv.x].colors_of_figure != myColor || scriptToAccess.board[mv.z, mv.x].figure_name == "empty")
            {
                P_Moves.Add(mv);
            }

        }

        z = for_z;
        x = for_x;


        move mv1 = new move();
        mv1.x = x + 1;       
        mv1.z = z;
        if (mv1.x >= 0 & mv1.x < 8 & mv1.z >= 0 & mv1.z < 8)   
        {
            if (scriptToAccess.board[mv1.z, mv1.x].colors_of_figure != myColor || scriptToAccess.board[mv1.z, mv1.x].figure_name == "empty")
            {
                P_Moves.Add(mv1);
            }

        }

        z = for_z;
        x = for_x;

        move mv2 = new move();
        mv2.x = x + 1;      
        mv2.z = z - 1;
        if (mv2.x >= 0 & mv2.x < 8 & mv2.z >= 0 & mv2.z < 8)   
        {
            if (scriptToAccess.board[mv2.z, mv2.x].colors_of_figure != myColor || scriptToAccess.board[mv2.z, mv2.x].figure_name == "empty")
            {
                P_Moves.Add(mv2);
            }

        }

        z = for_z;
        x = for_x;

        move mv3 = new move();
        mv3.x = x;      
        mv3.z = z - 1;
        if (mv3.x >= 0 & mv3.x < 8 & mv3.z >= 0 & mv3.z < 8)  
        {
            if (scriptToAccess.board[mv3.z, mv3.x].colors_of_figure != myColor || scriptToAccess.board[mv3.z, mv3.x].figure_name == "empty")
            {
                P_Moves.Add(mv3);
            }

        }

        z = for_z;
        x = for_x;

        move mv4 = new move();
        mv4.x = x - 1;       
        mv4.z = z - 1;
        if (mv4.x >= 0 & mv4.x < 8 & mv4.z >= 0 & mv4.z < 8)  
        {
            if (scriptToAccess.board[mv4.z, mv4.x].colors_of_figure != myColor || scriptToAccess.board[mv4.z, mv4.x].figure_name == "empty")
            {
                P_Moves.Add(mv4);
            }
        }

        z = for_z;
        x = for_x;

        move mv5 = new move();
        mv5.x = x - 1;      
        mv5.z = z;
        if (mv5.x >= 0 & mv5.x < 8 & mv5.z >= 0 & mv5.z < 8)   
        {
            if (scriptToAccess.board[mv5.z, mv5.x].colors_of_figure != myColor || scriptToAccess.board[mv5.z, mv5.x].figure_name == "empty")
            {
                P_Moves.Add(mv5);
            }

        }

        z = for_z;
        x = for_x;

        move mv6 = new move();
        mv6.x = x - 1;       
        mv6.z = z + 1;
        if (mv6.x >= 0 & mv6.x < 8 & mv6.z >= 0 & mv6.z < 8)   
        {
            if (scriptToAccess.board[mv6.z, mv6.x].colors_of_figure != myColor || scriptToAccess.board[mv6.z, mv6.x].figure_name == "empty")
            {
                P_Moves.Add(mv6);
            }

        }

        z = for_z;
        x = for_x;

        move mv7 = new move();
        mv7.x = x;      
        mv7.z = z + 1;
        if (mv7.x >= 0 & mv7.x < 8 & mv7.z >= 0 & mv7.z < 8)   
        {
            if (scriptToAccess.board[mv7.z, mv7.x].colors_of_figure != myColor || scriptToAccess.board[mv7.z, mv7.x].figure_name == "empty")
            {
                P_Moves.Add(mv7);
            }

        }




        for (int i = 0; i < P_Moves.Count; i++)
        {
            P_Moves[i].name = "king";
            P_Moves[i].started_z = for_z;
            P_Moves[i].started_x = for_x;
        }

        for (int i = 0; i < P_Moves_r.Count; i++)
        {
            P_Moves_r[i].name = "king";
            P_Moves_r[i].started_z = for_z;
            P_Moves_r[i].started_x = for_x;
        }

    }
    

}
