using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Store : MonoBehaviour
{
    public GameObject Protagonist;
    public GameObject Canvas;
    public int IncreaseRage;

    private int[] ProductPrice;

    private void Start()
    {
        Debug.Log("GotHere!");
        ProductPrice = new int[5] { 10, 10, 10, 10, 10 };
    }

    public void SetProtagonist(GameObject Protagonist)
    {
        this.Protagonist = Protagonist;
    }

    public void SellLifePowerUp()
    {
        if (Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP() >= ProductPrice[0])
        {
            Protagonist.GetComponent<MainCharacter_StateManager>().IncreaseMaxHP(25);
            Protagonist.GetComponent<MainCharacter_StateManager>().ChangeXP(-(ProductPrice[0]));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_MaxHealth());
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP());
        }
    }

    public void SellEnergyPowerUp()
    {
        if (Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP() >= ProductPrice[1])
        {
            Protagonist.GetComponent<MainCharacter_StateManager>().IncreaseMaxStamina(25);
            Protagonist.GetComponent<MainCharacter_StateManager>().ChangeXP(-(ProductPrice[1]));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_MaxStamina());
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP());
        }
    }

    public void SellHealingPotion()
    {
        if (Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP() >= ProductPrice[0])
        {
            Protagonist.GetComponent<MainCharacter_StateManager>().IncreaseHealingPotionsTotal();
            Protagonist.GetComponent<MainCharacter_StateManager>().ChangeXP(-(ProductPrice[4]));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_HealingPotionsCounter());
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP());
        }
    }

    public void DisableStore()
    {
        Canvas.SetActive(false);
    }
}