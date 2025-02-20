using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이션 상태를 처리하는 클래스
public class AnimationHandler : MonoBehaviour
{
    // 파라미터를 컨트롤할 변수 생성
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    // String값을 Hash라고 하는 특정한 숫자로 변환해 줌. 고유한 숫자로 변환을 해서 사용하는 것

    protected Animator animator;    // 애니메이터 컴포넌트

    // 애니메이터 컴포넌트를 찾는 메서드
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();  // 자식 객체에서 애니메이터 컴포넌트를 찾음
    }

    // 이동 애니메이션을 설정하는 메서드
    public void Move(Vector2 obj)
    {
        // obj.magnitude: 벡터의 크기(이동의 정도)가 0.5 이상이면 움직임 애니메이션을 실행
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }
}
