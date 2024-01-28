using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsManager : IUnitsManager
{
    private Dictionary<int,int> aliveUnits=new Dictionary<int, int>();
    private Dictionary<int, int> units = new Dictionary<int, int>();

    public void AddUnitWithLayer(int layer)
    {
        if (aliveUnits.ContainsKey(layer))
        {
            aliveUnits[layer]++;
            units[layer]++;
        }
        else
        {
            aliveUnits.Add(layer, 1);
            units.Add(layer, 1);
        }        
    }

    public int GetCountAliveUnitsWithLayer(int layer)
    {
        
        if (aliveUnits.ContainsKey(layer))
        {
            return aliveUnits[layer];            
        }
        else
        {
            return 0;
        }        
    }

    public void RemoveUnitWithLayer(int layer)
    {
        if (aliveUnits.ContainsKey(layer))
        {
            aliveUnits[layer]--;
        }        
    }
    public int GetCountUnitsWithLayer(int layer)
    {
        if (units.ContainsKey(layer))
        {
            return units[layer];
        }
        else
        {
            return 0;            
        }
    }
    public int GetCountDeadUnitsWithLayer(int layer)
    {
        if (units.ContainsKey(layer))
        {
            return units[layer] - aliveUnits[layer];
        }
        else
        {
            return 0;
        }
    }
    public int[] GetAllLayers()
    {
        return units.Keys.ToArray<int>();
    }
}
