using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class EnemyUnitSpawner : MonoBehaviour
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
    private Unit unit;
    private IGameManager gameManager;
    [Inject]
    private DiContainer container;

    [Inject]
    private void Construct(IGameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    // Start is called before the first frame update
    void Start()
    {
        unit = enemyPrefab.GetComponent<Unit>();
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
            if (gameManager.GetPlayer(unit).units.Count < maxEnemyCount)
            {
                var offset = new Vector3(UnityEngine.Random.Range(0, 2f), 0, UnityEngine.Random.Range(0, 2f));
                var position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)];
                var enemyClone = container.InstantiatePrefab(enemyPrefab, position + offset, Quaternion.identity, enemyParent);
            }
            await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }
}
