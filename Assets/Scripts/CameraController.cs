using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
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
    private float rotationSpeed = 250;

// Pointer
    [SerializeField] private GameObject pointer;
    [SerializeField] private LayerMask layerMask;

// Spawnable prefabs
    [SerializeField] private GameObject hex;
    [SerializeField] private GameObject hex7;
    [SerializeField] private GameObject hex19;
    [SerializeField] private GameObject column;
    [SerializeField] private GameObject figureRed;
    [SerializeField] private GameObject figureBlue;

    [SerializeField] private GameObject level; // The GameObject where all the stuff is spawned

    private GameObject selectedHex;
    private GameObject selectedObject;

// Hex grid stuff
    private float squareRoot3 = (float)Math.Sqrt(3);
    private float yScale = 5;
    private Vector3 hexToSquare;
    private Vector3 squareToHex;

// Pause stuff
    static public bool isPaused = false;

// Menus
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject playerOneCanvas;
    [SerializeField] private GameObject playerTwoCanvas;


    private void Start()
    {
        currentSpeed = slowSpeed;
        pauseMenu.SetActive(isPaused);
        selectedHex = hex;
        hexToSquare = new Vector3(0.5f, 0, 0.5f * squareRoot3);
        squareToHex = new Vector3(-1/squareRoot3, 0, 2f / squareRoot3);
        Cursor.lockState = CursorLockMode.Locked;
        thisCamera = GetComponent<Camera>();
    }

    private void Update()
    {
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
            rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            this.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }

        //// pointer ////
        Vector2 centerScreenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = thisCamera.ScreenPointToRay(centerScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, layerMask))
        {
            pointer.transform.position = rayCastHit.point;
        }

        //// make raycast ////
        if (Input.GetMouseButtonDown(0) && !isPaused)
        {
            Instantiate(selectedHex, CalculateHexPosition(ShotRayVector3()), Quaternion.identity).transform.parent = level.transform;
        }

        //// destroy raycast ////

        if (Input.GetMouseButtonDown(1) && !isPaused)
        {
            GameObject hitObject = ShotRayGameObject();
            try
            {
                if (!hitObject.CompareTag("Floor"))
                {
                    Destroy(hitObject);
                }
            }
            catch { }
        }

        //// pick up raycast ////
        if (Input.GetKeyDown(KeyCode.Space) && !isPaused)
        {
            GameObject hitFigure = ShotRayGameObject();
            try
            {
                if (hitFigure.CompareTag("Figure") || hitFigure.CompareTag("Card"))
                {
                    selectedObject = hitFigure;
                }
                else
                {
                    if (selectedObject.CompareTag("Card"))
                    {
                        selectedObject.transform.position = CalculateHexPosition(ShotRayVector3());
                    }
                    else
                    {
                        selectedObject.transform.position = ShotRayVector3();
                    }
                    selectedObject = null;
                }
            }
            catch { }
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

        //// Player Canvas ////
        playerOneCanvas.SetActive(false);
        if (Input.GetKey(KeyCode.R))
        {
            playerOneCanvas.SetActive(true);
        }

        playerTwoCanvas.SetActive(false);
        if (Input.GetKey(KeyCode.T))
        {
            playerTwoCanvas.SetActive(true);
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
        Vector2 centerScreenPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = thisCamera.ScreenPointToRay(centerScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit rayCastHit, float.MaxValue, layerMask))
        {
            pointer.transform.position = rayCastHit.point;
        }
        return pointer.transform.position;
    }

    #endregion

    #region On Click Events
    public void OnButtonClickedOne()
    {
        selectedHex = hex;
    }
    public void OnButtonClickedSeven()
    {
        selectedHex = hex7;
    }
    public void OnButtonClicked19()
    {
        selectedHex = hex19;
    }
    public void OnButtonClickedColumn()
    {
        selectedHex = column;
    }
    public void OnButtonRedFigure()
    {
        selectedHex = figureRed;
    }
    public void OnButtonBlueFigure()
    {
        selectedHex = figureBlue;
    }
    #endregion
}


