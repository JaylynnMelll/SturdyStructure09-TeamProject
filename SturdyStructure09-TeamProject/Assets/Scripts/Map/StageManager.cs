using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Transform mapParent;
    [SerializeField] private MapPoolManager mapPoolManager;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Portal portal;

    private int currentStage = 1;
    private GameObject currentRoom;

    public void Init()
    {
        LoadRoom(currentStage);
    }

    public void LoadRoom(int stage)
    {
        // ���� �� ��ȯ
        if (currentRoom != null)
            mapPoolManager.ReturnRoom(currentRoom);

        RoomType type = (stage % 5 == 0) ? RoomType.Boss : RoomType.Normal;
        currentRoom = mapPoolManager.GetRoom(type);
        currentRoom.transform.position = Vector3.zero;
        currentRoom.transform.SetParent(mapParent);

        // enemy���� ���� �������� ���� ����
        //foreach(var enemy in currentRoom.GetComponentInChildren<EnemyController>())
        //{
        //    enemy.Init(stage);
        //}

        // ��Ż ��Ȱ��ȭ
        portal.gameObject.SetActive(false);

        // ��������Ʈ ����
        portal.OnPlayerEnterPortal = NextStage;
    }

    // Ŭ���� �� ��Ż Ȱ��ȭ
    public void ActivatePortal()
    {
        portal.transform.position = new Vector3(0, 4f, 0);
        portal.gameObject.SetActive(true);
    }

    // ���� ���������� �̵�
    private void NextStage()
    {
        currentStage++;
        LoadRoom(currentStage);
    }

    public int GetCurrentStage() => currentStage;
}
