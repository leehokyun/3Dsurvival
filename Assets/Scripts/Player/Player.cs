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
        controller = GetComponent<PlayerController>(); //���� �ν�����â �ȿ� �����ϱ� �׳� ������Ʈ�� �����͹���
        condition = GetComponent<PlayerCondition>();
        equip = GetComponent<Equipment>();
    }

    public float moveSpeedUp(float speedMultiple)
    {
        moveSpeed *= speedMultiple;
        return moveSpeed;
    }
}
