using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;
    private EnemyPool enemyPool;

    public void Init(GameManager gameManager, EnemyPool enemyPool)
    {
        this.gameManager = gameManager;
        this.enemyPool = enemyPool;
    }

    // ���������� ����� ������ ȣ��Ǵ� �� ���� �޼���
    // ���� ��ġ, ���� �������������� ����, �������� ��ȣ�� �޾ƿ´�
    public void SpawnEnemy(List<Transform> spawnPoints, bool isBossStage, int stageNumber)
    {
        EnemyType enemyType = isBossStage ? EnemyType.Boss : EnemyType.Normal;

        // spawnPoint�� �� ����
        foreach (var spawnPoint in spawnPoints)
        {
            Vector3 spawnPos = spawnPoint.position;
            GameObject enemy = enemyPool.GetEnemy(enemyType, spawnPos);

            // �ʱ�ȭ
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.Init(this, gameManager.player.transform);

            // ü�� ���ݷ� ���� ����
        }
    }

    // ����� ���� Ǯ�� ��ȯ
    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        enemyPool.ReturnEnemy(enemy.gameObject);
    }
}
