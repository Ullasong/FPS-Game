using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 플레이어의 체력의 변동과 UI를 처리하고싶다.
// 태어날 때 체력을 최대체력으로하고싶다.
// Enemy가 Player를 Hit하면 체력을 1 감소시키고싶다.
// 체력이 0이하가되면 게임오버 처리하고싶다.
public class PlayerHP : MonoBehaviour
{
    int maxHP = 10;
    int curHP;
    public Slider sliderHP;

    public int HP
    {
        get { return curHP; }
        set
        {
            curHP = value;
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
