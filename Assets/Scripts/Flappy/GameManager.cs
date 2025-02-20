using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;     // 싱글톤 패턴 - 자기 자신을 참조할 수 있는 static 변수

    // 싱글톤 패턴 - 외부에서 GameManager 인스턴스에 접근할 수 있도록 하는 프로퍼티
    public static GameManager Instance { get { return gameManager; } }  
    private int currentScore = 0;   // 현재 점수 (게임 내에서 점수를 관리하는 변수)
    UIManager uiManager;    // UI 매니저를 참조할 변수
    private bool gameStarted = false;  // 게임 시작 여부 확인

    // UI 매니저를 외부에서 접근할 수 있도록 public 프로퍼티로 제공
    public UIManager UIManager { get { return uiManager;}}

    private void Awake()
    {
        // 싱글톤 패턴 - 게임 매니저 인스턴스가 최초로 생성될 때 자기 자신(객체)을 등록
        gameManager = this;    
        // UI 매니저를 씬에서 찾아서 참조
        uiManager = FindObjectOfType<UIManager>(); 
    }

    private void Start()
    {
        Time.timeScale = 0;  // 게임을 멈춤
        uiManager.UpdateScore(0);   // 게임 시작 시 점수를 0으로 설정하고 UI에 반영
        uiManager.SetStart(true);     // UI 매니저의 재시작 버튼 활성화
    }

    private void Update()
    {
        if (!gameStarted) // 아직 게임이 시작되지 않았다면
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        uiManager.SetStart(false);  // "Click to Start" 문구 숨기기
        Time.timeScale = 1;  // 게임 시작
    }

    public void GameOver()
    {
        uiManager.SetStart(false);     // 게임 오버 시 "Click to Start" 문구 숨기기
        uiManager.SetGameOverUI();     // UI 매니저의 재시작 버튼 활성화
    }

    // 점수를 추가하고 UI에 업데이트
    public void AddScore(int score)
    {
        currentScore += score;  // 현재 점수에 추가 점수를 더함
        uiManager.UpdateScore(currentScore);    // 점수 증가 시 UI 매니저의 UpdateScore로 currentScore를 전달(업데이트)
    }

    public void RestartGame()
    {
        // 현재 활성화된 씬(게임 씬)을 다시 로드하여 게임을 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("SampleScene");  // 메인 씬으로 이동
    }
}   