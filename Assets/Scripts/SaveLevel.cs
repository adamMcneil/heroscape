using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SaveLevel : MonoBehaviour
{
    private const string SAVE_SEPARATOR = "#SAVE_SEPARATOR#";
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private InputField saveFileInput;
    [SerializeField] private InputField loadFileInput;
    private string[] piecesName = {"Hex(Clone)", "Hex7(Clone)", "Hex19(Clone)", "Column(Clone)", "Road(Clone)", "Water(Clone)"};
    private string path = "\\SaveFiles\\";
    private string fileExtention = ".txt";


    static private Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();

    private void Start()
    {
        MakeDictionary();
    }

    private void MakeDictionary()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            nameToObject.Add(piecesName[i], pieces[i]);
        }
    }

    public void SaveLevelPieces()
    {
        try
        {
            List<string> jsonPieces = new List<string>();
            foreach (Transform pieceGameObject in GetComponentsInChildren<Transform>())
            {
                if (pieceGameObject.name == "Level" || pieceGameObject.parent != this.transform)
                {
                    continue;
                }
                Piece piece = new Piece(pieceGameObject.name, pieceGameObject.transform.position);
                jsonPieces.Add(JsonUtility.ToJson(piece));
            }
            string saveString = string.Join(SAVE_SEPARATOR, jsonPieces);
            File.WriteAllText(Application.dataPath + path + saveFileInput.text + fileExtention, saveString);
            Debug.Log(saveFileInput.text);
        }
        catch { Debug.Log("file did not save correctly"); }
    }

    public void LoadLevelPieces()
    {
        try
        {
            string saveString = File.ReadAllText(Application.dataPath + path + loadFileInput.text + fileExtention);
            string[] jsonPieces = saveString.Split(new[] { SAVE_SEPARATOR }, System.StringSplitOptions.None);
            foreach (string jsonString in jsonPieces)
            {
                Piece piece = JsonUtility.FromJson<Piece>(jsonString);
                GameObject madeObject = PhotonNetwork.Instantiate(nameToObject[piece.name].name, piece.position, Quaternion.identity);
                madeObject.transform.parent = this.transform;
            }
            Debug.Log(loadFileInput.text);

        }
        catch { Debug.Log("file did not load correctly"); }
    }
}

[Serializable]
public class Piece
{
    public Piece(string name, Vector3 position)
    {
        this.name = name;
        this.position = position;
    }
    public string name;
    public Vector3 position;
}
