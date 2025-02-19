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

    // 플레이어의 입력을 받아 이동 및 방향을 처리하는 메서드
    protected override void HandleAction()
    {
        
    }

    void OnMove(InputValue inputValue)
    {
        // // WASD 입력을 받아서 수평 및 수직 값을 가져옴
        // float horizontal = Input.GetAxisRaw("Horizontal");  // Axis라는 축값을 이용해서 키 입력을 받음
        // float vertical = Input.GetAxisRaw("Vertical");

        movementDirection = inputValue.Get<Vector2>();

        // 방향 벡터를 만들고 nomalized하여 이동 방향 설정
        movementDirection = movementDirection.normalized;
    }


    // void OnFire(InputValue inputValue)
    // {
    //     // UI에 마우스가 올라가 있을 때는 무기를 쏘지 않게 만들어 줌
    //     if(EventSystem.current.IsPointerOverGameObject())   
    //         return;
    // }
}
