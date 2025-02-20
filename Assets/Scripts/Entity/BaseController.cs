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
    [SerializeField] private Transform weaponPivot;             // 무기 회전을 담당하는 피벗(회전 기준점)
    
    protected Vector2 movementDirection = Vector2.zero;         // 이동하는 방향 지정 (초기값 (0,0))
    public Vector2 MovementDirection { get { return movementDirection; } }  // 이동 방향을 외부에서 읽을 수 있도록 속성 추가

    // protected Vector2 lookDirection = Vector2.zero;             // 캐릭터가 바라보는 방향 지정 (초기값 (0,0))
    // public Vector2 LookDirection { get { return lookDirection; } }  // 바라보는 방향을 외부에서 읽을 수 있도록 속성 추가

    private Vector2 knockback = Vector2.zero;   // 넉백 방향 지정. 피격 시 밀려나는 방향
    private float knockbackDuration = 0.0f;     // 넉백 지속시간

    protected AnimationHandler animationHandler;    // 애니메이션 처리를 담당하는 컴포넌트
    protected StatHandler statHandler;              // 캐릭터의 스탯을 관리하는 컴포넌트

    protected bool isAttacking;                         // 현재 공격 중인지 여부

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
        HandleAction();         // 입력 처리. 이동, 공격 등에 필요한 데이터 처리들이 일어남
        Rotate();  // 캐릭터의 방향을 회전
        HandleAttackDelay();    // 캐릭터의 공격 딜레이
    }

    // 고정된 간격으로 실행 (움직임 처리에 적합)
    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);    // 캐릭터 이동 처리

        if(knockbackDuration > 0.0f)    // 넉백의 지속 시간이 남아 있다면
        {
            knockbackDuration -= Time.fixedDeltaTime;   // 지속 시간을 감소시킴
        }
    }

    // 캐릭터의 행동 처리 (자식 클래스에서 구현)
    protected virtual void HandleAction()
    {

    }

    // 캐릭터의 이동 처리
    private void Movement(Vector2 direction)    
    {
        direction = direction * statHandler.Speed;      // 캐릭터의 속도 적용

        if(knockbackDuration > 0.0f)    // 넉백 지속 시간이 남아있다면. 넉백이 적용 중이라면
        {
            direction *= 0.2f;          // 이동 속도를 감소
            direction += knockback;     // 넉백 방향을 추가
            // 넉백을 적용해야 된다면 기존의 이동 방향의 힘을 줄여주고 넉백에 힘만 넣어주겠다는 뜻
        }

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

    /// <summary>
    /// 넉백 적용. 공격을 맞았을 때 밀려나는 효과 적용 (넉백을 얼마만큼 적용할 건지 방향과 시간 지정)
    /// </summary>
    /// <param name="other">넉백을 발생시킨 대상</param>
    /// <param name="power">넉백 힘</param>
    /// <param name="duration">넉백 지속 시간</param>
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;   // 넉백 지속 시간 설정

        // other(공격자)와 플레이어 사이의 벡터를 구하고, 반대 방향(-)으로 힘을 가함
        knockback = -(other.position - transform.position). normalized * power;
    }

    // 공격 딜레이를 관리해 일정 시간(간격)마다 공격할 수 있도록 처리
    private void HandleAttackDelay()    
    {

    }

    // 공격 실행 (자식 클래스에서 오버라이드 가능)
    protected virtual void Attack()
    {

    }

    // 캐릭터 사망 처리
    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach(SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach(Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);    // 2초 후 객체 제거
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