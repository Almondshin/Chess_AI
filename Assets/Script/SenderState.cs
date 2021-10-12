using UnityEngine;
using System.Collections;

//색상 선택
public class SenderState : MonoBehaviour 
{
    public int color; // 0 or 1
    public GameObject AIOBJ;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Here you can set the color from the main menu
    // Need color 0 or 1 (white and black respectively)
    public void SetColor(int colorr) // 메인 메뉴에서 색상을 설정할 수 있습니다. 메뉴 표시줄로 스크립트를 확인합니다.
    {
        color = colorr;
    }
       
}
