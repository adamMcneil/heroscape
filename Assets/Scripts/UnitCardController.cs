using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UnitCardController : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text lifeText;
    [SerializeField] private Text moveText;
    [SerializeField] private Text rangeText;
    [SerializeField] private Text attackText;
    [SerializeField] private Text defenseText;
    [SerializeField] private Text pointText;
    [SerializeField] private Text abilityDescription;
    [SerializeField] private Text count;
    [SerializeField] private Image backGround;
    [SerializeField] private Dropdown dropDownAblities;
    public Unit myUnit;


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

    public void UpdateAblityDescription()
    {
        abilityDescription.text = myUnit.abilities[dropDownAblities.value].description;
    }

    public void SetUnit(Unit unit)
    {
        this.myUnit = unit;
        UpdataCard();
    }

    private void UpdataCard()
    {
        nameText.text = myUnit.name;
        lifeText.text = myUnit.life.ToString();
        moveText.text = myUnit.move.ToString();
        rangeText.text = myUnit.range.ToString();
        attackText.text = myUnit.attack.ToString();
        defenseText.text = myUnit.defense.ToString();
        pointText.text = myUnit.points.ToString();
        count.text = myUnit.figures.ToString();
        backGround.color = factionColors[myUnit.general];

        dropDownAblities.ClearOptions();
        List<string> ablitiyNameList = new List<string>();
        foreach (Ability ablility in myUnit.abilities)
        {
            ablitiyNameList.Add(ablility.name);
        }
        dropDownAblities.AddOptions(ablitiyNameList);
        abilityDescription.text = myUnit.abilities[0].description;

    }
}
