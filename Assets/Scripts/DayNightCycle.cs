using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f; //time�� 0.5�϶��� ����(12��) �ذ� ���� ���� ��. ������ 90��.
    private float timeRate;
    public Vector3 noon; //Vector 90 0 0

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient gradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4f;
        //�� 0.25�ΰ�? time�� 0.5��� �����̰� �׶� ������ 90�������Ѵ�.360���� 0.5�� 180���� ���´�. �׷��� 0.25�� ���༭ 90���� ������ �ؾ���.   
        //������ ������ ����(noon)�� �����ְ� time 0.5�϶� 12�� -> 90���� �־���ϴµ�. 90�����ٰ� 0.25���ϸ� 90�� �ȳ����ϱ� 4�� �����ָ� �����ð��� ������ִ� ������� �Ϸ�.
        lightSource.color = gradient.Evaluate(time);//time�� 0%~100%������ ������ ���� ���ٵ� �׿� ���� �÷��� ��ȭ,
        lightSource.intensity = intensity;


        //�� ���ְ� ���ֱ�
        GameObject go = lightSource.gameObject; 
        if(lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if(lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
