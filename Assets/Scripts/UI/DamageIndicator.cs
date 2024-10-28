using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;
    
    private Coroutine coroutine; //코루틴 변수

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }


    public void Flash()
    {
        //코루틴이 이미 기존에 있을수도 있으니 스톱 걸기.)
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        coroutine = StartCoroutine(FadeAway());

    }

    //코루틴을 쓰기 위해서는 IEnumerator 키워드를 통해 작성해야합니다.
    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f; //연속적인 변화값
        float a = startAlpha;

        while(a > 0) //빨간색이 뜨고 서서히 옅어지는 걸 표현
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f / 255f, 100f / 255f, a);
            yield return null;
        } //반환값이 있어야 코루틴 사용 가능. 반환할 게 없다면 null로 쓰면된다.

        image.enabled = false;
    }


}
