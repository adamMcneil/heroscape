using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private string path = "Heros\\Braxas\\card\\";
    private string fileName = "Braxas-1";
    void Start()
    {
    Debug.Log(path + fileName);
        var cardIMG = Resources.Load<Texture>(path +fileName);
        this.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", cardIMG);
    }
}
