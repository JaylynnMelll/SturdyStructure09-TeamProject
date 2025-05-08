using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // ����� �θ� ����, �� Ǯ�� ���, �÷��̾� ��ġ
    [SerializeField] private Transform mapParent;
    [SerializeField] private MapPoolManager mapPoolManager;
    [SerializeField] private Transform playerTransform;

    // ��Ż ������
    [SerializeField] private Portal portal;

    // ���� �������� ��ȣ, ���� Ȱ��ȭ �� ��
    [SerializeField] private int currentStage = 1;
    private GameObject currentRoom;

    // �ʱ�ȭ
    public void Init()
    {
        LoadRoom(currentStage);
    }

    // �׽�Ʈ�� awake
    private void Awake()
    {
        LoadRoom(currentStage);
    }

    // �� ���� �� �ʱ�ȭ
    public void LoadRoom(int stage)
    {
        // �÷��̾� ��ġ �ʱ�ȭ
        playerTransform.position = Vector3.zero;

        // ���� ���� �־��ٸ� Ǯ�� ��ȯ
        if (currentRoom != null)
            mapPoolManager.ReturnRoom(currentRoom);

        // ���� ������ �Ǵ�(5�� ���)
        RoomType type = (stage % 5 == 0) ? RoomType.Boss : RoomType.Normal;

        // Ǯ���� �� ���� (������ ����)
        currentRoom = mapPoolManager.GetRoom(type);

        // �� ��ġ�� �θ� ����
        currentRoom.transform.position = Vector3.zero;
        currentRoom.transform.SetParent(mapParent);

        // enemy���� ���� �������� ���� ����
        //foreach(var enemy in currentRoom.GetComponentInChildren<EnemyController>())
        //{
        //    enemy.Init(stage);
        //}

        // ��Ż ��Ȱ��ȭ
        portal.gameObject.SetActive(false);

        // Ŭ���� ���� ���� ����
        StartCoroutine(CheckRoomClear());

        // ��Ż�� ����� �� ȣ���� �Լ�
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

        // Ŭ���� �� ��Ż Ȱ��ȭ
        ActivatePortal();
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

    // ���� �������� �� ��ȯ
    public int GetCurrentStage() => currentStage;
}
