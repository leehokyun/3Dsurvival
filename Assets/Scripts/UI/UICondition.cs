using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    // 값들을 지속적으로 업데이트해줄 코드.
    public Condition health;
    public Condition dashEnergy;
    public Condition jumpEnergy;
    public Condition hunger;
    public Condition stamina;


    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
