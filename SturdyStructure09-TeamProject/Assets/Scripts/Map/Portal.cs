using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Action OnPlayerEnterPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 태그에만 포탈 작동
        if (collision.CompareTag("Player"))
        {
            // 다음 스테이지 로드
            OnPlayerEnterPortal?.Invoke();
        }
    }
}
