using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("UI Buttons")]
    [SerializeField] Button buttonInventory;
    [SerializeField] Button buttonShop;
    [SerializeField] Button buttonSell;

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

    [Header("Inventory UI")]
    [SerializeField] GameObject GridItens;
    [SerializeField] GameObject InventoryWindow;
    public Image HoodEquipped;
    public Image BodyEquipped;
    public Image LegsEquipped;
    public Image HoodEquippedCharacter;
    public Image BodyEquippedCharacter;
    public Image LegsEquippedCharacter;
    [SerializeField] Button ButtonHood;
    [SerializeField] Button ButtonBody;
    [SerializeField] Button ButtonLegs;
    public Sprite UnequipedHood;
    public Sprite UnequipedBody;
    public Sprite UnequipedLegs;
    public Sprite OriginalHood;

    [Header("Selling System")]
    Clothes selectedItemToSell;
    [SerializeField] GameObject Seller;
    [SerializeField] GameObject GridSeller;
    [SerializeField] TextMeshProUGUI nameSelectedToSell;
    [SerializeField] TextMeshProUGUI priceToSell;
    [SerializeField] Image imageSelected;
    [SerializeField] Button ButtonToSell;

    [Header("UI Store")]
    [SerializeField] GameObject MenuStore;
    [SerializeField] Button ButtonStore;
    [SerializeField] Button BuyHood;
    [SerializeField] Button BuyShirt;
    [SerializeField] Button BuyPants;
    [SerializeField] Button SellItens;

    [Header("Sounds System")]
    [SerializeField] Button MuteOrNot;

    [SerializeField] RectTransform referenceHood;
    [SerializeField] RectTransform referenceItem;

    List<Clothes> ShopOpened = new List<Clothes>();

    private void Start()
    {
        ButtonStore.onClick.RemoveAllListeners();
        ButtonStore.onClick.AddListener(() => OpenMenuStore());
      

        buttonInventory.onClick.RemoveAllListeners();
        buttonInventory.onClick.AddListener(() => OpenInventory());

        ButtonHood.onClick.RemoveAllListeners();
        ButtonHood.onClick.AddListener(() => UnequipButton(PlayerData.Instance.HatRole, ButtonHood, HoodEquipped, UnequipedHood));

        ButtonBody.onClick.RemoveAllListeners();
        ButtonBody.onClick.AddListener(() => UnequipButton(PlayerData.Instance.BodyRole, ButtonBody, BodyEquipped, UnequipedBody));

        ButtonLegs.onClick.RemoveAllListeners();
        ButtonLegs.onClick.AddListener(() => UnequipButton(PlayerData.Instance.LegRole, ButtonLegs, LegsEquipped, UnequipedLegs));

        MuteOrNot.onClick.RemoveAllListeners();
        MuteOrNot.onClick.AddListener(() => MuteAllSounds());
    }
    public void MuteAllSounds()
    {
        SoundClick.Instance.SoundClicking1();
        MuteOrNot.transform.GetChild(0).gameObject.SetActive(false);
        SoundClick.Instance.MuteAll();
        MuteOrNot.onClick.RemoveAllListeners();
        MuteOrNot.onClick.AddListener(() => UnMuteAllSounds());
    }
    public void UnMuteAllSounds()
    {
        MuteOrNot.transform.GetChild(0).gameObject.SetActive(true);
        SoundClick.Instance.UnMuteAll();
        MuteOrNot.onClick.RemoveAllListeners();
        MuteOrNot.onClick.AddListener(() => MuteAllSounds());

    }
    #region Function to UI Buttons
    public void OpenMenuStore()
    {
        SoundClick.Instance.SoundClicking1();

        MenuStore.SetActive(true);

        BuyHood.onClick.RemoveAllListeners();
        BuyHood.onClick.AddListener(() => OpenShop(clotheType.Hood));

        BuyShirt.onClick.RemoveAllListeners();
        BuyShirt.onClick.AddListener(() => OpenShop(clotheType.Body));

        BuyPants.onClick.RemoveAllListeners();
        BuyPants.onClick.AddListener(() => OpenShop(clotheType.Legs));

        SellItens.onClick.RemoveAllListeners();
        SellItens.onClick.AddListener(() => OpenSeller());

    }

    public void ChangeSpriteEquipped(Image equipment, Clothes item)
    {
        equipment.sprite = item.iconImage;
    }
    public void OpenInventory()
    {
        SoundClick.Instance.SoundClicking1();
        CloseShop();
        CloseSeller();
        InventoryWindow.SetActive(true);
        PopulateInventoryItens(PlayerData.Instance.personalItens);
       // buttonInventory.onClick.RemoveAllListeners();
       // buttonInventory.onClick.AddListener(() => CloseInventory());

    }

    public void CloseInventory()
    {
        SoundClick.Instance.SoundClicking2();
        InventoryWindow.SetActive(false);
        buttonInventory.onClick.RemoveAllListeners();
        buttonInventory.onClick.AddListener(() => OpenInventory());
    }
    public void AlertError(string tittle, string message)
    {
        alertWindow.SetActive(true);
        titleAlert.text = tittle;
        msgAlert.text = message;
        okButtonAlert.onClick.RemoveAllListeners();
        okButtonAlert.onClick.AddListener(() => alertWindow.gameObject.SetActive(false));
    }

    public void OpenSeller()
    {
 
        Seller.SetActive(true);
        buttonSell.onClick.RemoveAllListeners();
        buttonSell.onClick.AddListener(() => CloseSeller());
        InventoryToSell(PlayerData.Instance.personalItens);
        ButtonToSell.onClick.RemoveAllListeners();
        ButtonToSell.onClick.AddListener(() => SellingItem(selectedItemToSell));
        CloseShop();
        CloseInventory();
        MenuStore.SetActive(false);
    }

    public void CloseSeller()
    {
        Seller.SetActive(false);
        buttonSell.onClick.RemoveAllListeners();
        buttonSell.onClick.AddListener(() => OpenSeller());
    }
    #endregion

    #region Seller System
    public void InventoryToSell(List<Clothes> clothesList)
    {

        List<GameObject> childsGridItens = new List<GameObject>();

        foreach (Transform child in GridSeller.transform)
        {
            childsGridItens.Add(child.gameObject);

        }

        int numberOfChilds = GridSeller.transform.childCount;
        int numberOfItens = clothesList.Count;
        int indexOfInventory = 0;

        foreach (GameObject child in childsGridItens)
        {
            if (indexOfInventory < numberOfItens)
            {
                child.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                child.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = clothesList[indexOfInventory].iconImage;
                child.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                int reference = indexOfInventory;
                child.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SelectionToSell(clothesList[reference]));
                //child.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => child.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false));
                //  child.transform.GetChild(0).GetComponent<Button>().onClick.  colocar função para alterar o objeto em questão
            }
            else
            {
                child.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                child.transform.GetChild(0).GetComponent<Button>().interactable = false;
            }

            indexOfInventory++;

        }

    }

    public void SelectionToSell(Clothes item)
    {

        imageSelected.gameObject.SetActive(true);

        selectedItemToSell = item;
        imageSelected.sprite = item.iconImage;
        nameSelectedToSell.text = item.name;
        priceToSell.text = item.sellingPrice.ToString();
    }

    public void SellingItem(Clothes item)
    {
        SoundClick.Instance.SoundClicking2();
        PlayerData.Instance.RemoveItem(item);

        UpdateMoneyUI(item.sellingPrice);

        nameSelectedToSell.text = " ";
        priceToSell.text = " ";

        imageSelected.gameObject.SetActive(false);
        selectedItemToSell = null;
        InventoryToSell(PlayerData.Instance.personalItens);




    }
    #endregion 

    #region Buying System

    public void OpenShop(clotheType type)
    {
        

        selectedItem.gameObject.SetActive(false);
        nameItem.text = " ";
        priceItem.text = " ";

        CloseInventory();
        CloseSeller();
        UIShopping.SetActive(true);
        buttonShop.onClick.RemoveAllListeners();
        buttonShop.onClick.AddListener(() => CloseShop());
        PopulateShoppingItens(type);
        MenuStore.SetActive(false);

    }
    public void CloseShop()
    {
        

        UIShopping.SetActive(false);
       // buttonShop.onClick.RemoveAllListeners();
       // buttonShop.onClick.AddListener(() => OpenShop());

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
            if (type == clotheType.Hood)
            {
                imageShop[i].GetComponent<RectTransform>().anchoredPosition = referenceHood.anchoredPosition;
                imageShop[i].GetComponent<RectTransform>().localScale = referenceHood.localScale;

            }
            else
            {
                imageShop[i].GetComponent<RectTransform>().anchoredPosition = referenceItem.anchoredPosition;
                imageShop[i].GetComponent<RectTransform>().localScale = referenceItem.localScale;

            }


        }

        int index = 0;
        foreach (Clothes item in ShopOpened)
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
        SoundClick.Instance.SoundClicking1();

        selectedItem.gameObject.SetActive(true);
        selectedClothe = item;
        selectedItem.sprite = item.iconImage;
        nameItem.text = item.name;
        priceItem.text = item.buyingPrice.ToString();


        Debug.Log(item.name);


    }
    public void BuyingItem()
    {
        if (selectedClothe.buyingPrice > PlayerData.Instance.playerMoney)
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

    #endregion

    #region Inventory System


    public void UnequipButton(Clothes item, Button bt, Image img, Sprite part)
    {
        SoundClick.Instance.SoundClicking2();

        if (item.name.Length < 2)
        {
            PlayerData.Instance.UpdatePersonalItens();
            return;
        }

        // PlayerData.Instance.AddNewItem(item);
        UnequipingItem(item);
        img.sprite = part;

        Color newColor = img.color;
        newColor.a = 0.5f;
        img.color = newColor;

        bt.interactable = false;
        PlayerData.Instance.RemoveEquippedIten(item);
        PopulateInventoryItens(PlayerData.Instance.personalItens);


        clotheType typeItem = item.part;

        switch (typeItem)
        {
            case clotheType.Hood:

                HoodEquipped.sprite = UnequipedHood;
                HoodEquippedCharacter.sprite = OriginalHood;

                Sprite newSprite = OriginalHood;
                PlayerData.Instance.hoodPart.sprite = newSprite;

  
                break;

            case clotheType.Body:

                BodyEquipped.sprite = UnequipedBody;
                BodyEquippedCharacter.sprite = UnequipedBody;

                Sprite newSprite2 = UnequipedBody;
                PlayerData.Instance.bodyPart.sprite = newSprite2;
                break;

            case clotheType.Legs:
                LegsEquipped.sprite = UnequipedLegs;
                LegsEquippedCharacter.sprite = UnequipedLegs;

                Sprite newSprite3 = UnequipedLegs;
                PlayerData.Instance.legsPart.sprite = newSprite3;
                break;

        }
    }
    public void PopulateInventoryItens(List<Clothes> clothesList)
    {
        
        List<GameObject> childsGridItens = new List<GameObject>();

        foreach (Transform child in GridItens.transform)
        {
            childsGridItens.Add(child.gameObject);
        }
        foreach (GameObject button in childsGridItens)
        {
            button.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
        int numberOfChilds = GridItens.transform.childCount;
       // int numberOfItens = clothesList.Count;
        int indexOfInventory = 0;

        foreach (GameObject child in childsGridItens)
        {
            if (indexOfInventory < PlayerData.Instance.personalItens.Count)
            {
                child.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                child.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = PlayerData.Instance.personalItens[indexOfInventory].iconImage;
                child.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                int reference = indexOfInventory;
                child.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => InventoryToEquip(PlayerData.Instance.personalItens[reference]));
                child.transform.GetChild(0).GetComponent<Button>().interactable = true;
                if(PlayerData.Instance.personalItens[reference].part == clotheType.Hood)
                {
                    child.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = referenceHood.anchoredPosition;
                    child.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale = referenceHood.localScale;

                }
                else
                {
                    child.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition = referenceItem.anchoredPosition;
                    child.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale = referenceItem.localScale;

                }
                // child.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => child.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false));
                //  child.transform.GetChild(0).GetComponent<Button>().onClick.  colocar função para alterar o objeto em questão
            }
            else
            {
                child.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
              //  child.transform.GetChild(0).GetComponent<Button>().interactable= false; 
            }

            indexOfInventory++;

        }



    }

    public void InventoryToEquip(Clothes item)
    {
        
      //  UnequipingItem(item);
      //
      EquipingItem(item);

        PlayerData.Instance.UpdatePersonalItens();
        PopulateInventoryItens(PlayerData.Instance.personalItens);


    }

    public void EquipingItem(Clothes item)
    {

        clotheType typeItem = item.part;
        int indexOf = 0;
        foreach (Clothes equip in PlayerData.Instance.personalItens)
        {
            if (equip.name == item.name && equip.name.Length > 2)
            {

                switch (typeItem)
                {
                    case clotheType.Hood:
                        PlayerData.Instance.ChangeHood(item);
                        Console.WriteLine("Você escolheu o número 1.");
                        HoodEquipped.gameObject.SetActive(true);
                        HoodEquippedCharacter.sprite = item.ingameImage;

                        ButtonHood.interactable = true;
                        Color newColor = HoodEquipped.color;
                        newColor.a = 1f;
                        HoodEquipped.color = newColor;



                        break;

                    case clotheType.Body:
                        PlayerData.Instance.ChangeBody(item);
                        Console.WriteLine("Você escolheu o número 2.");
                        BodyEquipped.gameObject.SetActive(true);
                        BodyEquippedCharacter.sprite = item.ingameImage;

                        ButtonBody.interactable = true;
                        Color newColor1 = BodyEquipped.color;
                        newColor1.a = 1f;
                        BodyEquipped.color = newColor1;


                        break;

                    case clotheType.Legs:
                        PlayerData.Instance.ChangeLegs(item);
                        Console.WriteLine("Você escolheu o número 3.");
                        LegsEquipped.gameObject.SetActive(true);
                        LegsEquippedCharacter.sprite = item.ingameImage;

                        ButtonLegs.interactable = true;
                        Color newColor2 = LegsEquipped.color;
                        newColor2.a = 1f;
                        LegsEquipped.color = newColor2;


                        break;

                }


                // PlayerData.Instance.personalItens.RemoveAt(indexOf);
                return;

            }
            indexOf++;
        }
    }

    public void UnequipingItem(Clothes item)
    {
        

        clotheType typeItem = item.part;
        Clothes itemRemoved;

        switch (typeItem)
        {
            case clotheType.Hood:

                if(PlayerData.Instance.HatRole.name.Length < 2) return;

                itemRemoved = PlayerData.Instance.HatRole;
                PlayerData.Instance.AddNewItem(itemRemoved);
                break;

            case clotheType.Body:
                if (PlayerData.Instance.BodyRole.name.Length < 2) return;
                itemRemoved = PlayerData.Instance.BodyRole;
                PlayerData.Instance.AddNewItem(itemRemoved);
                break;

            case clotheType.Legs:
                if (PlayerData.Instance.LegRole.name.Length < 2) return;
                itemRemoved = PlayerData.Instance.LegRole;
                PlayerData.Instance.AddNewItem(itemRemoved);
                break;


        }


        PlayerData.Instance.UpdatePersonalItens();

    }
    #endregion
}
