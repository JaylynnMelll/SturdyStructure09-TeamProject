using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// �� Ÿ���� �Ϲ�, ������ ������
public enum RoomType { Normal, Boss }

public class MapPoolManager : MonoBehaviour
{
    [Header("������ ���")]
    [SerializeField] private GameObject[] normalRooms;
    [SerializeField] private GameObject[] bossRooms;

    // �� ������ �������� ���� ť�� ����
    private Dictionary<GameObject, Queue<GameObject>> roomPools = new ();

    // �� ������
    public GameObject GetRoom(RoomType type)
    {
        // Ÿ�Կ� ���� �ҽ� �迭 ����
        GameObject[] sourceArray = (type == RoomType.Normal) ? normalRooms : bossRooms;

        // ������ ������ ����
        GameObject prefab = sourceArray[Random.Range(0, sourceArray.Length)];

        // �ش� ������ ť�� ���ٸ� ����
        if(!roomPools.ContainsKey(prefab))
            roomPools[prefab] = new Queue<GameObject>();

        Queue<GameObject> pool = roomPools[prefab];

        // ť�� ���� ������ ���� ������ ������, ���ٸ� ���� ����
        if(pool.Count > 0)
        {
            GameObject room = pool.Dequeue();
            room.SetActive(true);
            return room;
        }

        else
        {
            return Instantiate(prefab);
        }
    }

    // �� ��ȯ
    public void ReturnRoom(GameObject room)
    {
        room.SetActive(false);
        room.transform.SetParent(transform);

        // ���� EnemyController�� ����(Ǯ��x)
        foreach(Transform child in room.transform)
        {
            if (child.TryGetComponent<EnemyController>(out var enemy))
                Destroy(child.gameObject);
        }

        // ��ȯ�� ������ ť�� ����
        GameObject prefab = FindMatchingPrefab(room);
        if(prefab != null)
        {
            roomPools[prefab].Enqueue(room);
        }
    }

    // �ν��Ͻ� �̸����� ������ ã�� (�̸� ���� ���η� ã��)
    private GameObject FindMatchingPrefab(GameObject roomInstance)
    {
        foreach(var prefab in normalRooms.Concat(bossRooms))
        {
            if (roomInstance.name.Contains(prefab.name))
                return prefab;
        }
        return null;
    }
}
