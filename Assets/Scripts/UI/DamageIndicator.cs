using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;
    
    private Coroutine coroutine; //�ڷ�ƾ ����

    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }


    public void Flash()
    {
        //�ڷ�ƾ�� �̹� ������ �������� ������ ���� �ɱ�.)
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        coroutine = StartCoroutine(FadeAway());

    }

    //�ڷ�ƾ�� ���� ���ؼ��� IEnumerator Ű���带 ���� �ۼ��ؾ��մϴ�.
    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f; //�������� ��ȭ��
        float a = startAlpha;

        while(a > 0) //�������� �߰� ������ �������� �� ǥ��
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f / 255f, 100f / 255f, a);
            yield return null;
        } //��ȯ���� �־�� �ڷ�ƾ ��� ����. ��ȯ�� �� ���ٸ� null�� ����ȴ�.

        image.enabled = false;
    }


}
