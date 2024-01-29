using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitsManager : IUnitsManager
{
    private Dictionary<int, int> aliveUnits = new Dictionary<int, int>();
    private Dictionary<int, int> units = new Dictionary<int, int>();    
    private List<Player> playerList = new List<Player>();

    public void AddUnitWithOwnerId(int ownerId)
    {
        var player = playerList.Where(x => x.Id == ownerId).FirstOrDefault<Player>();
        if (player == null)
        {
            player = new Player();
            player.Id = ownerId;
            player.aliveUnits = 1;
            player.totallUnits = 1;
            playerList.Add(player);            
        }
        else
        {
            player.aliveUnits++;
            player.totallUnits++;
        }        
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
}
