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
        textStats.text =string.Empty;
        var keys=unitsManager.GetAllLayers();
        foreach (var layer in keys)
        {            
            textStats.text += LayerMask.LayerToName(layer)+"\n";
            textStats.text += "Alive-" + unitsManager.GetCountAliveUnitsWithLayer(layer) + "\n";
            textStats.text += "Dead-" + unitsManager.GetCountDeadUnitsWithLayer(layer) + "\n";
        }
    }
}
