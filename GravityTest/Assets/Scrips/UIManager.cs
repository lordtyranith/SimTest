using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] Button buttonInventory;
    [SerializeField] Button buttonShop;

    [Header("Money")]
    [SerializeField] TextMeshProUGUI moneyDisplay;

    [Header("Shopping")]  // Its could be used in another classe just for shopping, but to the test will use the same becuse we have few functions
    [SerializeField] GameObject UIShopping;
    [SerializeField] Image selectedItem;
    [SerializeField] TextMeshProUGUI nameItem;
    [SerializeField] TextMeshProUGUI priceItem;
    [SerializeField] Button FirstItem;
    [SerializeField] Button SecondItem;
    [SerializeField] Button ThirdItem;
    [SerializeField] Image FirstShop;
    [SerializeField] Image SecondShop;
    [SerializeField] Image ThirdShop;
    [SerializeField] Clothes selectedClothe;
    [SerializeField] Button buttonBuying;

    [Header("Advice Alert")]
    [SerializeField] GameObject alertWindow;
    [SerializeField] TextMeshProUGUI titleAlert;
    [SerializeField] TextMeshProUGUI msgAlert;
    [SerializeField] Button okButtonAlert;


    List<Clothes> ShopOpened = new List<Clothes>(); 

    private void Start()
    {
        buttonShop.onClick.RemoveAllListeners();
        buttonShop.onClick.AddListener(() => OpenShop());
        
    }

    public void AlertError(string tittle, string message)
    {
        alertWindow.SetActive(true);   
        titleAlert.text = tittle;
        msgAlert.text = message;
        okButtonAlert.onClick.RemoveAllListeners();
        okButtonAlert.onClick.AddListener(() => alertWindow.gameObject.SetActive(false));
    }

    public void OpenShop()
    {
        UIShopping.SetActive(true);
        buttonShop.onClick.RemoveAllListeners();
        buttonShop.onClick.AddListener(() => CloseShop());
        PopulateShoppingItens(clotheType.Hood);
    }
    public void CloseShop()
    {
        UIShopping.SetActive(false);
        buttonShop.onClick.RemoveAllListeners();
        buttonShop.onClick.AddListener(() => OpenShop());

    }


    public void PopulateShoppingItens(clotheType type)
    {
        ShopOpened.Clear();

        List<Clothes> ClothesToShop = GameManager.Instance.clothesList.GetItensForCategory(type);
        ShopOpened = ClothesToShop;

        List<Image> imageShop = new List<Image>();  
        imageShop.Add(FirstShop); imageShop.Add(SecondShop); imageShop.Add(ThirdShop);

        List<Button> buttonShop = new List<Button>();
        buttonShop.Add(FirstItem); buttonShop.Add(SecondItem); buttonShop.Add(ThirdItem);



        for (int i = 0; i < ShopOpened.Count; i++)
        {
            imageShop[i].sprite = ShopOpened[i].iconImage;
        }
        int index = 0;  
        foreach(Clothes item in ShopOpened)
        {
            buttonShop[index].onClick.RemoveAllListeners();
            buttonShop[index].onClick.AddListener(() => SelecingItem(item)); 
            index++;
        }


        buttonBuying.onClick.RemoveAllListeners();
        buttonBuying.onClick.AddListener(() => BuyingItem());

    }


    public void SelecingItem(Clothes item)
    {
        selectedClothe = item;
        selectedItem.sprite = item.iconImage;
        nameItem.text = item.name;
        priceItem.text = item.buyingPrice.ToString();


        Debug.Log(item.name);   


    }
    public void BuyingItem()
    {
        if(selectedClothe.buyingPrice > PlayerData.Instance.playerMoney)
        {
            // alert to advice u have no enough money
            AlertError("Warning", "You have no money to do this action");
            return;
        }

        UpdateMoneyUI(-selectedClothe.buyingPrice);
        PlayerData.Instance.AddNewItem(selectedClothe); 
    }



    public void UpdateMoneyUI(float money)
    {
        float currentMoney = PlayerData.Instance.UpdateMoney(money);
        moneyDisplay.text = "$" + currentMoney.ToString();    
    }





}
