using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField]
    private List<Player> players = new List<Player>();
    private event Action onDataChanged;

    public void AddUnit(Unit unit)
    {
        foreach (Player player in players)
        {
            if (player == unit.player)
            {
                player.AddUnit(unit);
                onDataChanged?.Invoke();
            }
        }
    }

    public void RemoveUnit(Unit unit)
    {
        foreach (Player player in players)
        {
            if (player == unit.player)
            {
                player.RemoveUnits(unit);
                onDataChanged?.Invoke();
            }
        }
    }

    public Player GetPlayerById(int playerId)
    {
        return players.Where(x => x.Id == playerId).FirstOrDefault();
    }

    public Player[] GetEnemyPlayersForPlayer(Player player)
    {
        return players.Where(x => x != player).ToArray();
    }

    public Player GetPlayer(Unit unit)
    {
        return players.Where(x => x.Id == unit.ownerId).FirstOrDefault();
    }
}
