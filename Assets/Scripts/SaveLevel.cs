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
    private string path = "\\SaveFiles\\";
    private string fileExtention = ".txt";



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
                Piece piece = new Piece(pieceGameObject.name.Replace("(Clone)", ""), pieceGameObject.transform.position);
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
                GameObject madeObject = PhotonNetwork.Instantiate(piece.name, piece.position, Quaternion.identity);
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
