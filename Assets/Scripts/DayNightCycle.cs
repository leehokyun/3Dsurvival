using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f; //time이 0.5일때가 정오(12시) 해가 가장 높을 때. 각도는 90도.
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
        //왜 0.25인가? time이 0.5라면 정오이고 그때 각도가 90도여야한다.360도에 0.5면 180도가 나온다. 그래서 0.25를 빼줘서 90도가 나오게 해야함.   
        //여기의 정오의 각도(noon)를 곱해주고 time 0.5일때 12시 -> 90도에 있어야하는데. 90도에다가 0.25곱하면 90이 안나오니까 4를 곱해주면 정오시간을 계산해주는 각도계산 완료.
        lightSource.color = gradient.Evaluate(time);//time에 0%~100%사이의 일정한 값이 들어갈텐데 그에 맞춰 컬러가 변화,
        lightSource.intensity = intensity;


        //해 꺼주고 켜주기
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
