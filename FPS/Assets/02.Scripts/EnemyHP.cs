using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 적의 체력의 변동과 UI를 처리하고싶다.
// 태어날 때 체력을 최대체력으로하고싶다.
// Player가 Enemy에게 Damage를 입히면 체력을 1 감소시키고싶다.
// 체력이 0이하가되면 Enemy를 파괴하고싶다.
public class EnemyHP : MonoBehaviour
{
    public int maxHP = 2;
    int curHP;
    public Slider sliderHP;

    public int HP
    {
        get { return curHP; }
        set
        {
            // curHP에 value를 대입하되 범위 안의 값으로 하고싶다.
            curHP = Mathf.Clamp(value, 0, maxHP);
            
            sliderHP.value = curHP;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 체력을 최대체력으로하고싶다.
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
