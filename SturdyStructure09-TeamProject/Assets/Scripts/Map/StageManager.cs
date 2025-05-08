using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 방들의 부모 역할, 방 풀링 담당, 플레이어 위치
    [SerializeField] private Transform mapParent;
    [SerializeField] private MapPoolManager mapPoolManager;
    [SerializeField] private Transform playerTransform;

    // 포탈 프리팹
    [SerializeField] private Portal portal;

    // 현재 스테이지 번호, 현재 활성화 된 방
    [SerializeField] private int currentStage = 1;
    private GameObject currentRoom;

    // 초기화
    public void Init()
    {
        LoadRoom(currentStage);
    }

    // 테스트용 awake
    private void Awake()
    {
        LoadRoom(currentStage);
    }

    // 방 생성 및 초기화
    public void LoadRoom(int stage)
    {
        // 플레이어 위치 초기화
        playerTransform.position = Vector3.zero;

        // 이전 방이 있었다면 풀에 반환
        if (currentRoom != null)
            mapPoolManager.ReturnRoom(currentRoom);

        // 보스 방인지 판단(5의 배수)
        RoomType type = (stage % 5 == 0) ? RoomType.Boss : RoomType.Normal;

        // 풀에서 방 꺼냄 (없으면 생성)
        currentRoom = mapPoolManager.GetRoom(type);

        // 방 위치와 부모 설정
        currentRoom.transform.position = Vector3.zero;
        currentRoom.transform.SetParent(mapParent);

        // enemy에게 현재 스테이지 정보 전달
        //foreach(var enemy in currentRoom.GetComponentInChildren<EnemyController>())
        //{
        //    enemy.Init(stage);
        //}

        // 포탈 비활성화
        portal.gameObject.SetActive(false);

        // 클리어 조건 감시 시작
        StartCoroutine(CheckRoomClear());

        // 포탈에 닿았을 때 호출할 함수
        portal.OnPlayerEnterPortal = NextStage;
    }

    private IEnumerator CheckRoomClear()
    {
        while(true)
        {
            if (GameObject.FindObjectsOfType<EnemyController>().Length == 0)
                break;

            yield return new WaitForSeconds(1f);
        }

        // 클리어 후 포탈 활성화
        ActivatePortal();
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

    // 현재 스테이지 값 반환
    public int GetCurrentStage() => currentStage;
}
