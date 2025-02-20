using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // 플레이어(타겟)을 따라가는 카메라 제어 클래스
    public Transform target;    // 따라갈 대상 (플레이어 등의 오브젝트)
    float offsetX;              // 초기 플레이어와 카메라 사이의 X축 거리(오프셋)

    void Start()
    {   
        // target이 설정되지 않았다면(=null) 실행하지 않음
        if (target == null)     
            return;

        // 첫 배치 시 초기 카메라 위치와 플레이어 위치의 X축 차이를 계산하여 offsetX에 저장
        // 카메라가 처음 설정된 위치에서 일정 거리만큼 떨어져 있도록 유지하기 위함
        offsetX = transform.position.x - target.position.x; 
    }

    void Update()
    {
        // target이 없으면 더 이상 진행할 필요 없으니 리턴
        if(target == null)
            return;

        // 현재 카메라의 위치를 가져옴
        Vector3 pos = transform.position;   // 내 위치 가져오기

        // 카메라의 X 좌표를 플레이어의 X 좌표 + 초기 오프셋 거리만큼 유지하도록 설정
        pos.x = target.position.x + offsetX;    // 캐릭터 이동을 따라가는데 처음 배치해 놓은 거리만큼 떨어진 상태로 계속 따라가는 것

        // 변경된 위치를 카메라에 적용
        transform.position = pos;
    }
}