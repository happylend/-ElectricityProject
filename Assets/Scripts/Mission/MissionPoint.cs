using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPoint : MonoBehaviour
{
    public DialogSystem ds;
    public MissionControl mc;
    public GameObject NPC;
    public Transform nextNPCPos;

    [Header("=====´Ó3¿ªÊ¼=====")]
    public int missionPoint = 3;

    public bool PlayerTrigger = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(PlayerTrigger)
            {
                mc.index = missionPoint;
                ds.missionNum++;
                ds.GetText();
                NPC.transform.position = nextNPCPos.position;
                PlayerTrigger = false;
            }
        }
    }
}
