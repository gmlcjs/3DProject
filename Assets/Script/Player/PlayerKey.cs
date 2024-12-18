using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    // 캐릭터의 이동 속도
    public float moveSpeed = 5f;

    // 캐릭터의 회전 속도
    public float turnSpeed = 5f;

    // 점프 강도
    public float jumpForce = 5f;

    // 캐릭터가 바닥에 있는지 확인하는 변수
    private bool isGrounded;

    // 점프 횟수를 추적하는 변수 (1단 점프, 2단 점프)
    private int jumpCount = 0;

    // 최대 점프 횟수 (2단 점프 설정)
    public int maxJumpCount = 2;

    // 애니메이터 컴포넌트를 저장할 변수
    Animator m_Animator;

    // Rigidbody 컴포넌트를 저장할 변수 (물리적 속성 제어)
    Rigidbody rb;

    // 캐릭터의 이동 방향을 저장할 변수
    Vector3 m_Movement;

    // 초기 설정
    void Start()
    {
        // 애니메이터 컴포넌트 가져오기
        m_Animator = GetComponent<Animator>();

        // Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();
    }

    // 매 프레임마다 실행되는 함수
    void Update()
    {
        // 사용자 입력을 받아 이동 방향 계산
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 이동 벡터를 계산하여 정규화(방향만 고려)
        m_Movement = new Vector3(horizontal, 0, vertical).normalized;

        // 애니메이션 상태 변경 (달리는 애니메이션)
        m_Animator.SetBool("isRunning", m_Movement != Vector3.zero);

        // 이동 처리
        transform.position += m_Movement * moveSpeed * Time.deltaTime;

        // 이동 방향이 있을 경우 회전 처리
        if (m_Movement != Vector3.zero)
        {
            // 이동 방향에 맞게 회전하기 위한 목표 회전값 계산
            Quaternion toRotation = Quaternion.LookRotation(m_Movement, Vector3.up);

            // 부드럽게 회전하기 위한 보간
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        // 점프 처리
        Jump();
    }

    // 바닥에 닿았을 때 실행되는 함수 (충돌 처리)
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 "Map" 태그를 가진 경우
        if (collision.gameObject.CompareTag("Map"))
        {
            // 점프 횟수 리셋 (바닥에 닿으면 점프 횟수는 0으로 초기화)
            jumpCount = 0;
        }
    }

    // 바닥에서 떨어졌을 때 실행되는 함수 (충돌 종료 처리)
    private void OnCollisionExit(Collision collision)
    {
        // 충돌한 오브젝트가 "Map" 태그를 가진 경우
        if (collision.gameObject.CompareTag("Map"))
        {
            // 바닥에서 떨어졌으므로 isGrounded를 false로 설정
            isGrounded = false;
        }
    }

    // 점프 처리 함수
    private void Jump()
    {
        // 스페이스바를 눌렀을 때 점프 가능한 상태인지 확인
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 바닥에 있거나, 점프 횟수가 최대값보다 적을 경우 점프
            if (isGrounded || jumpCount < maxJumpCount)
            {
                // 위로 힘을 추가하여 점프 처리 (AddForce는 물리적인 힘을 추가)
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                // 점프 횟수 증가
                jumpCount++;
            }
        }
    }

}
