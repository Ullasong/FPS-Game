using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rifle : MonoBehaviour
{
    // ÅºÃ¢¼Ó¿¡ ÃÑ¾Ë °¹¼ö
    public int count;
    // ÃÖ´ë ÃÑ¾Ë °¹¼ö
    public int maxCount = 10;

    public Text textCount;

    // ÃÑÀ» ½ò ¼ö ÀÖ´Â°¡?
    public bool CanShoot()
    {
        return count > 0;
    }
    // ÃÑ¾ËÀ» ÇÏ³ª °¨¼ÒÇÏ°í½Í´Ù.
    public void Shoot()
    {
        if (count > 0)
        {
            count--;
            textCount.text = count + "/" + maxCount;
        }
    }
    // ÅºÃ¢À» Ã¤¿ì°í½Í´Ù.
    public void Reload()
    {
        count = maxCount;
        textCount.text = count + "/" + maxCount;
    }
    void Start()
    {
        Reload();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
