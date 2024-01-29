
public interface IUnitsManager
{
    public void AddUnitWithOwnerId(int ownerId);
    public void RemoveUnitWithOwnerId(int ownerId);
    public int GetCountAliveUnitsWithOwnerId(int ownerId);
    public int GetCountUnitsWithOwnerId(int ownerId);
    public int[] GetAllOwnerIds();
    public int GetCountDeadUnitsWithOwnerId(int ownerId);
}
