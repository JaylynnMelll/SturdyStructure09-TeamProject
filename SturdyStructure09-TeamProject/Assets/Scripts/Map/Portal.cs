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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // ����� �ݹ��� ������ ���� �� ����(MapManager.NextStage)
            OnPlayerEnterPortal?.Invoke();
        }
    }
}
