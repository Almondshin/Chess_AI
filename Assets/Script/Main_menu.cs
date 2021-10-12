using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Main_menu : MonoBehaviour  //버튼에 넣는 스크립트
{

    public GameObject _ui_start;    //Start_ui
    public GameObject _ui_exit;     //Exit_ui

    public GameObject _ui_play;     //Play_ui
    public GameObject _ui_back;     //Back_ui

    // public GameObject _ui_play_black; // start the game for black
    public GameObject _ui_play_white; // start the game for white

    public GameObject stateOBJ;


    void Awake()
    {
        stateOBJ = GameObject.Find("StateMessanger");
    }

   public void Exit_Game() //onclick function에 추가되는 항목
    {
        Application.Quit();
    }

   public void Start_Game() //onclick function에 추가되는 항목
    {
       
       _ui_start.SetActive(false);
       _ui_exit.SetActive(false);

       _ui_back.SetActive(true);
       //_ui_play_black.SetActive(true);
       _ui_play_white.SetActive(true);
   }

   public void Play_Game() //onclick function에 추가되는 항목
    {
        SceneManager.LoadScene("TEST_board");
   }

   public void Back_to_menu() //onclick function에 추가되는 항목
    {
       _ui_start.SetActive(true);
       _ui_exit.SetActive(true);

       _ui_play.SetActive(false);
       _ui_back.SetActive(false);
       // _ui_play_black.SetActive(false);
       _ui_play_white.SetActive(false);
   }

   public void PlayAsBlack() //onclick function에 추가되는 항목
    {

       SenderState scriptToAccess = stateOBJ.GetComponent<SenderState>();
       scriptToAccess.SetColor(1);
       Debug.Log("Color setted is 1");
       SceneManager.LoadScene("TEST_board"); //빌드되어있지 않으면 로드하지 않는다


    }

   public void PlayAsWhite() //onclick function에 추가되는 항목
    {
       SenderState scriptToAccess = stateOBJ.GetComponent<SenderState>();
       scriptToAccess.SetColor(0);
       Debug.Log("Color setted is 0");
       SceneManager.LoadScene("TEST_board"); //빌드되어있지 않으면 로드하지 않는다


    }

}
