using UnityEngine;

public class UnitView : MonoBehaviour
{
    public GameObject effect;
    public float effectDuration = 1;

    private Unit unit;
    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
        unit.onDie += OnDieEffect;
    }

    private void OnDieEffect()
    {
        var copyEffect = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(copyEffect, effectDuration);
    }

    private void OnDestroy()
    {
        unit.onDie -= OnDieEffect;
    }
}
