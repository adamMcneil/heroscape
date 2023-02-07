using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileChanger : MonoBehaviour
{
    [SerializeField] private GameObject hex;
    [SerializeField] private GameObject hex7;
    [SerializeField] private GameObject hex19;
    [SerializeField] private GameObject column;
    [SerializeField] private GameObject road;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject rock1;
    [SerializeField] private GameObject rock3;
    [SerializeField] private GameObject rock6;

    #region On Click Events
    public void OnButtonClickedOne()
    {
        CameraController.selectedHex = hex;
    }
    public void OnButtonClickedSeven()
    {
        CameraController.selectedHex = hex7;
    }
    public void OnButtonClicked19()
    {
        CameraController.selectedHex = hex19;
    }
    public void OnButtonClickedColumn()
    {
        CameraController.selectedHex = column;
    }
    public void OnButtonWater()
    {
        CameraController.selectedHex = water;
    }
    public void OnButtonRoad()
    {
        CameraController.selectedHex = road;
    }
    public void OnButtonTree()
    {
        CameraController.selectedHex = tree;
    }
    public void OnButtonRock1()
    {
        CameraController.selectedHex = rock1;
    }
    public void OnButtonRock3()
    {
        CameraController.selectedHex = rock3;
    }
    public void OnButtonRock6()
    {
        CameraController.selectedHex = rock6;
    }
    #endregion
}
