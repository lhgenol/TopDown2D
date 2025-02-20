using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 게임 내 UI를 관리하는 클래스
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   // (인스펙터에서 설정) 점수를 표시하는 UI 텍스트
    public TextMeshProUGUI gameOverText; // (인스펙터에서 설정) 재시작 안내 문구 UI 텍스트
    public TextMeshProUGUI startText; // (인스펙터에서 설정) 최초 시작 안내 문구 UI 텍스트
    public Button restartButton;    // 재시작 버튼
    public Button mainButton;       // 메인 버튼

    void Start()
    {   
        startText.gameObject.SetActive(true);       // 시작 텍스트만 보여주기
        
        // 버튼 클릭 리스너 추가
        restartButton.onClick.AddListener(OnRestartClicked);
        mainButton.onClick.AddListener(OnMainClicked);
    }

    // 게임 오버 시 재시작 안내 문구를 표시하는 함수
    public void SetStart(bool show)
    {
        startText.gameObject.SetActive(show);  // 시작 텍스트 활성화
    }

    // 게임 오버 시 게임 오버 문구 및 버튼 표시하는 함수
    public void SetGameOverUI()
    {
        startText.gameObject.SetActive(false);      // 시작 문구 숨기기
        gameOverText.gameObject.SetActive(true);    // 게임 오버 문구 활성화
        restartButton.gameObject.SetActive(true);   // 재시작 버튼 활성화
        mainButton.gameObject.SetActive(true);      // 메인 버튼 활성화
    }

    // 현재 점수를 UI에 반영하는 함수
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();  // 숫자를 문자열로 변환하여 UI에 표시
    }

    // 재시작 버튼 클릭 시 호출되는 함수
    private void OnRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    // 메인 버튼 클릭 시 호출되는 함수
    private void OnMainClicked()
    {
        GameManager.Instance.GoToMenu();
    }
}