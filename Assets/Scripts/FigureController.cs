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

    private int currentRotation = 0;

    private Vector3 scaleVector = new Vector3(0.5f, 0.5f, 0.5f);

    public void LoadPath(string path)
    {
        try
        {
            GameObject instance = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
            instance.transform.localScale = scaleVector;
            instance.transform.parent = this.transform;
            instance.transform.Rotate(-90, 0, 0);
        }
        catch { }
    }

    private void FixPosition()
    {
        Vector3 parentPostion = this.transform.parent.GetComponent<Transform>().position;
    }

    public void RotateFigure()
    {
        currentRotation += 60;
        this.transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);
    }
}
