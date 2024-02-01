using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public int Id;
    public string playerName;
    public List<Unit> units;
    public int deadUnits { get; private set; }
    public Action onUnitsValueChanged;

    private IGameManager gameManager;

    [Inject]
    private void Construct(IGameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    public void AddUnit(Unit unit)
    {
        units.Add(unit);
        onUnitsValueChanged?.Invoke();
    }

    public void RemoveUnits(Unit unit)
    {
        deadUnits++;
        units.Remove(unit);
        onUnitsValueChanged?.Invoke();
    }
}
