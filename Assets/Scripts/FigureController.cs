using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    [SerializeField] private GameObject figurePrefab;

    private int currentRotation = 0;

    private Vector3 scaleVector = new Vector3(0.5f, 0.5f, 0.5f);

    [SerializeField] private Material mat;

    static Dictionary<string, Color> factionToColor = new Dictionary<string, Color>()
    {
        { "jandar", new Color(0.2156f, 0.4745f, 0.7137f) },
        { "aquilla", new Color(0.8431f, 0.7843f, 0.0078f) },
        { "einar", new Color(0.7607844f, 0.4196079f, 0.1490196f) },
        { "ullar", new Color(0.2941177f, 0.3843138f, 0.1686275f) },
        { "utgar", new Color(0.6078432f, 0.03137255f, .05490196f) },
        { "valkrill", new Color(0.627451f, 0.4745098f, 0.2588235f) },
        { "vydar", new Color(0.282353f, 0.4941177f, 0.5647059f) }
    };


    public void LoadPath(string path, string faction)
    {
        try
        {
            GameObject instance = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
            instance.transform.localScale = scaleVector;
            instance.transform.parent = this.transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.Rotate(-90, 0, 0);
            instance.GetComponentInChildren<MeshRenderer>().material.color = factionToColor[faction];
        }
        catch { }
    }

    public void RotateFigure()
    {
        currentRotation += 60;
        this.transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);
    }
}
