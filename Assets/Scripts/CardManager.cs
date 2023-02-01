using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.SocialPlatforms;
using System.IO;

public class CardManagerLegacy : MonoBehaviour
{
// Import Cards
    public TextAsset jsonFile;
    private Units units = null;
    [SerializeField] private GameObject unitCard;
    public GameObject onBoardCard;

// Card Placement
    static public float cardWidth = 500;
    static public float cardHieght = 150 * 2;
    private Vector3 cardPosition = new Vector3(0, Screen.height/2);
    private float scrollSpeed = 7500;

// Player canvas
    [SerializeField] public GameObject playerOneCanvas;
    [SerializeField] public GameObject playerTwoCanvas;

    public void Start()
    {
        MakeUnitArray();
        MakeDeckOnCanvas();
    }

    private void Update()
    {
        //// Scroll ////
        if (CameraController.isPaused)
        {
            this.transform.position =  this.transform.position - new Vector3(0, Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime, 0);
        }
    }

    private void MakeUnitArray()
    {
        units = JsonUtility.FromJson<Units>(jsonFile.text);
    }

    private void MakeDeckOnCanvas()
    {

        foreach (Unit unit in units.units)
        {
            GameObject unitCardInstant = Instantiate(unitCard);

            unitCardInstant.transform.parent = this.gameObject.transform;
            unitCardInstant.transform.position = cardPosition;
            IncrementCardPosition();

        }
    }

    private void IncrementCardPosition()
    {
        cardPosition = cardPosition + Vector3.right * cardWidth;
        if(cardPosition.x > Screen.width)
        {
            cardPosition = new Vector3(0, cardPosition.y - cardHieght);
        }
    }
}

