using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ٸ� ��ü�� �ε����� ���� �ݰ� 3M���� Enemy�鿡�� 3 �������� �ְ�ʹ�.
// ���߽ð�ȿ���� ǥ���ϰ�ʹ�.
public class Grenade : MonoBehaviour
{
    public float radius = 3;
    public GameObject explosionFactory;
    private void OnCollisionEnter(Collision collision)
    {
        // 1. �ݰ� 3M���� Enemy����� ã��ʹ�.
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] cols = Physics.OverlapSphere(transform.position, radius, layerMask);
        for (int i = 0; i < cols.Length; i++)
        {
            // 2. ��Ͼ��� Enemy���ӿ�����Ʈ���� Enemy������Ʈ�� ��������ʹ�.
            print(cols[i].gameObject.name);
            Enemy enemy = cols[i].GetComponent<Enemy>();
            // 3. Enemy������Ʈ���� 3 �������� �ְ�ʹ�.
            enemy.TakeDamage(3);
        }

        // ���߽ð�ȿ���� ǥ���ϰ�ʹ�.
        GameObject exp = Instantiate(explosionFactory);
        exp.transform.position = transform.position;

        // 4. ������
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
