using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; //현재값
    public float startValue; //시작값
    public float maxValue; //최대값
    public float passiveValue; //시간에 따라 변화하는 값
    public Image uiBar;

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue; //시작했을 때 현재값은 스타트밸류.
    }

    // Update is called once per frame
    void Update()
    {
        uiBar.fillAmount = GetPercentage();    
    }

    //ui바에 반영할 퍼센테이지
    float GetPercentage() 
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        //curValue += value; 이렇게만 하면 현재값이 최대HP를 넘어버릴 수도 있다. 예외처리를 해줘야함.
        curValue = Mathf.Min(curValue + value, maxValue); //2개의 매개변수를 받아서 그중에 작은 값을 반환하는 함수.

    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
