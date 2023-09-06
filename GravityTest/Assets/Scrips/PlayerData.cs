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
    [SerializeField] SpriteRenderer hoodPart; 
    [SerializeField] SpriteRenderer bodyPart; 
    [SerializeField] SpriteRenderer legsPart; 


    private void Start()
    {
        
    }

    public void ChangeHood(Clothes item)
    {
        if (HatRole != null)
        {
            personalItens.Add(HatRole);
        }

        personalItens.Remove(item);
        HatRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.HoodEquipped, item);

        Sprite newSprite = item.ingameImage;

        hoodPart.sprite = newSprite;


    }
    public void ChangeBody(Clothes item)
    {
        personalItens.Remove(item);
        BodyRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.BodyEquipped, item);
        Sprite newSprite = item.ingameImage;

        bodyPart.sprite = newSprite;

    }
    public void ChangeLegs(Clothes item)
    {
        personalItens.Remove(item);
        LegRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.LegsEquipped, item);
        Sprite newSprite = item.ingameImage;

        legsPart.sprite = newSprite;
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



}
