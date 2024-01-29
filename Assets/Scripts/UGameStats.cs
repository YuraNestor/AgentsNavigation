using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UGameStats : MonoBehaviour
{
    [SerializeField]
    private Text textStats;

    private IUnitsManager unitsManager;

    [Inject]
    private void Construct(IUnitsManager unitsManager)
    {
        this.unitsManager = unitsManager;
    }

    private void OnEnable()
    {
        unitsManager.SubscribeForChanges(ShowStats);
    }

    private void OnDisable()
    {
        unitsManager.UnSubscribeForChanges(ShowStats);
    }    

    private void ShowStats()
    {
        textStats.text = string.Empty;
        var unitPlayers = unitsManager.GetAllUnitPlayers();
        foreach (var unitPlayer in unitPlayers)
        {
            textStats.text += unitPlayer.name + "\n";
            textStats.text += "Alive-" + unitPlayer.aliveUnits + "\n";
            textStats.text += "Dead-" + (unitPlayer.totallUnits - unitPlayer.aliveUnits) + "\n";
        }
    }
}
