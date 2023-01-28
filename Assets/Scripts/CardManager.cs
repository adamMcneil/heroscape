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

    public void Start()
    {
        //string jsonString = File.ReadAllText(Application.dataPath + "/HeroScapeData/units");

        Units unitInJson = JsonUtility.FromJson<Units>(jsonFile.text);
        int count = 0;
        foreach (Unit unit in unitInJson.units)
        {
            Debug.Log("unit: " + unit.name);
            count++;
            foreach (Ability ability in unit.abilities)
            {
                Debug.Log(ability.name);
            }
        }
        Debug.Log(count);
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
    public string personalit;
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
public class Abilities

{
    public Ability[] abilities;
}

[System.Serializable]
public class Ability
{
    public string name;
    public string description;
}