using System;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject Protagonist;
    public GameObject Canvas;
    public int IncreaseRate;

    private int[] ProductPrice;
    private PauseScript Pause;

    private void Start()
    {
        Pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseScript>();

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
            Protagonist.GetComponent<MainCharacter_StateManager>().IncreaseMaxHP(IncreaseRate);
            Protagonist.GetComponent<MainCharacter_StateManager>().ChangeXP(-(ProductPrice[0]));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_MaxHealth()));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP()));
        }
    }

    public void SellEnergyPowerUp()
    {
        if (Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP() >= ProductPrice[1])
        {
            Protagonist.GetComponent<MainCharacter_StateManager>().IncreaseMaxStamina(IncreaseRate);
            Protagonist.GetComponent<MainCharacter_StateManager>().ChangeXP(-(ProductPrice[1]));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_MaxStamina()));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP()));
        }
    }

    public void SellHealingPotion()
    {
        if (Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP() >= ProductPrice[0])
        {
            Protagonist.GetComponent<MainCharacter_StateManager>().IncreaseHealingPotionsTotal();
            Protagonist.GetComponent<MainCharacter_StateManager>().ChangeXP(-(ProductPrice[4]));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_HealingPotionsCounter()));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<MainCharacter_StateManager>().Get_CurrentXP()));
        }
    }

    public void DisableStore()
    {
        Pause.Resume();
        Canvas.SetActive(false);
    }
}