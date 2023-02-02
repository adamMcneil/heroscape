using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLevel : MonoBehaviour
{
    private const string SAVE_SEPARATOR = "#SAVE_SEPARATOR#";
    [SerializeField] private GameObject[] pieces;
    private string[] piecesName = {"Hex(Clone)", "Hex7(Clone)", "Hex19(Clone)", "Column(Clone)", "Figure Red(Clone)", "Figure Blue(Clone)"};
    private string saveFileName = "/heroscape_save_file.txt";


    static private Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();

    private void Start()
    {
        MakeDictionary();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            SaveLevelPieces();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevelPieces();
        }
    }

    private void MakeDictionary()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            nameToObject.Add(piecesName[i], pieces[i]);
        }
    }

    private void SaveLevelPieces()
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
        File.WriteAllText(Application.dataPath + saveFileName, saveString);
    }

    private void LoadLevelPieces()
    {
        string saveString = File.ReadAllText(Application.dataPath + saveFileName);
        string[] jsonPieces = saveString.Split(new[] { SAVE_SEPARATOR }, System.StringSplitOptions.None);
        foreach (string jsonString in jsonPieces)
        {
            Piece piece = JsonUtility.FromJson<Piece>(jsonString);
            GameObject madeObject = Instantiate(nameToObject[piece.name]);
            madeObject.transform.position = piece.position;
            madeObject.transform.parent = this.transform;
        }
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
