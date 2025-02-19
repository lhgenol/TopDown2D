using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 스탯(체력, 이동속도 등)을 관리하는 클래스
public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10;    // 체력 변수 (최소 1, 최대 100)

    // 체력 프로퍼티. -값을 가져올 때(get) 현재 체력을 반환, -값을 설정할 때(set) 0~100 사이로 제한 (Clamp 사용)
    public int Health
    {
        get => health;  // 현재 체력 반환
        set => health = Mathf.Clamp(value, 0, 100); // 0~100 범위 내로 값 제한
    }

    [Range(1f, 20f)][SerializeField] private float speed = 3;   // 이동 속도 변수 (최소 1, 최대 20)

    // 이동 속도 프로퍼티. -값을 가져올 때(get) 현재 속도를 반환, -값을 설정할 때(set) 0~20 사이로 제한
    public float Speed
    {
        get => speed;   // 현재 이동 속도 반환
        set => speed = Mathf.Clamp(value, 0, 20);   // 0~20 범위 내로 값 제한
    }
}
