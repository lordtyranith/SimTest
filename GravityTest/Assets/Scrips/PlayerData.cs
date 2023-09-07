using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData> 
{
    public float playerMoney;

    [Header("Inventory")]
    public List<Clothes> personalItens= new List<Clothes>();

    [Header("Using Itens")]
    public Clothes HatRole;
    public Clothes BodyRole;
    public Clothes LegRole;

    [Header("Sprites Character")]
    public SpriteRenderer hoodPart; 
    public SpriteRenderer bodyPart; 
    public SpriteRenderer legsPart; 


    private void Start()
    {
        
    }

    public void ChangeHood(Clothes item)
    {
       if (HatRole != null)
       {
           personalItens.Add(HatRole);
           personalItens.Remove(item);
       }
       else
       {
            personalItens.Remove(item);
       }

        
        HatRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.HoodEquipped, item);

        Sprite newSprite = item.ingameImage;

        hoodPart.sprite = newSprite;


        UpdatePersonalItens();

    }

    public void UpdatePersonalItens()
    {

        int index = 0;
        foreach(Clothes item in personalItens)
        {
            if (item.name.Length < 2)
            {
                personalItens.RemoveAt(index);
                return;
            }
            index++;
        }



      // for (int i = 0; i < personalItens.Count; i++)
      // {
      //     if (personalItens[i].name.Length < 2)
      //     {
      //         personalItens.RemoveAt(i);
      //         return;
      //     }
      // }
    }
    public void ChangeBody(Clothes item)
    {
       
        if (BodyRole != null)
        {
            personalItens.Add(BodyRole);
            personalItens.Remove(item);
        }
        else
        {
            personalItens.Remove(item);
        }



        BodyRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.BodyEquipped, item);
        Sprite newSprite = item.ingameImage;

        bodyPart.sprite = newSprite;

        for (int i = 0; i < personalItens.Count; i++)
        {
            if (personalItens[i].name.Length < 2)
            {
                personalItens.RemoveAt(i);
                return;
            }
        }


    }
    public void ChangeLegs(Clothes item)
    {
       
        if (LegRole != null)
        {
            personalItens.Add(LegRole);
            personalItens.Remove(item);
        }
        else
        {
            personalItens.Remove(item);
        }


        LegRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.LegsEquipped, item);
        Sprite newSprite = item.ingameImage;

        legsPart.sprite = newSprite;

        for (int i = 0; i < personalItens.Count; i++)
        {
            if (personalItens[i].name.Length < 2)
            {
                personalItens.RemoveAt(i);
                return;
            }
        }
    }

    public float UpdateMoney(float amount)
    {
        playerMoney = playerMoney + amount;
        return playerMoney;
    }


    public void AddNewItem(Clothes item)
    {
        personalItens.Add(item);    
    }

    public void RemoveItem(Clothes item)
    {
        personalItens.Remove(item);

    }


    public void RemoveEquippedIten(Clothes item)
    {
        clotheType typeItem = item.part;

        switch (typeItem)
        {
            case clotheType.Hood:
                HatRole = null;

                break;

            case clotheType.Body:
                BodyRole = null;
                break;

            case clotheType.Legs:
                LegRole = null;
                break;

        }
    }


}
