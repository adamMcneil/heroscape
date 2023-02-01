using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    private string path_1 = "Heros\\";
    private string path_2 = "\\obj\\"; 

    private Unit myUnit;
    private int squadNumber = -1;

    [SerializeField] private GameObject figurePrefab;

    public void SetUnit(Unit unit)
    {
        myUnit = unit;
        LoadUnit();
    }

    public void SetUnitSquad(Unit unit, int number)
    {
        myUnit = unit;
        squadNumber = number;
        LoadUnitSquad();
    }

    private void LoadUnit()
    {
        try
        {
            if (myUnit.figures == 1)
            {
                LoadPath(path_1 + myUnit.name + path_2 + myUnit.name);
            }
            else
            {
                LoadSquad();
            }
        }
        catch { }
    }

    private void LoadUnitSquad()
    {
        try
        {
            LoadPath(path_1 + myUnit.name + path_2 + myUnit.name + squadNumber.ToString());
        }
        catch { }
    }

    private void LoadSquad()
    {
        Debug.Log("here");
        Debug.Log(myUnit.figures);

        for (int i = 1; i <= myUnit.figures; i++)
        {
            GameObject figureInstant = Instantiate(figurePrefab);
            figureInstant.transform.SetParent(this.transform);
            figureInstant.transform.position = this.transform.position;
            figureInstant.GetComponent<FigureController>().SetUnitSquad(myUnit, i);
        }
        Debug.Log("here");

        // Destroy(this.gameObject);
    }

    private void LoadPath(string path)
    {
        GameObject instance = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
        if (instance == null) { Destroy(this.gameObject); }
        instance.transform.parent = this.transform;
        instance.transform.Rotate(-90, 0, 0);
    }
}
