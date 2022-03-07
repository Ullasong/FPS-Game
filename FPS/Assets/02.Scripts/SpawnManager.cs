using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;


// ������ �����ϰ�ʹ�.
// ���� �ִ� ���� ���� ���� ���Ϸ� �����ϰ�ʹ�.
// ���� �ı��ɶ� killCount�� ������Ű�ٰ� �����̻��̵Ǹ� ������ ó���� �ϰ�ʹ�.
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
        // ���ø����̼��� ����Ǿ��ٸ� ��� ��ȯ�ϰ�ʹ�.
        if (false == Application.isPlaying)
        {
            return;
        }

        StartCoroutine("IELevelUp");
    }

    IEnumerator IELevelUp()
    {
        // ���� killCount�� NeedKillCount�̻��̶�� 
        while (killCount >= NeedKillCount)
        {
            // �������ϰ�ʹ�.
            killCount -= NeedKillCount;
            createCount = 0;
            Level++;
            // TODO : �ð�ȿ���� ǥ���ϰ�ʹ�.
            // 3�� ������ =>   R = (���� ? A : B);
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

    // �ִ� ������
    public int MaxCreateCount
    {
        get { return level; }
    }

    // �������� �ʿ��� killCount
    public int NeedKillCount
    {
        get { return level; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;

        // �¾ �� spawnList �迭�� ä���ʹ�.
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

    // �����ð����� ���� �����ϰ�ʹ�.
    private void UpdateArea()
    {
        // 1. �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;
        // 2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            // 3. ����ð��� �ʱ�ȭ �ϰ�ʹ�.
            currentTime = 0;
            // 4. ������������ �� ������ �������� �����ϰ�ʹ�.
            Vector3 pos = GetRandomPosition();
            // 5. �װ��� ���� �����ϰ�ʹ�.
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

        // ������ ������ ���� �� 
        float x = Random.Range(min.x, max.x);
        float y = spawnAreaCube.bounds.size.y;
        float z = Random.Range(min.z, max.z);

        // ������ ��ġ���� �Ʒ��������� Ray�� ����
        Ray ray = new Ray(new Vector3(x, y, z), Vector3.down);
        RaycastHit hitInfo;
        for (int i = 0; i < 100; i++)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                // ���� �ε������� Floor���
                if (hitInfo.transform.name.Contains("Floor"))
                {
                    // �� �ε��� ��ġ�� ��ȯ�ϰ�ʹ�.
                    return hitInfo.point;
                }
            }
        }
        return Vector3.zero;
    }

    // �����ð����� ���� �����ϰ�ʹ�.
    // ��ġ����� �ϳ��� ��ġ�� �����ϰ� ��ġ��Ű��ʹ�.
    float currentTime;// - ����ð�
    public float createTime = 1;// - �����ð�
    public GameObject enemyFactory;// - ������
    public Transform[] spawnList; // - ��ġ���
    void UpdateNormal()
    {
        // 1. �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;
        // 2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            // 3. ����ð��� �ʱ�ȭ �ϰ�ʹ�.
            currentTime = 0;
            // 4. �� ���忡�� ���� �����ϰ�
            GameObject enemy = Instantiate(enemyFactory);
            // 5. ��ġ����� �ϳ��� ��ġ�� �����ϰ� ��ġ��Ű��ʹ�.
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
