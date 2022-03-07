using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ü���� ������ UI�� ó���ϰ�ʹ�.
// �¾ �� ü���� �ִ�ü�������ϰ�ʹ�.
// Player�� Enemy���� Damage�� ������ ü���� 1 ���ҽ�Ű��ʹ�.
// ü���� 0���ϰ��Ǹ� Enemy�� �ı��ϰ�ʹ�.
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
            // curHP�� value�� �����ϵ� ���� ���� ������ �ϰ�ʹ�.
            curHP = Mathf.Clamp(value, 0, maxHP);
            
            sliderHP.value = curHP;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // �¾ �� ü���� �ִ�ü�������ϰ�ʹ�.
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
