using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 방 타입을 일반, 보스로 나눈다
public enum RoomType { Normal, Boss }

public class MapPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] normalRooms;
    [SerializeField] private GameObject[] bossRooms;

    private Dictionary<GameObject, Queue<GameObject>> roomPools = new Dictionary<GameObject, Queue<GameObject>>();

    public GameObject GetRoom(RoomType type)
    {
        GameObject[] sourceArray = (type == RoomType.Normal) ? normalRooms : bossRooms;
        GameObject prefab = sourceArray[UnityEngine.Random.Range(0, sourceArray.Length)];

        if(!roomPools.ContainsKey(prefab))
            roomPools[prefab] = new Queue<GameObject>();

        Queue<GameObject> pool = roomPools[prefab];

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

    public void ReturnRoom(GameObject room)
    {
        room.SetActive(false);
        room.transform.SetParent(transform);

        foreach(Transform child in room.transform)
        {
            if (child.TryGetComponent<EnemyController>(out var enemy))
                Destroy(child.gameObject);
        }

        GameObject prefab = FindMatchingPrefab(room);
        if(prefab != null)
        {
            roomPools[prefab].Enqueue(room);
        }
    }

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
