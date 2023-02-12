using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PingController : MonoBehaviourPun
{
    void Start()
    {
        if (photonView.IsMine)
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(photonView.gameObject);
    }

}
