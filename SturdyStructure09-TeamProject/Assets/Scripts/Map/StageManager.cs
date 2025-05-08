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
        // 현재 방 반환
        if (currentRoom != null)
            mapPoolManager.ReturnRoom(currentRoom);

        RoomType type = (stage % 5 == 0) ? RoomType.Boss : RoomType.Normal;
        currentRoom = mapPoolManager.GetRoom(type);
        currentRoom.transform.position = Vector3.zero;
        currentRoom.transform.SetParent(mapParent);

        // enemy에게 현재 스테이지 정보 전달
        //foreach(var enemy in currentRoom.GetComponentInChildren<EnemyController>())
        //{
        //    enemy.Init(stage);
        //}

        // 포탈 비활성화
        portal.gameObject.SetActive(false);

        // 델리게이트 연결
        portal.OnPlayerEnterPortal = NextStage;
    }

    // 클리어 시 포탈 활성화
    public void ActivatePortal()
    {
        portal.transform.position = new Vector3(0, 4f, 0);
        portal.gameObject.SetActive(true);
    }

    // 다음 스테이지로 이동
    private void NextStage()
    {
        currentStage++;
        LoadRoom(currentStage);
    }

    public int GetCurrentStage() => currentStage;
}
