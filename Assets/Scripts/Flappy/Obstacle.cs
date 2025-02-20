using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장애물을 제어하는 클래스
public class Obstacle : MonoBehaviour
{   
    // 장애물의 Y축 배치 범위 (최대 높이와 최소 높이)
    public float highPosY = 1f;     // 장애물 상단 위치의 최대값
    public float lowPosY = - 1f;    // 장애물 하단 위치의 최소값

    // 장애물 사이의 크기 (플레이어가 통과할 공간). top과 bottom 사이 공간을 얼마나 가져갈 건지
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    // 장애물의 윗부분과 아랫부분 Transform
    public Transform topObject;     // 상단 장애물 오브젝트
    public Transform bottomObject;  // 하단 장애물 오브젝트

    public float widthPadding = 4f; // 장애물 배치 시 가로 간격 (이전 장애물과의 거리)

    GameManager gameManager;    // 게임 매니저 인스턴스를 저장할 변수

    private void Start()
    {
        gameManager = GameManager.Instance; // 싱글톤 패턴을 사용한 GameManager의 인스턴스를 가져옴
    }

    // 장애물을 랜덤한 위치에 배치하는 함수
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        // holeSize를 랜덤 값으로 설정 (최소 ~ 최대 구멍 크기 사이)
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        // 구멍 크기에 맞게 장애물 위, 아래 위치 조정
        topObject.localPosition = new Vector3(0, halfHoleSize);     // holesize가 나온 거에 반만큼을 위로 올림
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // holesize가 나온 거에 반만큼을 아래로 내림

        // 이전 장애물의 위치에서 widthPadding만큼 오른쪽으로 이동하여 새 장애물 배치
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);

        placePosition.y = Random.Range(lowPosY, highPosY);      // 장애물의 Y축 위치를 랜덤하게 설정

        transform.position = placePosition;     // 장애물의 최종 위치를 설정

        return placePosition;       // 새롭게 배치된 장애물의 위치 반환
    }

    // 플레이어가 장애물을 통과했을 때 점수를 증가
    private void OnTriggerExit2D(Collider2D collision)      // 충돌이 끝났났을 때
    {
        // 플레이어가 맞는지 확인하기 위해 충돌한 객체에서 Player 컴포넌트를 가져옴
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)             // 충돌한 객체가 플레이어라면
            gameManager.AddScore(1);    // 점수 1점 추가
    }
}