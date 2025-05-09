using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// �� Ÿ��
public enum EnemyType { Normal, Boss }

public class EnemyPool : MonoBehaviour
{
    [Header("������ ���")]
    [SerializeField] private GameObject[] normalEnemies;
    [SerializeField] private GameObject[] bossEnemies;

    private Dictionary<GameObject, Queue<GameObject>> enemyPools = new();

    // �� ������
    public GameObject GetEnemy(EnemyType type, Vector3 spawnPos)
    {
        GameObject[] sourceArray = (type == EnemyType.Normal) ? normalEnemies : bossEnemies;

        if (sourceArray.Length == 0)
        {
            Debug.LogWarning($"EnemyType {type}�� ��ϵ� �������� �����ϴ�.");
            return null;
        }

        GameObject prefab = sourceArray[Random.Range(0, sourceArray.Length)];

        if (!enemyPools.ContainsKey(prefab))
            enemyPools[prefab] = new Queue<GameObject>();

        Queue<GameObject> pool = enemyPools[prefab];

        GameObject enemy;

        if (pool.Count > 0)
        {
            enemy = pool.Dequeue();
        }
        else
        {
            enemy = Instantiate(prefab);
        }

        enemy.transform.position = spawnPos;
        enemy.SetActive(true);
        return enemy;
    }

    // �� ��ȯ
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.SetParent(transform);

        GameObject prefab = FindMatchingPrefab(enemy);
        if (prefab != null)
        {
            enemyPools[prefab].Enqueue(enemy);
        }
    }

    // �̸� ��� ������ ã��
    private GameObject FindMatchingPrefab(GameObject enemyInstance)
    {
        foreach (var prefab in normalEnemies.Concat(bossEnemies))
        {
            if (enemyInstance.name.Contains(prefab.name))
                return prefab;
        }
        return null;
    }
}
