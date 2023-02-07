using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HexController : MonoBehaviourPun
{
    public void DestroyHex()
    {
        photonView.RequestOwnership();
        StartCoroutine(DestroyHexCorutine());

    }

    IEnumerator DestroyHexCorutine()
    {
        yield return new WaitForSeconds(0.1f);
        PhotonNetwork.Destroy(gameObject);

    }
}
