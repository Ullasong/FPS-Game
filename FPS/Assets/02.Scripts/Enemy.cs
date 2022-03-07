using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// agent�� �̿��ؼ� �÷��̾ ���ؼ� �̵��ϰ�ʹ�. 
public class Enemy : MonoBehaviour
{
    // ������
    public enum State
    {
        Search,
        Move,
        Attack,
        Damage,
        Death,
    }

    // Death �ִϸ��̼��� �Ϸ�Ǹ� Enemy�� �ı��ϰ�ʹ�.
    internal void OnDeathFinished()
    {
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        if (SpawnManager.instance != null)
        {
            // Enemy�� �ı��� �� killCount�� �ϳ� ������Ű��ʹ�.
            SpawnManager.instance.killCount++;
            SpawnManager.instance.CheckLevelUp();
        }
    }

    internal void OnDamageFinished()
    {
        // ���� state�� Death���
        if (state == State.Death)
        {
            // state�� Death�� �ٲ������ �ִϸ��̼��� ���������̴�.
            // Death �ִϸ��̼��� ����ϰ�ʹ�.
            anim.SetTrigger("Death");
            // �Լ��� �ٷ� �����ϰ�ʹ�.
            return;
        }

        // ���� Ÿ�ٰ� ���� �Ÿ��� ���� ���� �Ÿ���� 
        float dist = Vector3.Distance(transform.position, player.transform.position);
        // ���ݻ��·� �����ϰ�
        if (dist < attackDistance)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
        // �׷��� �ʴٸ� �̵����·� �����ϰ�ʹ�.
        else
        {
            state = State.Move;
            anim.SetTrigger("Move");
            // agent�� �������� ��� �ϰ� �ʹ�.
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
        // Ÿ���� �ﰢ �ٶ󺸰�ʹ�.
        //transform.LookAt(player.transform);

        // Ÿ���� �ε巴�� �ٶ󺸰�ʹ�.
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
    }

    // ���� ���ɰŸ�
    public float attackDistance = 2f;
    private void UpdateMove()
    {
        agent.destination = player.transform.position;
        // ���� Ÿ�ٰ��� �Ÿ��� ���ݰ��� �Ÿ����� �۴ٸ�
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            // ���ݻ��·� �����ϰ�ʹ�.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    EnemyHP ehp;
    internal void TakeDamage(int dmgAmount)
    {
        // ���� state�� Death���
        if (state == State.Death)
        {
            // �Լ��� �ٷ� �����ϰ�ʹ�.
            return;
        }

        if (ehp == null)
        {
            ehp = gameObject.GetComponent<EnemyHP>();
        }

        if (ehp != null)
        {
            // agent�� ���� ��� �ϰ� �ʹ�.
            agent.isStopped = true;

            // ü���� dmgAmount��ŭ ���ҽ�Ű��ʹ�.
            ehp.HP = ehp.HP - dmgAmount;
            // ü���� 0���ϰ��Ǹ� Enemy�� �ı��ϰ�ʹ�.
            if (ehp.HP <= 0)
            {
                // �������·� �����ϰ�ʹ�.
                state = State.Death;
                anim.SetTrigger("Death");
                gameObject.layer = LayerMask.NameToLayer("EnemyDeath");
            }
            else
            {
                // ���������·� �����ϰ�ʹ�.
                state = State.Damage;
                anim.SetTrigger("Damage");
            }
        }

    }

    private void UpdateSearch()
    {
        // ���� ã��ʹ�. 
        player = GameObject.Find("Player");
        // ���� ���� ã�Ҵٸ�
        if (player != null)
        {
            // �̵����·� �����ϰ�ʹ�.
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }

    public int damageAmount = 1;
    // Ÿ�ݼ��� (Enemy -> Player)
    public void OnAttackHit()
    {
        print("OnAttackHit");
        // ���� Ÿ���� ���� �����Ÿ� �ȿ� �ִٸ�
        // Hit�� �ϰ�ʹ�.
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            HitManager.instance.DoHit(damageAmount, () => {
              
                // ��½�� ������ �����Եȴ�.

            });
        }
    }

   


    // ���ݵ����������
    public void OnAttackFinished()
    {
        print("OnAttackFinished");
        // ���� Ÿ���� ���� �����Ÿ� �ۿ��ִٸ�
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist >= attackDistance)
        {
            // �̵����·� �����ϰ�ʹ�.
            state = State.Move;
            //anim.SetTrigger("Move");
            //anim.Play("Move", 0, 0);
            anim.CrossFade("Move", 0.1f, 0);
        }
    }
}
