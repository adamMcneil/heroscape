using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] private Text skulls;
    [SerializeField] private Text shields;
    [SerializeField] private Text nothing;
    [SerializeField] private Text twenty;

    private string baseSkulls = "Skulls: ";
    private string baseShields = "Shields: ";
    private string baseNothing = "Nothing: ";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RollDice(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RollDice(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RollDice(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RollDice(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RollDice(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            RollDice(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            RollDice(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            RollDice(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            RollDice(9);
        }
        if (Input.GetKeyDown(KeyCode.T)) 
        { 
            RollD20();
        }

    }

    private void RollDice(int numberOfRolls)
    {
        int attack = 0;
        int defense = 0;
        int blank = 0;
        for (int i = 0; i < numberOfRolls; i++)
        {
            int randomNumber = Random.Range(0, 6);
            if (randomNumber < 3)
            {
                attack++;
            }
            else if (randomNumber < 5) 
            {
                defense++;
            }
            else
            {
                blank++;
            }
            skulls.text = baseSkulls + attack.ToString();
            shields.text = baseShields + defense.ToString();
            nothing.text = baseNothing + blank.ToString();
        }
    }

    private void RollD20()
    {
        twenty.text = Random.Range(1, 21).ToString();
    }
}
