using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ī�޶��� ȸ������� ��ġ��Ű��ʹ�.
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
