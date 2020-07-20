﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Требование компонента типа Player_DataContainer для корректной игры
[RequireComponent(typeof(Player_DataContainer))]
public class Player_Mechanics : MonoBehaviour
{
    [HideInInspector] public Player_DataContainer playerData;
    [SerializeField] private Game_Controller gameController;

    private void Awake()
    {
        playerData = GetComponent<Player_DataContainer>();       

        transform.position = new Vector2(0, playerData.centerOfRotation.position.y - playerData.distanceToCenter);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) playerData.isClockwiseDirectioned = !playerData.isClockwiseDirectioned;
    }

    private void FixedUpdate()
    {
        // Направление вращения игрока, зависящее от bool в Player_DataContainer
        if (playerData.hp <= 0) gameController.EndGame();

        Vector3 direction;        

        if (playerData.isClockwiseDirectioned) direction = Vector3.forward;
        else direction = Vector3.back;

        transform.RotateAround(playerData.centerOfRotation.position, direction, playerData.speed);
    }

    public void GetDamage()
    {
        playerData.hp--;
        gameController.UpdateHPText();
    }
}
