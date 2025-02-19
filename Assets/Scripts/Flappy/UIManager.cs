using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   // (인스펙터) 점수 텍스트 가져오기
    public TextMeshProUGUI restartText; // (인스펙터) 재시작 텍스트 가져오기

    void Start()
    {
        if(restartText == null) 
            Debug.Log("restart text is null");

        if(scoreText == null)
        Debug.Log("score text is null");

        // 맨 처음 키자마자는 restart가 필요없기 때문에 restartText의 gameObject SetActive를 false로 해줌. 오브젝트를 끈다는 소리
        restartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        // restartText를 켜주는 작업
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        // 점수를 최신화해서 올려주는 작업
        scoreText.text = score.ToString();
    }
}