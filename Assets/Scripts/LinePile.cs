using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePile : MonoBehaviour
{
    public GameObject Pile1;
    public GameObject Pile2;
    public GameObject Line;
    public GameObject Player;
    public GameObject PulsesBall;
    public bool create = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pile1.transform.GetChild(0).GetComponent<Electrodes>().state != ElectrodesState.None && Pile2.transform.GetChild(0).GetComponent<Electrodes>().state != ElectrodesState.None)
        {
            if (Pile1.transform.GetChild(0).GetComponent<Electrodes>().state == Pile2.transform.GetChild(0).GetComponent<Electrodes>().state)
            {
                Pile1.transform.GetChild(0).GetComponent<Electrodes>().EnergyCount = 0;
                Pile2.transform.GetChild(0).GetComponent<Electrodes>().EnergyCount = 0;
                create = true;

            }
            else
            {
                Line.transform.position = new Vector3(Line.transform.position.x, 1, Line.transform.position.z);
            }
        }

        if(create)
        {
            Instantiate(PulsesBall, Pile1.transform.position, Pile1.transform.rotation);
            Instantiate(PulsesBall, Pile2.transform.position, Pile2.transform.rotation);
            create = false;
        }
    }
}
