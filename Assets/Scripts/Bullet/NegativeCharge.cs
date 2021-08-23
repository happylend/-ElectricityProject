using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeCharge : MonoBehaviour
{
    private float time = 0;
    public int damg = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.8f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Launch" && other.tag != "ElectromagneticGun" && other.tag != "MagnetCheck")
        {
            if (other.GetComponent<Electrodes>() != null)
            {
                //other.GetComponent<Electrodes>().InEnergy = true;
                other.GetComponent<Electrodes>().EnergyCount -= damg;
            }
            Destroy(this.gameObject);
        }

    }

}
