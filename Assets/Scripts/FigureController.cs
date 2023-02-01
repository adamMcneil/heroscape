using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    private string path = "Heros\\Braxas\\obj\\";
    private string modelName = "Braxas";
    void Start()
    {
        Debug.Log(path + modelName);
        GameObject instance = Instantiate(Resources.Load(path + modelName, typeof(GameObject))) as GameObject;
        instance.transform.parent = this.transform;
    }
}
