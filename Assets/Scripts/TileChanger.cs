using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TileChanger : MonoBehaviour
{
    [SerializeField] private GameObject hex;
    [SerializeField] private GameObject hex7;
    [SerializeField] private GameObject hex19;
    [SerializeField] private GameObject column;
    [SerializeField] private GameObject road;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject tree;
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
    #endregion
}
