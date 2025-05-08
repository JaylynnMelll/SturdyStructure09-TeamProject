using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Action OnPlayerEnterPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� �±׿��� ��Ż �۵�
        if (collision.CompareTag("Player"))
        {
            // ���� �������� �ε�
            OnPlayerEnterPortal?.Invoke();
        }
    }
}
