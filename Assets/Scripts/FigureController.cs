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

    public void LoadPath(string path)
    {
        try
        {
            GameObject instance = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
            //if (instance == null) { Destroy(this.gameObject); }
            instance.transform.parent = this.transform;
            instance.transform.Rotate(-90, 0, 0);
        }
        catch { }
    }
}
