using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData> 
{
    public float playerMoney;

    [SerializeField] List<Clothes> personalItens= new List<Clothes>();


    private void Start()
    {
        
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
