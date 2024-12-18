using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    Camera mainCamera;

    float sensitive = 4f; // ���콺 ����
    private float cameraRotationX = 0f; // ī�޶��� ���� ȸ�� ��

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        // ���콺 X�� Y �Է����� ĳ���Ϳ� ī�޶� ȸ�� ó��
        float mouseX = Input.GetAxis("Mouse X") * sensitive * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitive * Time.deltaTime;

        // ĳ������ �¿� ȸ�� ó��
        transform.Rotate(0, mouseX * 100f, 0); // Y�� ȸ�� (�¿�)

        // ī�޶��� ���Ʒ� ȸ�� ó�� (pitch)
        cameraRotationX -= mouseY * 100f;  // ���콺 Y �Է¿� ���� ���Ʒ� ȸ��
        cameraRotationX = Mathf.Clamp(cameraRotationX, -80f, 80f); // ȸ�� ���� ����

        // ī�޶� ���� ȸ�� ����
        mainCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }

}
