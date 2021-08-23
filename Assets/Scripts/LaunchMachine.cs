using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MachineType
{
    None,
    Magnets,//吸铁石
    Circuits,//电路
    NPC,//NPC
}

public class LaunchMachine : MonoBehaviour
{
    public IUserInput pi;

    public MachineType machine;

    private ActorController ac;
    private GameObject MessageText;

    private GameObject MissionContent;

    public bool InTalk;

    private void Awake()
    {
        if (ac == null) { ac = Object.FindObjectOfType<ActorController>(); }
        if (MessageText == null) { MessageText = GameObject.Find("LaunchImage"); }
        if (MissionContent == null) { MissionContent = GameObject.FindGameObjectWithTag("MissionContent"); }

    }
    // Start is called before the first frame update
    void Start()
    {
        MessageText.SetActive(false);
        MissionContent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ac.TriggerObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ac.canLaunch = true;
            ac.TriggerObject = this.gameObject;
            if(!InTalk)
            {
                MessageText.SetActive(true);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ac.canLaunch = false;
            ac.TriggerObject = null;
            MessageText.SetActive(false);
        }
    }

    public void launch()
    {
        //启动吸铁石
        if (machine == MachineType.Magnets)
        {
            this.transform.parent.GetComponent<Gravity>().startRay = true;
            //this.transform.parent.GetChild(0).GetComponent<Electrodes>().EnergyCount = 0;
        }
        if (machine == MachineType.NPC)
        {
            if (MissionContent.activeSelf == false)
            {
                MissionContent.SetActive(true);
                MessageText.SetActive(false);
                pi.inputEnable = false;
                InTalk = true;
            }
            else
            {
                MissionContent.SetActive(false);
            }

        }
    }
}
