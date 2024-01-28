
public interface IUnitsManager
{
    public void AddUnitWithLayer(int layer);
    public void RemoveUnitWithLayer(int layer);
    public int GetCountAliveUnitsWithLayer(int layer);
    public int GetCountUnitsWithLayer(int layer);
    public int[] GetAllLayers();
    public int GetCountDeadUnitsWithLayer(int layer);
}
