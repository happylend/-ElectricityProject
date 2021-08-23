using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Pile;
    public MissionControl mc;
    public ElectrodesState electrodesState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pile.transform.GetChild(0).GetComponent<Electrodes>().state == electrodesState)
        {
            Destroy(Pile);
            mc.index = 2;
            Destroy(this.gameObject);
        }
    }
}
