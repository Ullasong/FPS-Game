using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ڰ� ���콺 ���ʹ�ư�� ������ ī�޶���ġ���� ī�޶�չ������� Ray�� ����� �ε��� ���� �Ѿ��ڱ����忡�� �Ѿ��ڱ������� �� ��ġ�� ��ġ�ϰ�ʹ�.
// �Ѿ��� �����ϰ�ʹ�.
public class Gun : MonoBehaviour
{
    public Rifle rifle;

    public GameObject bulletImpactFactory;
    // Start is called before the first frame update
    void Start()
    {
        gunTarget = zoomOutPosition.localPosition;
    }

    private void Update()
    {
        UpdateZoom();
        UpdateShoot();
        UpdateThrowGrenade();
    }

    public float zoomInValue = 10f;
    public float zoomOutValue = 60f;
    public float zoomInSpeed = 20;
    public float zoomOutSpeed = 5;
    float fovTarget = 60f;
    float zoomTargetSpeed = 5;
    public Transform gun;
    public Transform zoomInPosition;
    public Transform zoomOutPosition;
    Vector3 gunTarget;
    private void UpdateZoom()
    {
        // ���� ���콺 ���ʹ�ư�� ������������ ZoomIn
        if (Input.GetButton("Fire2"))
        {
            fovTarget = zoomInValue;
            zoomTargetSpeed = zoomInSpeed;
            gunTarget = zoomInPosition.localPosition;
        }
        // �׷��� �ʰ� ���콺 ���� ��ư�� ���� ZoomOut
        else if (Input.GetButtonUp("Fire2"))
        {
            fovTarget = zoomOutValue;
            zoomTargetSpeed = zoomOutSpeed;
            gunTarget = zoomOutPosition.localPosition;
        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovTarget, Time.deltaTime * zoomTargetSpeed);

        gun.localPosition = Vector3.Lerp(gun.localPosition, gunTarget, Time.deltaTime * zoomTargetSpeed);
    }

    public GameObject grenadeFactory;
    public float throwPower = 10;
    private void UpdateThrowGrenade()
    {
        // 0. ���� G��ư�� ������
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 1. ��ź���忡�� ��ź�� �����
            GameObject grenade = Instantiate(grenadeFactory);
            // 2. ��ź�� �ѱ���ġ�� �����ٳ���
            grenade.transform.position = transform.position;
            // 3. ��ź���Լ� Rigidbody������Ʈ�� �����ͼ�
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            // 4. Rigidbody�� �� ���� 45�� �������� ���� ���ϰ� �ʹ�.
            Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up;
            dir.Normalize();
            rb.AddForce(dir * throwPower, ForceMode.Impulse);
            rb.AddTorque(-transform.right * 50, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void UpdateShoot()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rifle.Reload();
        }

        // 1. ����ڰ� ���콺 ���ʹ�ư�� ������
        if (Input.GetButtonDown("Fire1") && rifle.CanShoot())
        {
            rifle.Shoot();
            // 2. ī�޶���ġ���� ī�޶�չ������� Ray�� �����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 3. ���� �ٶ� �� �ε����ٸ�
            RaycastHit hitInfo;
            int layerMask = ~(1 << LayerMask.NameToLayer("EnemyDeath"));
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
            {
                // 4. �ε��� ���� �Ѿ��ڱ����忡�� �Ѿ��ڱ�������
                GameObject bi = Instantiate(bulletImpactFactory);
                // 5. �� ��ġ�� ��ġ�ϰ�ʹ�.
                bi.transform.position = hitInfo.point;
                bi.transform.forward = hitInfo.normal;

                // Player�� Enemy���� Damage�� ������ 
                // ���� �ε������� Enemy��� EnemyHP������Ʈ�� �����ͼ�
                if (hitInfo.transform.name.Contains("Enemy"))
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    enemy.TakeDamage(1);
                }
            }
        }
    }
}
