using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy�� Player�� ������ �� ImageHit�� ��½ �Ÿ��� �ϰ�ʹ�.
public class HitManager : MonoBehaviour
{
    public static HitManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject imageHit;
    public PlayerHP playerHP;

    void Start()
    {
        // �¾ �� imageHit�� ������ �ʰ� �ϰ�ʹ�.
        imageHit.SetActive(false);
    }


    
    // ��½�̰�ʹ�.
    public void DoHit(int damageAmount, System.Action callback)
    {
        // ó��������
        StopCoroutine("IEHit"); 
        StartCoroutine("IEHit", callback);

        // Enemy�� Player�� Hit�ϸ� ü���� 1 ���ҽ�Ű��ʹ�.
        playerHP.HP -= damageAmount;
        // �÷��̾ �׾����� GameOverUI�� ���̰��ϰ�ʹ�.
        if (playerHP.HP <= 0)
        {
            GameManager.instance.gameOverUI.SetActive(true);
        }
    }

    IEnumerator IEHit(System.Action callback)
    {
        imageHit.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        imageHit.SetActive(false);
        callback();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
