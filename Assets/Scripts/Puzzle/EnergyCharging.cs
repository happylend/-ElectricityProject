using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharging : MonoBehaviour
{
    //获取充能状态
    private Electrodes electrodes;

    [Header("=====是否处于充能状态=====")]
    public bool InEnergy;
    [Header("=====是否能过载爆炸=====")]
    public bool CanLostEnergy;
    [Header("=====充能持续时间=====")]
    public float EnergyTime = 0;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (electrodes == null) { electrodes = this.transform.GetComponent<Electrodes>(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (!electrodes.InEnergy) 
        {
            this.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
        Charging();
        if (CanLostEnergy)
        {
            if(InEnergy)
            {
                Destroy(this.transform.parent.gameObject);
            }

        }
    }

    public void Charging()
    {
        if (!electrodes.InEnergy)
        {
            this.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }
        else
        {
            if (electrodes.state == ElectrodesState.In)
            {
                this.GetComponent<Renderer>().material.color = new Color(0.7f, 0.2f, 0.5f);
            }
            if (electrodes.state == ElectrodesState.Out)
            {
                this.GetComponent<Renderer>().material.color = new Color(0.2f, 0.5f, 0.7f);
            }
        }
    }


}
