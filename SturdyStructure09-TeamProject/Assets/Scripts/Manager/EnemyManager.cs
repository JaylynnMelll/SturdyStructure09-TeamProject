using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;
    private EnemyPool enemyPool;

    // 남아있는 적의 수
    public int aliveEnemyCount = 0;

    public void Init(GameManager gameManager, EnemyPool enemyPool)
    {
        this.gameManager = gameManager;
        this.enemyPool = enemyPool;
    }

    // 스테이지가 진행될 때마다 호출되는 적 생성 메서드
    // 생성 위치, 보스 스테이지인지의 여부, 스테이지 번호를 받아온다
    public void SpawnEnemy(List<Transform> spawnPoints, bool isBossStage, int stageNumber)
    {
        EnemyType enemyType = isBossStage ? EnemyType.Boss : EnemyType.Normal;

        // spawnPoint에 적 생성
        foreach (var spawnPoint in spawnPoints)
        {
            Vector3 spawnPos = spawnPoint.position;
            GameObject enemy = enemyPool.GetEnemy(enemyType, spawnPos);

            // 초기화
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.Init(this, gameManager.player.transform);

            // 리셋. 스테이지에 맞는 상태로 리셋한다
            enemyController.ResetEnemy(stageNumber);

            aliveEnemyCount++;
        }
        
    }

    // 사망한 적을 풀에 반환
    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        enemyPool.ReturnEnemy(enemy.gameObject);
        aliveEnemyCount--;
    }

    // aliveEnemyCount가 0 이하일 때 true를 반환한다.
    public bool IsAllEnemyCleared() => aliveEnemyCount <= 0;
}