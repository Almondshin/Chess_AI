using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class figure
{

    // ��� �ǽ��ΰ��

    public int x;  // x(����) 
    public int z; //  z(���ڿ�)
    public int colors_of_figure;   // 0 - player color  1 - enemy color 
    public bool active = false; // �ǽ��� Ȱ�� ��������, �� ����ڰ� �����ߴ��� ����
    public bool isSecondActive = false;
    public string figure_name = "empty";
    public bool first_move = true;
}

