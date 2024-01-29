using Cysharp.Threading.Tasks;
using ProjectDawn.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Vector3[] spawnPositions;
    [SerializeField]
    private Transform enemyParent;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int spawnCount = 10;
    [SerializeField]
    private float spawnDeley = 0.5f;
    [SerializeField]
    private int maxEnemyCount = 300;

    private IUnitsManager unitsManager;
    [Inject]
    private DiContainer container;

    [Inject]
    private void Construct(IUnitsManager unitsManager)
    {
        this.unitsManager = unitsManager;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyPeriodically().Forget();
    }
    
    private async UniTaskVoid SpawnEnemyPeriodically()
    {
        while (true)
        {
            SpawnEnemys(spawnCount).Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(spawnDeley), cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }

    private async UniTaskVoid SpawnEnemys(int spawnCount)
    {
        if (spawnPositions.Length == 0)
        {
            return;
        }        
        for (int i = 0; i < spawnCount; i++) 
        {
            if (unitsManager.GetCountAliveUnitsWithOwnerId(enemyPrefab.GetComponent<Unit>().ownerId)<maxEnemyCount)
            {
                var position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)];
                var enemyClone = container.InstantiatePrefab(enemyPrefab, position, Quaternion.identity, enemyParent);
            }
            await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }
}
