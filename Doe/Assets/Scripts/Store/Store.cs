/*using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Store : MonoBehaviour
{
    public GameObject Protagonist;
    public GameObject Canvas;

    private float[] ProductPrice;

    private void Start()
    {
        Debug.Log("GotHere!");
        ProductPrice = new float[5] { 10.0f, 10.0f, 10.0f, 10.0f, 10.0f };
    }

    public void SetProtagonist(GameObject Protagonist)
    {
        this.Protagonist = Protagonist;
    }

    public void SellLifePowerUp()
    {
        if (Protagonist.GetComponent<ProtagonistMovement>().XP >= ProductPrice[0])
        {
            Protagonist.GetComponent<ProtagonistMovement>().TotalLife += 2;
            Protagonist.GetComponent<ProtagonistMovement>().XP -= ProductPrice[0];
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().TotalLife));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().XP));
        }
    }

    public void SellEnergyPowerUp()
    {
        if (Protagonist.GetComponent<ProtagonistMovement>().XP >= ProductPrice[1])
        {
            Protagonist.GetComponent<ProtagonistMovement>().TotalEnergy += 2;
            Protagonist.GetComponent<ProtagonistMovement>().XP -= ProductPrice[1];
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().TotalEnergy));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().XP));
        }
    }

    public void SellRecoveryPowerUp()
    {
        if (Protagonist.GetComponent<ProtagonistMovement>().XP >= ProductPrice[0])
        {
            Protagonist.GetComponent<ProtagonistMovement>().EnergyRecovery += 2;
            Protagonist.GetComponent<ProtagonistMovement>().XP -= ProductPrice[2];
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().EnergyRecovery));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().XP));
        }
    }

    public void SellProtectionPowerUp()
    {
        if (Protagonist.GetComponent<ProtagonistMovement>().XP >= ProductPrice[0])
        {
            Protagonist.GetComponent<ProtagonistMovement>().ProtectionDefense += 2;
            Protagonist.GetComponent<ProtagonistMovement>().XP -= ProductPrice[3];
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().ProtectionDefense));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().XP));
        }
    }

    public void SellHealingPotion()
    {
        if (Protagonist.GetComponent<ProtagonistMovement>().XP >= ProductPrice[0])
        {
            Protagonist.GetComponent<ProtagonistMovement>().HealingPotions += 1;
            Protagonist.GetComponent<ProtagonistMovement>().Money -= ProductPrice[4];
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().HealingPotions));
            Debug.Log(String.Format("{0}", Protagonist.GetComponent<ProtagonistMovement>().Money));
        }
    }

    public void DisableStore()
    {
        Canvas.SetActive(false);
    }
}
*/