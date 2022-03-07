using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스를 움직여서 카메라를 회전하고 싶다.
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
        // 마우스를 움직여서 (변화량)
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;

        // rx의 각도를 제한하고싶다.
        rx = Mathf.Clamp(rx, -75, 75);

        // 카메라를 회전하고 싶다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }

    float Clamp(float value, float min, float max)
    {
        // value가 min보다 작다면 min을 반환하고싶다.
        if (value < min)
            return min;
        // value가 max보다 크다면 max을 반환하고싶다.
        if (value > max)
            return max;
        // 이도저도 아니라면 value를 반환하고싶다.
        return value;
    }
}
