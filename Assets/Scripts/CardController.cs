using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private string path = "";
    private string fileName = "Acolarh-1";
    void Start()
    {
    Debug.Log("here");
        var cardIMG = Resources.Load<Texture>(fileName);
        this.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", cardIMG);
    }
}
