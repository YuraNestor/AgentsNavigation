using Cysharp.Threading.Tasks;
using ProjectDawn.Navigation.Hybrid;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Unit : MonoBehaviour
{
    public int ownerId;
    public bool limitedRadius;
    public float viewRadius;
    public float AttacRange;
    public float lookDelay;
    public bool killAndDie = true;
    public bool ignorePreTarget = true;
    public Transform target;
    public Player player { get; private set; }
    public Action onDie;

    private AgentAuthoring agent;
    private IGameManager gameManager;
    private Player[] enemyPlayers;

    [Inject]
    private void Construct(IGameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void Start()
    {
        player = gameManager.GetPlayerById(ownerId);
        enemyPlayers = gameManager.GetEnemyPlayersForPlayer(player);
        gameManager.AddUnit(this);
        agent = GetComponent<AgentAuthoring>();
        LookForClosestEnemyPeriodically().Forget();
    }

    protected async UniTaskVoid LookForClosestEnemyPeriodically()
    {
        while (true)
        {
            if (target)
            {
                if (ignorePreTarget)
                {
                    LookForClosestTarget();
                }
                agent.SetDestination(target.position);
                TryKillEnemy(target);
            }
            else
            {
                agent.SetDestination(transform.position);
                LookForClosestTarget();
            }
            await UniTask.Delay(TimeSpan.FromSeconds(lookDelay), cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }

    private void LookForClosestTarget()
    {
        List<Unit> targets = new List<Unit>();
        foreach (var enemyPlayer in enemyPlayers)
        {
            targets.AddRange(enemyPlayer.units);
        }
        Transform closestTarget = null;
        if (targets.Count != 0)
        {
            float minDistance = float.MaxValue;
            foreach (var target in targets)
            {
                var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (minDistance > distanceToTarget)
                {
                    minDistance = distanceToTarget;
                    closestTarget = target.transform;
                }
            }
            if (!limitedRadius || minDistance <= viewRadius)
            {
                target = closestTarget;
            }
        }
    }

    public void Die()
    {
        onDie?.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        gameManager.RemoveUnit(this);
    }

    private void TryKillEnemy(Transform target)
    {
        if (target.GetComponent<Unit>().ownerId == ownerId)
        {
            return;
        }
        if (Vector3.Distance(transform.position, target.position) <= AttacRange)
        {
            target.GetComponent<Unit>().Die();
            if (killAndDie)
            {
                Die();
            }
        }
    }
}
