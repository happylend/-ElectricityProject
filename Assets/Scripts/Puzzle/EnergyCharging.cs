using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharging : MonoBehaviour
{
    //��ȡ����״̬
    private Electrodes electrodes;

    [Header("=====�Ƿ��ڳ���״̬=====")]
    public bool InEnergy;
    [Header("=====�Ƿ��ܹ��ر�ը=====")]
    public bool CanLostEnergy;
    [Header("=====���ܳ���ʱ��=====")]
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
