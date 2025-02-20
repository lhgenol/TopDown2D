using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 캐릭터의 움직임 및 충돌 처리를 담당하는 클래스
public class Player : MonoBehaviour
{
    Animator animator;      // 애니메이션을 제어할 Animator 컴포넌트
    Rigidbody2D _rigidbody; // 물리 연산을 위한 Rigidbody2D 컴포넌트

    public float flapForce = 6f;        // 점프하는 힘
    public float forwardSpeed = 3f;     // 플레이어가 앞으로 나아가는 속도
    public bool isDead = false;         // 플레이어가 죽었는지 여부
    float deathCooldown = 0f;           // 사망 후 일정 시간이 지난 뒤 재시작 가능하도록 설정

    bool isFlap = false;                // 점프 입력 여부 확인

    public bool godMode = false;        // 게임 테스트를 위해 설정

    GameManager gameManager;            // 게임을 관리하는 GameManager 참조

    void Start()
    {
        gameManager = GameManager.Instance;             // 싱글톤 GameManager 인스턴스 가져오기
        animator = GetComponentInChildren<Animator>();  // 자식 오브젝트에서 Animator 컴포넌트 찾기
        _rigidbody = GetComponent<Rigidbody2D>();       // Rigidbody2D 컴포넌트 찾기
    }

    void Update()
    {
        // 플레이어가 사망했다면
        if(isDead)
        {
            if(deathCooldown <= 0)
            {
                // 죽었을 때 스페이스바 또는 마우스 클릭 시 게임 재시작
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;    // 죽지 않으면 시간 감소
            }
        }
        // 죽지 않았다면
        else
        {
             // 점프 입력 처리. Space 키 입력을 받았을 때 또는 마우스 클릭을 입력 받았을 때
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;  // 점프 입력을 받음
            }
        }
    }

    // 물리 처리 (FixedUpdate는 물리 연산이 처리되는 주기에 맞춰 실행됨)
    private void FixedUpdate()
    {
        if (isDead) return;      // 플레이어가 죽었다면 아무 작업도 하지 않고 리턴

        Vector3 velocity = _rigidbody.velocity;     // 현재 rigidbody가 갖고 있는 속도를 가져옴
        velocity.x = forwardSpeed;      // 플레이어를 일정한 속도로 앞으로 이동

        if (isFlap)     // 점프 입력이 있었다면 (isFlap이 true라면)
        {
            velocity.y += flapForce;    // 위쪽으로 힘을 가함
            isFlap = false;             // 사용했기 때문에 다시 false로 만듦 (점프 입력 초기화)
        }

        _rigidbody.velocity = velocity; // 변경된 속도를 적용

        // 캐릭터의 회전 (각도 조정)
        float angle = Mathf.Clamp( (_rigidbody.velocity.y * 10), -90, 90);   // 속도에 따라 회전 각도를 제한
        transform.rotation = Quaternion.Euler(0, 0, angle);     // z축을 기준으로 회전 적용
    }

    // 충돌 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(godMode) return;     // 무적 모드가 활성화되었으면 충돌 무시
        if(isDead) return;      // 이미 죽었다면 추가 처리 없이 리턴

        isDead = true;          // 아니면 isDead를 true로 (사망 상태로 변경)
        deathCooldown = 1f;     // 1초 후 게임 재시작 가능하도록 설정

        animator.SetInteger("IsDie", 1);   // isDie 파라미터가 1이 됐을 때 애니메이션에서 사망 상태로 변경
        gameManager.GameOver();     // 게임 오버 처리 호출
    }
}