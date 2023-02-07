using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HexController : MonoBehaviourPun
{
    public void DestroyHex()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
