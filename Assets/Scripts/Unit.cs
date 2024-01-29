using Cysharp.Threading.Tasks;
using ProjectDawn.Navigation.Hybrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Unit : MonoBehaviour
{    
    public int ownerId;
    public float viewRadius;
    public float AttacRange;
    public float lookDelay;
    public LayerMask targetMask;
    public bool killAndDie=true;
    public bool ignorePreTarget = true;
    public GameObject effect;
    public Transform target;    
    protected AgentAuthoring agent;    
    private IUnitsManager unitsManager;

    [Inject]
    private void Construct(IUnitsManager unitsManager)
    {
        this.unitsManager = unitsManager;
    }

    void Start()
    {
        unitsManager.AddUnitWithOwnerId(ownerId);
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
                    FieldOfViewCheck();
                }                    
                agent.SetDestination(target.position);
                TryKillEnemy(target);
            }
            else
            {
                agent.SetDestination(transform.position);
                FieldOfViewCheck();
            }
            await UniTask.Delay(TimeSpan.FromSeconds(lookDelay), cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        Transform closestTarget = null;
        if (rangeChecks.Length != 0)
        {            
            float minDistance = float.MaxValue;            
            foreach (var rangeCheck in rangeChecks)
            {
                var distanceToTarget = Vector3.Distance(transform.position, rangeCheck.transform.position);
                if (minDistance > distanceToTarget)
                {
                    minDistance = distanceToTarget;
                    closestTarget = rangeCheck.transform;
                }
            }
            target = closestTarget;
        }        
    }

    private void OnDestroy()
    {        
        unitsManager.RemoveUnitWithOwnerId(ownerId);
    }

    private void TryKillEnemy(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) <= AttacRange)
        {
            Destroy(target.gameObject);
            var particleEnemy = Instantiate(target.GetComponent<Unit>().effect, transform.position, Quaternion.identity);
            Destroy(particleEnemy, 1);
            if (killAndDie)
            {
                Destroy(gameObject);
                var particle = Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(particle, 1);                
            }                
        }        
    }    
}
