using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;        // 점프하는 힘
    public float forwardSpeed = 3f;     // 정면으로 이동하는 힘
    public bool isDead = false;         // 죽었는지 구분
    float deathCooldown = 0f;           // 일정 시간이 지나고 죽을 수 있게 설정

    bool isFlap = false;                // 점프를 뛰었는지 안뛰었는지 구분

    public bool godMode = false;        // 게임 테스트를 위해 설정

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;             // 클래스명으로 Instance라고 하는 프로퍼티를 접근
        animator = GetComponentInChildren<Animator>();  // Animator 컴포넌트를 가져옴
        _rigidbody = GetComponent<Rigidbody2D>();       // Rigidbody2D 컴포넌트를 가져옴

        if(animator == null)
            Debug.LogError("Not Founded Animator");

        if(_rigidbody == null)
            Debug.LogError("Not Founded Rigidbody");
    }

    // Update is called once per frame
    void Update()
    {
        // 죽었을 때 처리
        if(isDead)
        {
            if(deathCooldown <= 0)
            {
                // 죽으면 게임 재시작
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
        // 죽지 않았을 때 처리
        else
        {
             // Space 키 입력을 받았을 때. 또는 마우스 클릭을 입력 받았을 때
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;  // 점프를 했다
            }
        }
    }

    // 물리 처리
    private void FixedUpdate()
    {
        if (isDead) return;      // isDead가 true라면 아무 작업 하지 않고 리턴

        Vector3 velocity = _rigidbody.velocity;     // rigidbody가 갖고 있는 가속도를 가져옴
        velocity.x = forwardSpeed;      // 계속 똑같은 속도로 이동(가속도를 계속 같은 값을 넣어주고 있기 때문)

        if (isFlap)     // isFlap이 true라면
        {
            velocity.y += flapForce;    // flapForce를 더해줌
            isFlap = false;             // 사용했기 때문에 다시 false로 만듦
        }

        _rigidbody.velocity = velocity;

        // 각도 회전
        float angle = Mathf.Clamp( (_rigidbody.velocity.y * 10), -90, 90);   // 떨어지고 있는지 올라가고 있는지 구분해 각도 조정
        transform.rotation = Quaternion.Euler(0, 0, angle);     // z에 angle값(각도)을 넣어줌
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(godMode) return;     // true일 때 리턴

        if(isDead) return;      // true일 때 리턴

        isDead = true;          // 아니면 isDead를 true로
        deathCooldown = 1f;     // 1초 후 게임 재시작

        animator.SetInteger("IsDie", 1);   // isDie 파라미터가 1이 됐을 때 Die의 모습으로 넘어감
        gameManager.GameOver();
    }
}