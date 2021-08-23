using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public GameObject BossHP;
    public BossManager bossManager;

    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        BossHP.SetActive(false);
        bossManager.enabled = false;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            BossHP.SetActive(true);
            bossManager.enabled = true;
        }
        else
        {
            BossHP.SetActive(false);
            bossManager.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            if (!start)
                start = true;
            else
                start = false;
        }
    }


}
