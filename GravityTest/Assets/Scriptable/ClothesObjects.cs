using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public enum clotheType {Hood, Legs, Body}


[System.Serializable]
public class Clothes
{
    public string name;
    public clotheType part;
    public float buyingPrice;
    public float sellingPrice;
    [TextArea(1, 30)]
    public string description;
    public Sprite iconImage;
    public Sprite ingameImage;


}


[CreateAssetMenu(fileName = "Clothes", menuName = "ItensEquipable")]

public class ClothesObjects : ScriptableObject
{
  
    public List<Clothes> clothes;

    public List<Clothes> PegarItensPorCategoria(clotheType category)
    {
        return clothes.Where(X => X.part == category).ToList();
    }

}
