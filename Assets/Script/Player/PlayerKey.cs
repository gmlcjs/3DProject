using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    // ĳ������ �̵� �ӵ�
    public float moveSpeed = 5f;

    // ĳ������ ȸ�� �ӵ�
    public float turnSpeed = 5f;

    // ���� ����
    public float jumpForce = 5f;

    // ĳ���Ͱ� �ٴڿ� �ִ��� Ȯ���ϴ� ����
    private bool isGrounded;

    // ���� Ƚ���� �����ϴ� ���� (1�� ����, 2�� ����)
    private int jumpCount = 0;

    // �ִ� ���� Ƚ�� (2�� ���� ����)
    public int maxJumpCount = 2;

    // �ִϸ����� ������Ʈ�� ������ ����
    Animator m_Animator;

    // Rigidbody ������Ʈ�� ������ ���� (������ �Ӽ� ����)
    Rigidbody rb;

    // ĳ������ �̵� ������ ������ ����
    Vector3 m_Movement;

    // �ʱ� ����
    void Start()
    {
        // �ִϸ����� ������Ʈ ��������
        m_Animator = GetComponent<Animator>();

        // Rigidbody ������Ʈ ��������
        rb = GetComponent<Rigidbody>();
    }

    // �� �����Ӹ��� ����Ǵ� �Լ�
    void Update()
    {
        // ����� �Է��� �޾� �̵� ���� ���
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // �̵� ���͸� ����Ͽ� ����ȭ(���⸸ ���)
        m_Movement = new Vector3(horizontal, 0, vertical).normalized;

        // �ִϸ��̼� ���� ���� (�޸��� �ִϸ��̼�)
        m_Animator.SetBool("isRunning", m_Movement != Vector3.zero);

        // �̵� ó��
        transform.position += m_Movement * moveSpeed * Time.deltaTime;

        // �̵� ������ ���� ��� ȸ�� ó��
        if (m_Movement != Vector3.zero)
        {
            // �̵� ���⿡ �°� ȸ���ϱ� ���� ��ǥ ȸ���� ���
            Quaternion toRotation = Quaternion.LookRotation(m_Movement, Vector3.up);

            // �ε巴�� ȸ���ϱ� ���� ����
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        // ���� ó��
        Jump();
    }

    // �ٴڿ� ����� �� ����Ǵ� �Լ� (�浹 ó��)
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� "Map" �±׸� ���� ���
        if (collision.gameObject.CompareTag("Map"))
        {
            // ���� Ƚ�� ���� (�ٴڿ� ������ ���� Ƚ���� 0���� �ʱ�ȭ)
            jumpCount = 0;
        }
    }

    // �ٴڿ��� �������� �� ����Ǵ� �Լ� (�浹 ���� ó��)
    private void OnCollisionExit(Collision collision)
    {
        // �浹�� ������Ʈ�� "Map" �±׸� ���� ���
        if (collision.gameObject.CompareTag("Map"))
        {
            // �ٴڿ��� ���������Ƿ� isGrounded�� false�� ����
            isGrounded = false;
        }
    }

    // ���� ó�� �Լ�
    private void Jump()
    {
        // �����̽��ٸ� ������ �� ���� ������ �������� Ȯ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �ٴڿ� �ְų�, ���� Ƚ���� �ִ밪���� ���� ��� ����
            if (isGrounded || jumpCount < maxJumpCount)
            {
                // ���� ���� �߰��Ͽ� ���� ó�� (AddForce�� �������� ���� �߰�)
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                // ���� Ƚ�� ����
                jumpCount++;
            }
        }
    }

}
