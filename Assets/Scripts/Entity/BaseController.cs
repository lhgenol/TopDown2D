using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

// 캐릭터의 기본 컨트롤을 담당하는 클래스
public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;   // 컴포넌트 참조

    [SerializeField] private SpriteRenderer characterRenderer;  // 캐릭터의 스프라이트(이미지)를 제어하기 위한 렌더러
    
    protected Vector2 movementDirection = Vector2.zero;         // 이동하는 방향 지정 (초기값 (0,0))
    public Vector2 MovementDirection { get { return movementDirection; } }  // 이동 방향을 외부에서 읽을 수 있도록 속성 추가

    protected AnimationHandler animationHandler;    // 애니메이션 처리를 담당하는 컴포넌트
    protected StatHandler statHandler;              // 캐릭터의 스탯을 관리하는 컴포넌트

    // 게임 오브젝트가 생성될 때 호출(초기화 작업)
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();   // Rigidbody2D 컴포넌트를 가져와서 변수에 저장
        animationHandler = GetComponent<AnimationHandler>();    // 애니메이션 핸들러 가져오기
        statHandler = GetComponent<StatHandler>();              // 스탯 핸들러 가져오기
    }

    protected virtual void Start()
    {
        // 자식 클래스에서 필요하면 오버라이드해서 사용할 수 있도록 남겨둠
    }

    // 매 프레임마다 실행 (입력 처리, 회전, 공격 딜레이 관리 등)
    protected virtual void Update()
    {
        Rotate();  // 캐릭터의 방향을 회전
    }

    // 고정된 간격으로 실행 (움직임 처리에 적합)
    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);    // 캐릭터 이동 처리
    }

    // 캐릭터의 이동 처리
    private void Movement(Vector2 direction)    
    {
        direction = direction * statHandler.Speed;      // 캐릭터의 속도 적용

        _rigidbody.velocity = direction;    // Rigidbody의 속도를 설정해 이동 처리
        animationHandler.Move(direction);   // 애니메이션에 이동 정보(direction)를 전달해 모션을 변경
    }

    // 캐릭터 회전 처리 (이동 방향을 기준으로 이미지를 반전)
    private void Rotate()
    {
        if (movementDirection.x > 0) 
        {
            characterRenderer.flipX = false; // 오른쪽 이동 시 기본 방향
        }
        else if (movementDirection.x < 0)
        {
            characterRenderer.flipX = true; // 왼쪽 이동 시 이미지 반전
        }
    }

    // NPC와 충돌했을 때 미니게임 씬으로 이동
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 NPC인지 확인
        if (collision.gameObject.CompareTag("NPC"))
        {
            // 미니게임 씬으로 이동
            SceneManager.LoadScene("GameScene"); 
        }
    }
}