using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    Camera mainCamera;

    float sensitive = 4f; // 마우스 감도
    private float cameraRotationX = 0f; // 카메라의 상하 회전 값

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
        // 마우스 X와 Y 입력으로 캐릭터와 카메라 회전 처리
        float mouseX = Input.GetAxis("Mouse X") * sensitive * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitive * Time.deltaTime;

        // 캐릭터의 좌우 회전 처리
        transform.Rotate(0, mouseX * 100f, 0); // Y축 회전 (좌우)

        // 카메라의 위아래 회전 처리 (pitch)
        cameraRotationX -= mouseY * 100f;  // 마우스 Y 입력에 따라 위아래 회전
        cameraRotationX = Mathf.Clamp(cameraRotationX, -80f, 80f); // 회전 제한 범위

        // 카메라에 상하 회전 적용
        mainCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }

}
