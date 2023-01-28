using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardController : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text lifeText;
    [SerializeField] private Text moveText;
    [SerializeField] private Text rangeText;
    [SerializeField] private Text attackText;
    [SerializeField] private Text defenseText;
    [SerializeField] private Text pointText;
    [SerializeField] private Image backGround;
    Unit unit;

    static private Dictionary<string, Color> factionColors = new Dictionary<string, Color>()
    {    
        {"jandar", Color.blue },
        {"ullar", Color.green },
        {"vydar", new Color(0,0,.4f) },
        {"einar", new Color(1, .63f, 0) },
        {"aquilla", Color.yellow },
        {"utgar", Color.red },
        {"valkrill", new Color(.82f, .70f, .52f) }
    };


    public void SetUnit(Unit unit)
    {
        this.unit = unit;
        UpdataCard();
    }

    private void UpdataCard()
    {
        nameText.text = unit.name;
        lifeText.text = unit.life.ToString();
        moveText.text = unit.move.ToString();
        rangeText.text = unit.range.ToString();
        attackText.text = unit.attack.ToString();
        defenseText.text = unit.defense.ToString();
        pointText.text = unit.points.ToString();
        backGround.color = factionColors[unit.general];
    }
}
