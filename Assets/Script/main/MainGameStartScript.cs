using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameStartScript : MonoBehaviour
{
    // 트리거로 사용할 Collider가 있는지 확인
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와의 접촉을 확인 (태그나 객체명으로 식별 가능)
        if (other.CompareTag("Player"))
        {
            // 씬 로딩 (A 씬으로 이동)
            SceneManager.LoadScene("Game"); 
        }
    }

    void Update()
    {
        
    }
}
