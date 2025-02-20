using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 게임 내 UI를 관리하는 클래스
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   // (인스펙터에서 설정) 점수를 표시하는 UI 텍스트
    public TextMeshProUGUI restartText; // (인스펙터에서 설정) 재시작 안내 문구 UI 텍스트

    void Start()
    {
        // restartText 또는 scoreText가 설정되지 않았을 경우 경고 메시지 출력
        if(restartText == null) 
            Debug.Log("restart text is null");

        if(scoreText == null)
        Debug.Log("score text is null");

        // 게임 시작 시 재시작 텍스트를 숨김 (처음에는 보일 필요가 없음)
        restartText.gameObject.SetActive(false);
    }

    // 게임 오버 시 재시작 안내 문구를 표시하는 함수
    public void SetRestart()
    {
        // restartText를 켜주는 작업
        restartText.gameObject.SetActive(true); // 재시작 텍스트 활성화
    }

    // 현재 점수를 UI에 반영하는 함수
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();  // 숫자를 문자열로 변환하여 UI에 표시
    }
}