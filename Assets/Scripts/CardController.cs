using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private string path_1 = "Heros\\";
    private string path_2 = "\\card\\";
    private string fileEndding = "-1";

    private Unit myUnit;

    public void SetUnit(Unit unit)
    {
        myUnit = unit;
        LoadUnit();
    }

    private void LoadUnit()
    {
        var cardIMG = Resources.Load<Texture>(path_1 + myUnit.name + path_2 + myUnit.name + fileEndding);
        this.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", cardIMG);
    }
}
