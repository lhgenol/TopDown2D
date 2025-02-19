using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;     // 싱글톤 패턴 - 자신을 참조할 수 있는 static 변수

    // 싱글톤 패턴 - static 변수를 외부로 가져갈 수 있는 프로퍼티 하나
    public static GameManager Instance { get { return gameManager; } }  

    private int currentScore = 0;   // 현재 점수 변수 초기화

    UIManager uiManager;    // UI 매니저 접근

    // 외부에서 UI 매니저를 써야할 수도 있기 때문에 get 만들기
    public UIManager UIManager { get { return uiManager;}}

    private void Awake()
    {
        gameManager = this;     // 싱글톤 패턴 - 가장 최초의 객체를 설정해주는 작업
        uiManager = FindObjectOfType<UIManager>();  // UI 매니저 찾아오기
    }

    private void Start()
    {
        uiManager.UpdateScore(0);   // 게임 시작 시 점수를 0으로 만들기
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        uiManager.SetRestart();     // 게임 종료시 SetRestart 호출
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);    // 점수가 증가했을 때 UI 매니저의 UpdateScore로 currentScore를 전달
    }
}   