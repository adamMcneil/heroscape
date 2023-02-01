using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    private string path = "objlist/";
    private string modelName = "Dund";
    void Start()
    {
        GameObject instance = Instantiate(Resources.Load(path + modelName, typeof(GameObject))) as GameObject;
        instance.transform.parent = this.transform;
    }
}
