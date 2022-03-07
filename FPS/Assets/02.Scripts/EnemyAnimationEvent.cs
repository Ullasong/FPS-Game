using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Enemy enemy;

    public void OnDeathFinished()
    {
        enemy.OnDeathFinished();
    }

    public void OnDamageFinished()
    {
        enemy.OnDamageFinished();
    }


    // 타격순간 (Enemy -> Player)
    public void OnAttackHit()
    {
        enemy.OnAttackHit();
    }

    // 공격동작종료순간
    public void OnAttackFinished()
    {
        enemy.OnAttackFinished();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
