using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ð����� �����忡�� ���� ���� ����ġ�� ��ġ��Ű�� ȸ���� ��ġ��Ű�� �ʹ�.
public class EnemyManager : MonoBehaviour
{
    float currentTime;
    public float createTime = 1;
    public GameObject enemyFactory;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("IECreateEnemy");
        //Invoke("CreateEnemy", createTime);
    }

    void CreateEnemy()
    {
        // 3. �����忡�� ���� ����
        GameObject enemy = Instantiate(enemyFactory);
        // 4. �� ��ġ�� ��ġ��Ű��
        enemy.transform.position = this.transform.position;
        // 5. ȸ���� ��ġ��Ű�� �ʹ�.
        enemy.transform.rotation = this.transform.rotation;
        Invoke("CreateEnemy", createTime);
    }


    IEnumerator IECreateEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(createTime);
            CreateEnemy();
        }
    }


    // Update is called once per frame
    void Update()
    {
        // 1. �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;
        // 2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            //============================================
            // ���� �������� MaxCreateCount(����) ���� �۴ٸ�
            if (SpawnManager.instance.createCount < SpawnManager.instance.MaxCreateCount)
            {
                SpawnManager.instance.createCount++;

                // 3. �����忡�� ���� ����
                GameObject enemy = Instantiate(enemyFactory);
                // 4. �� ��ġ�� ��ġ��Ű��
                enemy.transform.position = this.transform.position;
                // 5. ȸ���� ��ġ��Ű�� �ʹ�.
                enemy.transform.rotation = this.transform.rotation;
            }
            //============================================

            // 6. ����ð��� �ʱ�ȭ �ϰ�ʹ�.
            currentTime = 0;
        }
    }
}
