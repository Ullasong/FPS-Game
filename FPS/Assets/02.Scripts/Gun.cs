using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 마우스 왼쪽버튼을 누르면 카메라위치에서 카메라앞방향으로 Ray를 만들고 부딪힌 곳에 총알자국공장에서 총알자국을만들어서 그 위치에 배치하고싶다.
// 총알을 제한하고싶다.
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
        // 만약 마우스 왼쪽버튼을 누르고있으면 ZoomIn
        if (Input.GetButton("Fire2"))
        {
            fovTarget = zoomInValue;
            zoomTargetSpeed = zoomInSpeed;
            gunTarget = zoomInPosition.localPosition;
        }
        // 그렇지 않고 마우스 왼쪽 버튼은 떼면 ZoomOut
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
        // 0. 만약 G버튼을 누르면
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 1. 폭탄공장에서 폭탄을 만들고
            GameObject grenade = Instantiate(grenadeFactory);
            // 2. 폭탄을 총구위치에 가져다놓고
            grenade.transform.position = transform.position;
            // 3. 폭탄에게서 Rigidbody컴포넌트를 가져와서
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            // 4. Rigidbody에 내 앞쪽 45도 방향으로 힘을 가하고 싶다.
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

        // 1. 사용자가 마우스 왼쪽버튼을 누르면
        if (Input.GetButtonDown("Fire1") && rifle.CanShoot())
        {
            rifle.Shoot();
            // 2. 카메라위치에서 카메라앞방향으로 Ray를 만들고
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 3. 만약 바라본 후 부딪혔다면
            RaycastHit hitInfo;
            int layerMask = ~(1 << LayerMask.NameToLayer("EnemyDeath"));
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
            {
                // 4. 부딪힌 곳에 총알자국공장에서 총알자국을만들어서
                GameObject bi = Instantiate(bulletImpactFactory);
                // 5. 그 위치에 배치하고싶다.
                bi.transform.position = hitInfo.point;
                bi.transform.forward = hitInfo.normal;

                // Player가 Enemy에게 Damage를 입히면 
                // 만약 부딪힌것이 Enemy라면 EnemyHP컴포넌트를 가져와서
                if (hitInfo.transform.name.Contains("Enemy"))
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    enemy.TakeDamage(1);
                }
            }
        }
    }
}
