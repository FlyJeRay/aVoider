﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Item_Script : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private string itemName;
    private Material sellingMaterial;
    [SerializeField] private Image itemImage;
    
    [SerializeField] private PlayerPrefs_Controller prefsController;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Shop_Data shopData;

    private bool isBought;
    
    private void Start()
    {
        itemImage.gameObject.transform.position = new Vector3(itemImage.transform.position.x, itemImage.transform.position.y, -1);

        Debug.Log("PlayerCoins: " + prefsController.GetCoins());

        sellingMaterial = transform.Find("Image").GetComponent<Image>().material;
        Debug.Log("Setted material: " + sellingMaterial);
        itemName = sellingMaterial.name;
        Debug.Log("Selling material: " + itemName);

        if (itemName != "WhiteMaterial" && itemName != "CustomMaterial") SetBoughtStatus();
        else isBought = true;

        if (itemImage.material.name == "Reach50Material")
        {
            float bestScore = prefsController.GetBestScore();

            if (bestScore >= 50) isBought = true;
            else isBought = false;
        }

        if (itemImage.material.name == "CustomMaterial")
        {
            Material newSettedMaterial = itemImage.material;

            Color color = prefsController.GetSavedColor();

            newSettedMaterial.color = color;
            newSettedMaterial.SetColor("_EmissionColor", color);
            newSettedMaterial.SetColor("_Color", color);

            itemImage.material = newSettedMaterial;
        }

        UpdateText();
        shopData.UpdateCoinsText(prefsController.GetCoins());
    }

    private void SetBoughtStatus()
    {
        isBought = prefsController.CheckIsItemBought(itemName) != 0;
    }
    
    public void UpdateText()
    {
        // Если предмет не куплен, в текст под ним выводится его цена
        if (!isBought && itemName != "Reach50Material")
        {
            priceText.text = price.ToString();
        }

        else if (!isBought && itemName == "Reach50Material")
        {
            priceText.text = "Reach 50";
        }

        // Если предмет куплен и является активным, в текст под ним выводится надпись "Equipped"
        else if (isBought && prefsController.GetSkinName() == itemName)
        {
            priceText.text = "Equipped";
            shopData.UpdateAnotherButtons(gameObject.GetComponent<Button>());
        }

        // В ином случае, в текст под ним выводится надпись "Equip"
        else if (isBought && prefsController.GetSkinName() != itemName && itemName != "CustomMaterial")
        {
            priceText.text = "Equip";
        }

        else if (isBought && prefsController.GetSkinName() != itemName && itemName == "CustomMaterial")
        {
            priceText.text = "Your Custom Color";
        }
    }
    
    public void Buy()
    {
        if (!isBought && prefsController.GetCoins() >= price && itemName != "Reach50Material")
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
        shopData.UpdateCoinsText(prefsController.GetCoins());
    }
}
