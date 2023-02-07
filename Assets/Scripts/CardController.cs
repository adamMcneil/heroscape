using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using static UnityEngine.UI.CanvasScaler;

public class CardController : MonoBehaviourPun
{
    private string fileEndding = "-1";

    private Unit myUnit;

    [SerializeField] private GameObject figurePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals)) 
        {
            photonView.RPC("SyncPicureRPC", RpcTarget.All, JsonUtility.ToJson(myUnit));
        }
    }

    public void SetUnitServer(Unit unit)
    {
        myUnit = unit;
        photonView.RPC("SyncPicureRPC", RpcTarget.All, JsonUtility.ToJson(unit));
        LoadUnit();
    }

    public void SetUnit(string unit)      
    {
        myUnit = JsonUtility.FromJson<Unit>(unit);
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
        figureInstant.GetComponent<FigureController>().LoadPath(path, myUnit.general, transform.position);
    }


    [PunRPC]
    void SyncPicureRPC(string unit)
    {
       SetUnit(unit);
    }

}
