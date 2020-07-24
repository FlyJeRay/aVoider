﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Item_Script : MonoBehaviour
{
    [SerializeField] private int price;
    private string itemName;
    private Material sellingMaterial;
    
    [SerializeField] private PlayerPrefs_Controller prefsController;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Shop_Data shopData;

    private bool isBought;
    
    private void Awake()
    {
        Debug.Log("PlayerCoins: " + prefsController.GetCoins());

        sellingMaterial = transform.Find("Image").GetComponent<Image>().material;
        Debug.Log("Setted material: " + sellingMaterial);
        itemName = sellingMaterial.name;
        Debug.Log("Selling material: " + itemName);
        
        SetBoughtStatus();
        UpdateText();
    }

    private void SetBoughtStatus()
    {
        isBought = prefsController.CheckIsItemBought(itemName) != 0;
    }
    
    public void UpdateText()
    {
        // Если предмет не куплен, в текст под ним выводится его цена
        if (!isBought)
        {
            priceText.text = price.ToString();
        }

        // Если предмет куплен и является активным, в текст под ним выводится надпись "Equipped"
        else if (isBought && prefsController.GetSkinName() == itemName)
        {
            priceText.text = "Equipped";
            shopData.UpdateAnotherButtons(gameObject.GetComponent<Button>());
        }

        // В ином случае, в текст под ним выводится надпись "Equip"
        else if (isBought && prefsController.GetSkinName() != itemName)
        {
            priceText.text = "Equip";
        }
    }
    
    public void Buy()
    {
        if (!isBought && prefsController.GetCoins() > price)
        {
            prefsController.UpdateCoins(-price);
            prefsController.SetItemBought(itemName);
            Debug.Log("Item bought: " + itemName);
            SetBoughtStatus();
        }
        
        else if (isBought && prefsController.GetSkinName() != itemName)
        {
            prefsController.SetSkin(itemName);
            Debug.Log("Item equipped: " + itemName);
            Debug.Log("Current equipped item: " + prefsController.GetSkinName());
        }

        UpdateText();
    }
}