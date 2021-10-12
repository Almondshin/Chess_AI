using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class figure
{

    // 흰색 피스인경우

    public int x;  // x(숫자) 
    public int z; //  z(문자열)
    public int colors_of_figure;   // 0 - player color  1 - enemy color 
    public bool active = false; // 피스가 활성 상태인지, 즉 사용자가 선택했는지 여부
    public bool isSecondActive = false;
    public string figure_name = "empty";
    public bool first_move = true;
}

