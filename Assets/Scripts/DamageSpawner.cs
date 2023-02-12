using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DamageSpawner : MonoBehaviourPun
{
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            StartCoroutine(ExampleCoroutine());
            GameObject dmgToken = PhotonNetwork.Instantiate("DamageCounter", this.transform.position, Quaternion.identity);
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds((float) .2);
    }
}
