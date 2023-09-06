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



    private void Start()
    {
        
    }

    public void ChangeHood(Clothes item)
    {
        HatRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.HoodEquipped, item);



    }
    public void ChangeBody(Clothes item)
    {

        BodyRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.BodyEquipped, item);

    }
    public void ChangeLegs(Clothes item)
    {

        LegRole = item;
        UIManager.Instance.ChangeSpriteEquipped(UIManager.Instance.LegsEquipped, item);

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




}
