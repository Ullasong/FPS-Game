using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���콺�� �������� ī�޶� ȸ���ϰ� �ʹ�.
public class CameraRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rx = transform.eulerAngles.x;
        ry = transform.eulerAngles.y;
    }
    float rx;
    float ry;
    public float rotSpeed = 200;
    // Update is called once per frame
    void Update()
    {
        // ���콺�� �������� (��ȭ��)
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;

        // rx�� ������ �����ϰ�ʹ�.
        rx = Mathf.Clamp(rx, -75, 75);

        // ī�޶� ȸ���ϰ� �ʹ�.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }

    float Clamp(float value, float min, float max)
    {
        // value�� min���� �۴ٸ� min�� ��ȯ�ϰ�ʹ�.
        if (value < min)
            return min;
        // value�� max���� ũ�ٸ� max�� ��ȯ�ϰ�ʹ�.
        if (value > max)
            return max;
        // �̵����� �ƴ϶�� value�� ��ȯ�ϰ�ʹ�.
        return value;
    }
}
