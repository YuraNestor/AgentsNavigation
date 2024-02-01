public interface IGameManager
{
    public void AddUnit(Unit unit);
    public void RemoveUnit(Unit unit);
    public Player GetPlayerById(int playerId);
    public Player[] GetEnemyPlayersForPlayer(Player player);
    public Player GetPlayer(Unit unit);
}
