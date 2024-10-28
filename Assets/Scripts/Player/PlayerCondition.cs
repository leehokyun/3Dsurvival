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

    Condition health { get { return uiCondition.health; } } //외부에서 health에 접근하려하면 uiCondition에 있는 health값을 반환해준다.
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina {  get { return uiCondition.stamina; } }
    

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    // Update is called once per frame
    void Update()
    {
        // 컨디션 값에 지속적으로 변화를 주는 함수
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);


        if (hunger.curValue <= 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime); //더이상 깎일 hunger값이 없을때는 Health값이 깎인다.
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
        Debug.Log("죽었다");
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
