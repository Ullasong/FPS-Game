using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 메인카메라의 회전방향과 일치시키고싶다.
public class Billboard : MonoBehaviour
{
    Transform mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mainCamera.rotation;
    }
}
