using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.SocialPlatforms;
using System.IO;

public class CardManager : MonoBehaviour
{
    public TextAsset jsonFile;
    private Units units = null;
    [SerializeField] private GameObject unitCard;
    private float cardWidth = 500;
    private float cardHieght = 150 * 2;
    private Vector3 cardPosition = new Vector3(0, Screen.height/2);

    private float scrollSpeed = 7500;

    public void Start()
    {
        MakeUnitArray();
        DisplayDeckOnCanvas();
    }

    private void Update()
    {
        if (CameraController.isPaused)
        {
            this.transform.position =  this.transform.position - new Vector3(0, Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime, 0); 
        }
    }

    private void MakeUnitArray()
    {
        units = JsonUtility.FromJson<Units>(jsonFile.text);
    }

    private void DisplayDeckOnCanvas()
    {

        foreach (Unit unit in units.units)
        {
            GameObject unitCardInstant = Instantiate(unitCard);

            unitCardInstant.transform.parent = this.gameObject.transform;
            unitCardInstant.transform.position = cardPosition;
            IncrementCardPosition();

            UnitCardController unitCardController = unitCard.GetComponent<UnitCardController>();
            unitCardController.SetUnit(unit);
        }
    }

    private void IncrementCardPosition()
    {
        cardPosition = cardPosition + Vector3.right * cardWidth;
        if(cardPosition.x > Screen.width)
        {
            cardPosition = new Vector3(0, cardPosition.y - cardHieght);
        }
    }
}

[System.Serializable]
public class Units

{
    public Unit[] units;
}

[System.Serializable]
public class Unit
{
    public string name;
    public string image;
    public string general;
    public string race;
    public string type;
    public string cardClass;
    public string personality;
    public int height;
    public int life;
    public int move;
    public int range;
    public int attack;
    public int defense;
    public int points;
    public int figures;
    public int hex;
    public string set;
    public Ability[] abilities;
}

[System.Serializable]
public class Ability
{
    public string name;
    public string description;
}