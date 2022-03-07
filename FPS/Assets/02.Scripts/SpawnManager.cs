using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;


// 레벨을 관리하고싶다.
// 적의 최대 생성 수를 레벨 이하로 제한하고싶다.
// 적이 파괴될때 killCount를 증가시키다가 레벨이상이되면 레벨업 처리를 하고싶다.
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject blueLevelUpVFXFactory;
    public GameObject purpleLevelUpVFXFactory;
    public Transform player;

    [HideInInspector]
    public int createCount;

    [HideInInspector]
    public int killCount;

    int level;
    public Text textLevel;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            textLevel.text = "Lv " + level;
        }
    }

    internal void CheckLevelUp()
    {
        // 어플리케이션이 종료되었다면 즉시 반환하고싶다.
        if (false == Application.isPlaying)
        {
            return;
        }

        StartCoroutine("IELevelUp");
    }

    IEnumerator IELevelUp()
    {
        // 만약 killCount가 NeedKillCount이상이라면 
        while (killCount >= NeedKillCount)
        {
            // 레벨업하고싶다.
            killCount -= NeedKillCount;
            createCount = 0;
            Level++;
            // TODO : 시각효과를 표현하고싶다.
            // 3항 연산자 =>   R = (조건 ? A : B);
            GameObject factory = Level % 2 == 0 ? purpleLevelUpVFXFactory : blueLevelUpVFXFactory;
            //GameObject factory;
            //if (Level % 2 == 0)
            //{
            //    factory = purpleLevelUpVFXFactory;
            //}
            //else
            //{
            //    factory = blueLevelUpVFXFactory;
            //}

            GameObject luvfx = Instantiate(factory);
            luvfx.transform.position = player.position + player.forward * 3f;
            luvfx.transform.parent = player;
            yield return new WaitForSeconds(0.2f);
        }
    }

    // 최대 생성수
    public int MaxCreateCount
    {
        get { return level; }
    }

    // 레벨업에 필요한 killCount
    public int NeedKillCount
    {
        get { return level; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;

        // 태어날 때 spawnList 배열을 채우고싶다.
        //MeshRenderer[] rs = GetComponentsInChildren<MeshRenderer>();
        //spawnList = new Transform[rs.Length];
        //for (int i = 0; i < spawnList.Length; i++)
        //{
        //    spawnList[i] = rs[i].transform;
        //}
    }

    public enum SpawnType
    {
        Normal,
        Area,
    }
    public SpawnType spawnType = SpawnType.Normal;

    void Update()
    {
        switch (spawnType)
        {
            case SpawnType.Normal: UpdateNormal(); break;
            case SpawnType.Area: UpdateArea(); break;
        }
    }

    // 일정시간마다 적을 생성하고싶다.
    private void UpdateArea()
    {
        // 1. 시간이 흐르다가
        currentTime += Time.deltaTime;
        // 2. 만약 현재시간이 생성시간을 초과하면
        if (currentTime > createTime)
        {
            // 3. 현재시간을 초기화 하고싶다.
            currentTime = 0;
            // 4. 생성범위내의 한 지점을 랜덤으로 설정하고싶다.
            Vector3 pos = GetRandomPosition();
            // 5. 그곳에 적을 생성하고싶다.
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = pos;
        }
    }

    private Vector3 GetRandomPosition(Vector3 origin, float radius)
    {
        return Vector3.zero;
    }

    public Collider spawnAreaCube;
    private Vector3 GetRandomPosition()
    {
        print("center : " + spawnAreaCube.bounds.center);
        print("size : " + spawnAreaCube.bounds.size);
        print("min : " + spawnAreaCube.bounds.min);
        print("max : " + spawnAreaCube.bounds.max);

        Vector3 min = spawnAreaCube.bounds.min;
        Vector3 max = spawnAreaCube.bounds.max;

        // 임의의 한점을 구한 후 
        float x = Random.Range(min.x, max.x);
        float y = spawnAreaCube.bounds.size.y;
        float z = Random.Range(min.z, max.z);

        // 임의의 위치에서 아래방향으로 Ray를 쏴서
        Ray ray = new Ray(new Vector3(x, y, z), Vector3.down);
        RaycastHit hitInfo;
        for (int i = 0; i < 100; i++)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 만약 부딪힌것이 Floor라면
                if (hitInfo.transform.name.Contains("Floor"))
                {
                    // 그 부딪힌 위치를 반환하고싶다.
                    return hitInfo.point;
                }
            }
        }
        return Vector3.zero;
    }

    // 일정시간마다 적을 생성하고싶다.
    // 위치목록중 하나의 위치에 랜덤하게 위치시키고싶다.
    float currentTime;// - 현재시간
    public float createTime = 1;// - 생성시간
    public GameObject enemyFactory;// - 적공장
    public Transform[] spawnList; // - 위치목록
    void UpdateNormal()
    {
        // 1. 시간이 흐르다가
        currentTime += Time.deltaTime;
        // 2. 만약 현재시간이 생성시간을 초과하면
        if (currentTime > createTime)
        {
            // 3. 현재시간을 초기화 하고싶다.
            currentTime = 0;
            // 4. 적 공장에서 적을 생성하고
            GameObject enemy = Instantiate(enemyFactory);
            // 5. 위치목록중 하나의 위치에 랜덤하게 위치시키고싶다.
            int index = Random.Range(0, spawnList.Length);
            Vector3 pos = spawnList[index].position;
            enemy.transform.position = pos;
        }



    }

    private void OnApplicationQuit()
    {
        instance = null;
    }
}
