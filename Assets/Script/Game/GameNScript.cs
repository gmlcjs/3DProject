using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNScript : MonoBehaviour
{
    // Ʈ���ŷ� ����� Collider�� �ִ��� Ȯ��
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾���� ������ Ȯ�� (�±׳� ��ü������ �ĺ� ����)
        if (other.CompareTag("Player"))
        {
            // �� �ε� (A ������ �̵�)
            SceneManager.LoadScene("GameScene2"); 
        }
    }

    void Update()
    {
        
    }
}
