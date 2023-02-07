using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class CardController : MonoBehaviour
{
    private string fileEndding = "-1";

    private Unit myUnit;

    [SerializeField] private GameObject figurePrefab;

    public void SetUnit(Unit unit)      
    {
        myUnit = unit;
        LoadUnit();
    }

    private void LoadUnit()
    {
        var cardIMG = Resources.Load<Texture>("Cards\\" + myUnit.name + fileEndding);
        this.gameObject.GetComponentInChildren<MeshRenderer>().material.SetTexture("_MainTex", cardIMG);
        //SpawnFigures();
    }

    public void SpawnFigures()
    {
        if (myUnit.figures == 1)
        {
            SpawnFigure("Figures\\" + myUnit.name);
        }
        else
        {
            for (int i = 1; i <= myUnit.figures; i++)
            {
                SpawnFigure("Figures\\" + myUnit.name + i.ToString());
            }
        }
    }

    private void SpawnFigure(string path)
    {
        GameObject figureInstant = PhotonNetwork.Instantiate(figurePrefab.name, this.transform.position, Quaternion.identity);
        figureInstant.transform.SetParent(this.transform);
        figureInstant.GetComponent<FigureController>().LoadPath(path, myUnit.general, 1);
    }

}
