using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CardAndHeroSpawner : MonoBehaviour
{
    // Import Cards
    public TextAsset jsonFile;
    private Units units = null;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject figurePrefab;

    // Card Placement
    static public float cardWidth = 5;
    static public float cardHieght = 5;
    private int numberInRow = 10;
    private Vector3 cardPosition = new Vector3(0,0,0);

    private void MakeUnitArray()
    {
        units = JsonUtility.FromJson<Units>(jsonFile.text);
    }

    public void MakeCardsOnBoard()
    {
        MakeUnitArray();
        foreach (Unit unit in units.units)
        {
            GameObject cardInstant = PhotonNetwork.Instantiate(cardPrefab.name, cardPosition, cardPrefab.transform.rotation);
            cardInstant.GetComponent<CardController>().SetUnit(unit);
            //cardInstant.transform.SetParent(this.transform);

            IncrementCardPosition();

        }
    }
        
    private void IncrementCardPosition()
    {
        cardPosition = new Vector3(cardPosition.x + cardWidth, 0, cardPosition.z);
        if (cardPosition.x > numberInRow * cardWidth)
        {
            cardPosition = new Vector3(0, 0, cardPosition.z + cardHieght);
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
    public string height;
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
