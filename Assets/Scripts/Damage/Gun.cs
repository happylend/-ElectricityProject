using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //获取充能状态
    private Electrodes electrodes;

    [Header("=====是否处于充能状态=====")]
    public bool InEnergy;
    [Header("=====充能时间=====")]
    public float EnergyTime = 0;
    private float time = 0;
    public GameObject Bomb;
    public GameObject GunShoot;
    public bool OpenFire;
    // Start is called before the first frame update
    void Start()
    {
        if (electrodes == null) { electrodes = this.transform.GetComponent<Electrodes>(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenFire)
        {
            time += Time.deltaTime;
            if (time > EnergyTime)
            {
                Instantiate(Bomb, GunShoot.transform.position, GunShoot.transform.rotation);
                time = 0;
            }

        }
        Charging();
    }

    public void Charging()
    {
        if (electrodes.EnergyCount == 0) 
        {
            Destroy(this.transform.parent.gameObject);
        }
        else
        {
            if (electrodes.EnergyCount > 0)
            {
                this.GetComponent<Renderer>().material.color = new Color(0.7f, 0.2f, 0.5f);
            }
            else
            {
                this.GetComponent<Renderer>().material.color = new Color(0.2f, 0.5f, 0.7f);
            }
        }
    }

}
