using UnityEngine;
using UnityEngine.UI;

public class UPlayer : MonoBehaviour
{
    public Player player;
    public Text infoText;

    private int aliveUnits;
    private int deadUnits;

    private void OnEnable()
    {
        player.onUnitsValueChanged += ShowStats;
    }

    private void ShowStats()
    {
        aliveUnits = player.units.Count;
        deadUnits = player.deadUnits;
        infoText.text = player.playerName;
        infoText.text += "\n" + "Alive units-" + aliveUnits + "\n" + "Dead units-" + deadUnits;
    }

    private void OnDisable()
    {
        player.onUnitsValueChanged -= ShowStats;
    }
}
