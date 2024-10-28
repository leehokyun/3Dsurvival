using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public Equipment equip;

    public ItemData itemData;
    public Action addItem;

    public float moveSpeed;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>(); //같은 인스펙터창 안에 있으니까 그냥 컴포넌트를 가져와버림
        condition = GetComponent<PlayerCondition>();
        equip = GetComponent<Equipment>();
    }

    public float moveSpeedUp(float speedMultiple)
    {
        moveSpeed *= speedMultiple;
        return moveSpeed;
    }
}
