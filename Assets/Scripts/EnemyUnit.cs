using ProjectDawn.Navigation.Hybrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    // Start is called before the first frame update
    public static int enemyCounter {get; private set;}
    void Start()
    {
        enemyCounter++;
        agent = GetComponent<AgentAuthoring>();
        LookForClosestEnemyPeriodically().Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        enemyCounter--;
    }
}
