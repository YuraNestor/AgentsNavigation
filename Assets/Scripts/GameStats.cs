using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameStats : MonoBehaviour
{
    [SerializeField]
    private Text textStats;
    private IUnitsManager unitsManager;

    [Inject]
    private void Construct(IUnitsManager unitsManager)
    {
        this.unitsManager = unitsManager;
    }
    
    // Update is called once per frame
    void Update()
    {
        textStats.text = string.Empty;
        var ids = unitsManager.GetAllOwnerIds();
        foreach (var id in ids)
        {
            textStats.text += "Id " + id + "\n";
            textStats.text += "Alive-" + unitsManager.GetCountAliveUnitsWithOwnerId(id) + "\n";
            textStats.text += "Dead-" + unitsManager.GetCountDeadUnitsWithOwnerId(id) + "\n";
        }
    }
}
