using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일정시간마다 적공장에서 적을 만들어서 내위치에 위치시키고 회전도 일치시키고 싶다.
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
        // 3. 적공장에서 적을 만들어서
        GameObject enemy = Instantiate(enemyFactory);
        // 4. 내 위치에 위치시키고
        enemy.transform.position = this.transform.position;
        // 5. 회전도 일치시키고 싶다.
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
        // 1. 시간이 흐르다가
        currentTime += Time.deltaTime;
        // 2. 만약 현재시간이 생성시간이 초과하면
        if (currentTime > createTime)
        {
            //============================================
            // 만약 생성수가 MaxCreateCount(레벨) 보다 작다면
            if (SpawnManager.instance.createCount < SpawnManager.instance.MaxCreateCount)
            {
                SpawnManager.instance.createCount++;

                // 3. 적공장에서 적을 만들어서
                GameObject enemy = Instantiate(enemyFactory);
                // 4. 내 위치에 위치시키고
                enemy.transform.position = this.transform.position;
                // 5. 회전도 일치시키고 싶다.
                enemy.transform.rotation = this.transform.rotation;
            }
            //============================================

            // 6. 현재시간을 초기화 하고싶다.
            currentTime = 0;
        }
    }
}
