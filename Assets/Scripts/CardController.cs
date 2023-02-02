using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private string path_1 = "Heros\\";
    private string pathCard = "\\card\\";
    private string pathObject = "\\obj\\";
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
        this.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", cardIMG);
        SpawnFigures();
    }

    private void SpawnFigures()
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
        GameObject figureInstant = Instantiate(figurePrefab);
        figureInstant.transform.SetParent(this.transform);
        figureInstant.GetComponent<FigureController>().LoadPath(path, myUnit.general);
    }
}
