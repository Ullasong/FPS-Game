using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// agent를 이용해서 플레이어를 향해서 이동하고싶다. 
public class Enemy : MonoBehaviour
{
    // 열거형
    public enum State
    {
        Search,
        Move,
        Attack,
        Damage,
        Death,
    }

    // Death 애니메이션이 완료되면 Enemy를 파괴하고싶다.
    internal void OnDeathFinished()
    {
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        if (SpawnManager.instance != null)
        {
            // Enemy가 파괴될 때 killCount를 하나 증가시키고싶다.
            SpawnManager.instance.killCount++;
            SpawnManager.instance.CheckLevelUp();
        }
    }

    internal void OnDamageFinished()
    {
        // 만약 state가 Death라면
        if (state == State.Death)
        {
            // state는 Death로 바뀌었지만 애니메이션이 씹힌상태이다.
            // Death 애니메이션을 재생하고싶다.
            anim.SetTrigger("Death");
            // 함수를 바로 종료하고싶다.
            return;
        }

        // 만약 타겟과 나의 거리가 공격 가능 거리라면 
        float dist = Vector3.Distance(transform.position, player.transform.position);
        // 공격상태로 전이하고
        if (dist < attackDistance)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
        // 그렇지 않다면 이동상태로 전이하고싶다.
        else
        {
            state = State.Move;
            anim.SetTrigger("Move");
            // agent야 멈추지마 라고 하고 싶다.
            agent.isStopped = false;
        }
    }

    public State state;

    GameObject player;
    public float speed = 5;
    NavMeshAgent agent;
    public Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.Search;
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Search: UpdateSearch(); break;
            case State.Move: UpdateMove(); break;
            case State.Attack: UpdateAttack(); break;
            default: break;
        }
    }

    private void UpdateAttack()
    {
        // 타겟을 즉각 바라보고싶다.
        //transform.LookAt(player.transform);

        // 타겟을 부드럽게 바라보고싶다.
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
    }

    // 공격 가능거리
    public float attackDistance = 2f;
    private void UpdateMove()
    {
        agent.destination = player.transform.position;
        // 만약 타겟과의 거리가 공격가능 거리보다 작다면
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            // 공격상태로 전이하고싶다.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    EnemyHP ehp;
    internal void TakeDamage(int dmgAmount)
    {
        // 만약 state가 Death라면
        if (state == State.Death)
        {
            // 함수를 바로 종료하고싶다.
            return;
        }

        if (ehp == null)
        {
            ehp = gameObject.GetComponent<EnemyHP>();
        }

        if (ehp != null)
        {
            // agent야 멈춰 라고 하고 싶다.
            agent.isStopped = true;

            // 체력을 dmgAmount만큼 감소시키고싶다.
            ehp.HP = ehp.HP - dmgAmount;
            // 체력이 0이하가되면 Enemy를 파괴하고싶다.
            if (ehp.HP <= 0)
            {
                // 죽음상태로 전이하고싶다.
                state = State.Death;
                anim.SetTrigger("Death");
                gameObject.layer = LayerMask.NameToLayer("EnemyDeath");
            }
            else
            {
                // 데미지상태로 전이하고싶다.
                state = State.Damage;
                anim.SetTrigger("Damage");
            }
        }

    }

    private void UpdateSearch()
    {
        // 적을 찾고싶다. 
        player = GameObject.Find("Player");
        // 만약 적을 찾았다면
        if (player != null)
        {
            // 이동상태로 전이하고싶다.
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }

    public int damageAmount = 1;
    // 타격순간 (Enemy -> Player)
    public void OnAttackHit()
    {
        print("OnAttackHit");
        // 만약 타겟이 공격 사정거리 안에 있다면
        // Hit를 하고싶다.
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            HitManager.instance.DoHit(damageAmount, () => {
              
                // 번쩍이 끝나면 들어오게된다.

            });
        }
    }

   


    // 공격동작종료순간
    public void OnAttackFinished()
    {
        print("OnAttackFinished");
        // 만약 타겟이 공격 사정거리 밖에있다면
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist >= attackDistance)
        {
            // 이동상태로 전이하고싶다.
            state = State.Move;
            //anim.SetTrigger("Move");
            //anim.Play("Move", 0, 0);
            anim.CrossFade("Move", 0.1f, 0);
        }
    }
}
