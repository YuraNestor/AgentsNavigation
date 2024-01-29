
using System;

public interface IUnitsManager
{
    public void SubscribeForChanges(Action action);
    public void UnSubscribeForChanges(Action action);
    public void AddUnitWithOwnerId(int ownerId);
    public void RemoveUnitWithOwnerId(int ownerId);
    public int GetCountAliveUnitsWithOwnerId(int ownerId);
    public int GetCountUnitsWithOwnerId(int ownerId);
    public int[] GetAllOwnerIds();
    public int GetCountDeadUnitsWithOwnerId(int ownerId);
    public Player[] GetAllUnitPlayers();
}
