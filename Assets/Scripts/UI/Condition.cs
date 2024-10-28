using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; //���簪
    public float startValue; //���۰�
    public float maxValue; //�ִ밪
    public float passiveValue; //�ð��� ���� ��ȭ�ϴ� ��
    public Image uiBar;

    // Start is called before the first frame update
    void Start()
    {
        curValue = startValue; //�������� �� ���簪�� ��ŸƮ���.
    }

    // Update is called once per frame
    void Update()
    {
        uiBar.fillAmount = GetPercentage();    
    }

    //ui�ٿ� �ݿ��� �ۼ�������
    float GetPercentage() 
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        //curValue += value; �̷��Ը� �ϸ� ���簪�� �ִ�HP�� �Ѿ���� ���� �ִ�. ����ó���� �������.
        curValue = Mathf.Min(curValue + value, maxValue); //2���� �Ű������� �޾Ƽ� ���߿� ���� ���� ��ȯ�ϴ� �Լ�.

    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
}
