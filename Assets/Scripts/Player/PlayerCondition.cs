using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    

    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } } //�ܺο��� health�� �����Ϸ��ϸ� uiCondition�� �ִ� health���� ��ȯ���ش�.
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina {  get { return uiCondition.stamina; } }
    

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    // Update is called once per frame
    void Update()
    {
        // ����� ���� ���������� ��ȭ�� �ִ� �Լ�
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);


        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime); //���̻� ���� hunger���� �������� Health���� ���δ�.
        }

        if (health.curValue <= 0f)
        {
            Die();
        }    
    }

    public void SpeedUp(float amount)
    {
        CharacterManager.Instance.Player.moveSpeed = CharacterManager.Instance.Player.moveSpeedUp(amount);
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }
    public void Die()
    {
        Debug.Log("�׾���");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }



    public bool UseStamina(float amount)
    {
        if(stamina.curValue - amount < 0f)
        {
            return false;
        }

        stamina.Subtract(amount);
        return true;
    }

}
