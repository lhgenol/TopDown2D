using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// 플레이어의 입력을 받아 이동 및 회전을 처리하는 컨트롤러
public class PlayerController : BaseController
{
    private Camera camera;  // 메인 카메라를 저장할 변수

    protected override void Awake()
    {
        base.Awake(); // 부모(BaseController)의 Awake() 호출
        camera = Camera.main; // 메인 카메라 할당
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();

        // 방향 벡터를 만들고 nomalized하여 이동 방향 설정
        movementDirection = movementDirection.normalized;
    }
}
