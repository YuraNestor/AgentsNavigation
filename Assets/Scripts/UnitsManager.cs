using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitsManager : IUnitsManager
{
    private event Action onDataChanged;
    private List<Player> playerList = new List<Player>();
    private string[] playerNames = { "Blue", "Red" };
    
    public void SubscribeForChanges(Action action)
    {        
        onDataChanged += action;
    }
    public void UnSubscribeForChanges(Action action) 
    { 
        onDataChanged -= action;
    }
    public void AddUnitWithOwnerId(int ownerId)
    {
        var player = playerList.Where(x => x.Id == ownerId).FirstOrDefault<Player>();
        if (player == null)
        {
            player = new Player();
            player.Id = ownerId;
            player.aliveUnits = 1;
            player.totallUnits = 1;
            if(ownerId>=playerNames.Length)
            {
                player.name = "No Name";
            }
            else
            {
                player.name = playerNames[ownerId];
            }
            playerList.Add(player);            
        }
        else
        {
            player.aliveUnits++;
            player.totallUnits++;
        }
        onDataChanged?.Invoke();
    }

    public int GetCountAliveUnitsWithOwnerId(int ownerId)
    {
        var player = playerList.Where(x => x.Id == ownerId).FirstOrDefault<Player>();
        if (player!=null)
        {
            return player.aliveUnits;
        }
        else
        {
            return 0;
        }
    }

    public void RemoveUnitWithOwnerId(int ownerId)
    {
        var player = playerList.Where(x => x.Id == ownerId).FirstOrDefault<Player>();
        if (player != null)
        {
            player.aliveUnits--;
        }
        onDataChanged?.Invoke();
    }

    public int GetCountUnitsWithOwnerId(int ownerId)
    {
        var player = playerList.Where(x => x.Id == ownerId).FirstOrDefault<Player>();
        if (player != null)
        {
            return player.totallUnits;
        }
        else
        {
            return 0;
        }
    }

    public int GetCountDeadUnitsWithOwnerId(int ownerId)
    {
        var player = playerList.Where(x => x.Id == ownerId).FirstOrDefault<Player>();
        if (player != null)
        {
            return player.totallUnits-player.aliveUnits;
        }
        else
        {
            return 0;
        }
    }

    public int[] GetAllOwnerIds()
    {
        return playerList.Select(player => player.Id).ToArray();
    }

    public Player[] GetAllUnitPlayers()
    {
        return playerList.ToArray();
    }
}
