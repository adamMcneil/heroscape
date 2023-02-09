using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarkerController : MonoBehaviour
{
    private float currentRotation = 0;

    public void RotateMoveMarker()
    {
        currentRotation += 90;
        this.transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);
    }
}
