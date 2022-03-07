using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy가 Player를 공격할 때 ImageHit를 번쩍 거리게 하고싶다.
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
        // 태어날 때 imageHit를 보이지 않게 하고싶다.
        imageHit.SetActive(false);
    }


    
    // 번쩍이고싶다.
    public void DoHit(int damageAmount, System.Action callback)
    {
        // 처음부터해
        StopCoroutine("IEHit"); 
        StartCoroutine("IEHit", callback);

        // Enemy가 Player를 Hit하면 체력을 1 감소시키고싶다.
        playerHP.HP -= damageAmount;
        // 플레이어가 죽었을때 GameOverUI를 보이게하고싶다.
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
