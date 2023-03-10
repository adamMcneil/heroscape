using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPun
{
    private Camera thisCamera;

 // Movement
    private float forward;
    private float back;
    private float up;
    private float down;
    private float right;
    private float left;

    private float rotationX;
    private float rotationY;

    private float currentSpeed;
    private float fastSpeed = 50;
    private float slowSpeed = 10;
    private float rotationSpeed = 0.5f;

// Pointer  
    [SerializeField] private LayerMask layerMask;

// Spawnable prefabs



    private GameObject level; // The GameObject where all the stuff is spawned

    static public GameObject selectedHex;
    private GameObject selectedObject;

// Hex grid stuff
    private float squareRoot3 = (float)Math.Sqrt(3);
    private float yScale = 5;
    private Vector3 hexToSquare;
    private Vector3 squareToHex;

// Pause stuff
    static public bool isPaused = false;
    static public bool isBuilding = false;

    // Menus
    private GameObject pauseMenu;

    private GameObject cardMaker;


    private void Start()
    {
        if (photonView.IsMine)
        {
            level = GameObject.Find("Level");
            pauseMenu = GameObject.Find("Pause Canvas");
            thisCamera = GameObject.Find("Camera").GetComponent<Camera>();
            cardMaker = GameObject.Find("CardAndHeroSpawner");

            currentSpeed = slowSpeed;
            pauseMenu.SetActive(isPaused);
            hexToSquare = new Vector3(0.5f, 0, 0.5f * squareRoot3);
            squareToHex = new Vector3(-1/squareRoot3, 0, 2f / squareRoot3);
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (photonView.ViewID == 1001 && photonView.IsMine)
        {
            cardMaker.GetComponent<CardAndHeroSpawner>().MakeCardsOnBoard();
        }
    }

    private void Update()
    {
        if (photonView.IsMine) { 
            //// boost /////
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (currentSpeed == slowSpeed)
                {
                    currentSpeed = fastSpeed;
                }
                else
                {
                    currentSpeed = slowSpeed;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
              thisCamera.fieldOfView = 10f;
            } else { 
              thisCamera.fieldOfView = 60;          
            }

            //// movement ////
            forward = Convert.ToSingle(Input.GetKey(KeyCode.W));
            back = -Convert.ToSingle(Input.GetKey(KeyCode.S));
            up = Convert.ToSingle(Input.GetKey(KeyCode.E));
            down = -Convert.ToSingle(Input.GetKey(KeyCode.Q));
            right = Convert.ToSingle(Input.GetKey(KeyCode.D));
            left = -Convert.ToSingle(Input.GetKey(KeyCode.A));

            this.transform.position = this.transform.position
            + (this.transform.forward * (forward + back)
            + this.transform.up * (up + down)
            + this.transform.right * (right + left))
            * (currentSpeed * Time.deltaTime);

            //// rotaion ////
            if (!isPaused)
            {
                rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
                rotationY += Input.GetAxis("Mouse X") * rotationSpeed;

                this.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
            thisCamera.transform.position = this.transform.position;
            thisCamera.transform.rotation = this.transform.rotation;


            //// make raycast ////
            if (Input.GetMouseButtonDown(0) && !isPaused && isBuilding)
            {
                PhotonNetwork.Instantiate(selectedHex.name, CalculateHexPosition(ShotRayVector3()), Quaternion.identity).transform.parent = level.transform;
            }

            //// destroy raycast ////

            if (Input.GetMouseButtonDown(1) && !isPaused)
            {
                GameObject hitObject = ShotRayGameObject();
                //try
                //{
                    if (!hitObject.CompareTag("Floor") && !hitObject.CompareTag("Card") && !hitObject.CompareTag("Figure"))
                    {
                        if (hitObject.GetComponent<PhotonView>() != null)
                        {
                            hitObject.GetComponent<HexController>().DestroyHex();
                        }
                    }
                //}
                //catch { }
            }

            //// pick up raycast ////
            if (Input.GetMouseButtonDown(0) && !isPaused && !isBuilding)
            {
                GameObject hitFigure = ShotRayGameObject();
                try
                {
                    if ((hitFigure.CompareTag("Figure") || hitFigure.CompareTag("Card") || hitFigure.CompareTag("DamageCounter") || hitFigure.CompareTag("MoveMarker")) && selectedObject == null)
                    {
                        selectedObject = hitFigure;
                        hitFigure.GetComponent<PhotonView>().RequestOwnership();
                    }
                    else
                    {
                        if (selectedObject.CompareTag("DamageCounter"))
                        {
                            selectedObject.transform.position = ShotRayVector3() + (Vector3.up*0.0f);
                        }
                        else
                        {
                            selectedObject.transform.position = CalculateHexPosition(ShotRayVector3());
                        }
                        selectedObject = null;
                    }
                }
                catch { selectedObject = null; }
            }

            //// rotate raycast ////
            if (Input.GetKeyDown(KeyCode.R) && !isPaused)
            {
                GameObject hitFigure = ShotRayGameObject();
                try
                {
                    if (hitFigure.CompareTag("Figure"))
                    {
                        hitFigure.GetComponent<FigureController>().RotateFigure();
                    }
                    else if (hitFigure.CompareTag("MoveMarker"))
                    {
                        hitFigure.GetComponent<MoveMarkerController>().RotateMoveMarker();
                    }
                }
                catch { }
            }

            //// make raycast ////
            if (Input.GetKeyDown(KeyCode.M) && !isPaused)
            {
                GameObject hitCard = ShotRayGameObject();
                try
                {
                    if (hitCard.CompareTag("Card"))
                    {
                        hitCard.GetComponent<CardController>().SpawnFigures();
                    }
                }
                catch { }
            }

            //// make raycast ////
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                PhotonNetwork.Instantiate("Ping", ShotRayVector3(), Quaternion.identity);

            }

            //// pause ////
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                pauseMenu.SetActive(isPaused);
            }

            //// pause ////
            if (Input.GetKeyDown(KeyCode.B))
            {
                isBuilding = !isBuilding;
            }
        }
    }



    #region Hex Math
    private Vector3 CalculateHexPosition(Vector3 position)
    {
        //  hex to square
        //  | 1 0     1/2           |
        //  | 0 1/5   0             |
        //  | 0 0     (3^(1/2)) / 2 |
        // 
        //  use the inverse to go from square to hex    

        Vector3 X = new Vector3(position.x, 0, 0);
        Vector3 Y = new Vector3(0, position.y*yScale, 0);
        Vector3 Z = position.z * squareToHex;

        Vector3 newHexPosition = X + Y + Z;
        newHexPosition = new Vector3((float)Math.Round(newHexPosition.x), (float)Math.Round(newHexPosition.y), (float)Math.Round(newHexPosition.z));

        X = new Vector3(newHexPosition.x, 0, 0);
        Y = new Vector3(0, newHexPosition.y/yScale, 0);
        Z = newHexPosition.z * hexToSquare;

        return X + Y + Z;
    }
    #endregion

    #region ShotRay
    GameObject ShotRayGameObject()
    {
        GameObject returnObject = null;
        Vector2 centerScreenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = thisCamera.ScreenPointToRay(centerScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, layerMask))
        {
            returnObject = rayCastHit.collider.gameObject;
        }
        return returnObject;
    }

    Vector3 ShotRayVector3()
    {
        Vector3 returnPosition = Vector3.zero;
        Vector2 centerScreenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = thisCamera.ScreenPointToRay(centerScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, layerMask))
        {
            returnPosition = rayCastHit.point;
        }
        return returnPosition;
    }

    #endregion


}


