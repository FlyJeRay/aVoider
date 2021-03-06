﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs_Controller : MonoBehaviour
{    
    // Best Score - float
    // Coins - int
    // Item buy Status - int
    // Best score functions

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Red")) SetSavedColorValue("Red", 1);
        if (!PlayerPrefs.HasKey("Green")) SetSavedColorValue("Green", 1);
        if (!PlayerPrefs.HasKey("Blue")) SetSavedColorValue("Blue", 1);
    }


    public void SetBestScore(float newBest)
    {
        PlayerPrefs.SetFloat("Best Score", newBest);
    }

    public bool CheckScoreBeat(float score)
    {
        return score > GetBestScore();
    }

    public float GetBestScore()
    {
        return PlayerPrefs.GetFloat("Best Score");
    }

        // Coins functions
    
    public void UpdateCoins(int earnedCoins)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + earnedCoins);
    }
    
    public int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins");
    }

    public void UpdateTotalCoins(int earnedCoins)
    {
        PlayerPrefs.SetInt("Total Coins", PlayerPrefs.GetInt("Total Coins") + earnedCoins);
    }

    public int GetTotalCoins()
    {
        return PlayerPrefs.GetInt("Total Coins");
    }

    public void FullStatsRecover()
    {
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetFloat("Best Score", 0);
    }

        // Shop functions
    
    public int CheckIsItemBought(string itemName)
    {
        return PlayerPrefs.GetInt(itemName);
    }

    public void SetItemBought(string itemName)
    {
        PlayerPrefs.SetInt(itemName, 1);
    }

        // Skin functions
    
    public void SetSkin(string itemName)
    {
        PlayerPrefs.SetString("Active Skin", itemName);
    }

    public string GetSkinName()
    {
        return PlayerPrefs.GetString("Active Skin");
    }

        // Custom Color functions

    public float GetSavedColorValue(string colorType)
    {
        return PlayerPrefs.GetFloat(colorType);
    }

    public void SetSavedColorValue(string colorType, float colorValue)
    {
        PlayerPrefs.SetFloat(colorType, colorValue);
    }

    public Color GetSavedColor()
    {
        return new Color(GetSavedColorValue("Red"), GetSavedColorValue("Green"), GetSavedColorValue("Blue"), 255);
    }

        // Stats functions

    public void IncreaseTotalPlayedGames()
    {
        PlayerPrefs.SetInt("Played Games", GetPlayedGames() + 1);
    }

    public int GetPlayedGames()
    {
        return PlayerPrefs.GetInt("Played Games");
    }

    public void SetAverageTimeOfLife(float newTime)
    {
        Debug.Log("This time equals " + newTime);

        if (GetPlayedGames() > 1)
        {
            float averageTime = GetAverageTime();
            Debug.Log("Average Time equals " + averageTime);

            int playedGames = GetPlayedGames();
            Debug.Log("Total games including this one equal " + playedGames);

            float TotalTime = averageTime * (playedGames - 1);
            Debug.Log("Total Time equals " + TotalTime);

            averageTime = (TotalTime + newTime) / playedGames;
            Debug.Log("New average time equals " + averageTime);

            PlayerPrefs.SetFloat("Average Time", averageTime);
        }

        else
        {
            PlayerPrefs.SetFloat("Average Time", newTime);
        }
    }

    public float GetAverageTime()
    {
        return PlayerPrefs.GetFloat("Average Time");
    }
}
