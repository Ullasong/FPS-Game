using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 다른 물체와 부딪히는 순간 반경 3M내의 Enemy들에게 3 데미지를 주고싶다.
// 폭발시각효과도 표현하고싶다.
public class Grenade : MonoBehaviour
{
    public float radius = 3;
    public GameObject explosionFactory;
    private void OnCollisionEnter(Collision collision)
    {
        // 1. 반경 3M내의 Enemy목록을 찾고싶다.
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, layerMask);
        for (int i = 0; i < cols.Length; i++)
        {
            // 2. 목록안의 Enemy게임오브젝트에서 Enemy컴포넌트를 가져오고싶다.
            print(cols[i].gameObject.name);
            Enemy enemy = cols[i].GetComponent<Enemy>();
            // 3. Enemy컴포넌트에게 3 데미지를 주고싶다.
            enemy.TakeDamage(3);
        }

        // 폭발시각효과도 표현하고싶다.
        GameObject exp = Instantiate(explosionFactory);
        exp.transform.position = transform.position;

        // 4. 나죽자
        Destroy(gameObject);
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
