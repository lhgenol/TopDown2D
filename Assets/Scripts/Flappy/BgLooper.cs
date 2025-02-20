using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경과 장애물의 위치를 반복적으로 재배치하는 클래스
public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;      // 배경 오브젝트의 개수 (반복적으로 배치될 배경 개수)
    public int obstacleCount = 0;   // 장애물 개수
    public Vector3 obstacleLastPosition = Vector3.zero; // 마지막으로 배치된 장애물의 위치

    void Start()    // 처음에 모든 장애물을 찾고 그 장애물들을 처음놈부터 끝놈까지 랜덤으로 배치해 줌
    {
        // 장애물(Obstacle) 오브젝트를 모두 찾아 배열로 저장
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();   

        // 첫 번째 장애물의 위치를 저장
        obstacleLastPosition = obstacles[0].transform.position;

        // 장애물 개수를 저장
        obstacleCount = obstacles.Length;

        // 모든 장애물을 랜덤 위치로 배치
        for (int i = 0; i < obstacleCount; i++)
        {
            // 장애물을 랜덤하게 배치하고, 그 위치를 obstacleLastPosition에 업데이트
            // 배치가 끝나면 배치한 위치를 받아와서 그 다음 obstacle이 배치될 곳을 전달해 줌
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    // BgLooper랑 충돌(Trigger)하는 장애물들 위치를 뒤로 랜덤 재배치
    private void OnTriggerEnter2D(Collider2D collision) // OnTriggerEnter2D: 실제 물리 충돌이 아닌 충돌에 대한 통보만 해줌
    {
        Debug.Log("Triggerd: " + collision.name);   // 충돌한 오브젝트의 이름을 로그로 출력

        // 충돌한 오브젝트의 태그가 "BackGround"라면
        if(collision.CompareTag("BackGround"))  // 충돌한 물체의 태그가 BackGround라면
        {   
            // 충돌한 배경 오브젝트의 BoxCollider2D를 가져와 가로 길이를 구함
            float widthOfBgObject = ((BoxCollider2D) collision).size.x;
            Vector3 pos = collision.transform.position; // 충돌한 현재 배경 오브젝트의 위치를 가져옴
            pos.x += widthOfBgObject * numBgCount;  // 배경을 numBgCount만큼 뒤쪽으로 이동시켜 반복적인 배경 효과를 만듦
            collision.transform.position = pos; // 새로운 위치를 적용
            return;     // 이미 BackGround를 재배치했다면 더이상 동작하지 않게 return
        }

        // 충돌한 오브젝트가 Obstacle(장애물)인지 확인
        Obstacle obstacle = collision.GetComponent<Obstacle>();

        if (obstacle)   // Obstacle 컴포넌트가 존재하면 (즉, 장애물이라면)
        {
            // Obstacle을 랜덤한 위치로 재배치하고, 마지막 위치를 업데이트
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}