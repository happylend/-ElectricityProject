using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangNPC : MonoBehaviour
{
    public DialogSystem ds;
    public GameObject NPC;

    public Transform nextNPCPos;
    public Transform lastNPCPos;

    public int MissionIndex;
    private bool PlayerTrigger;
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

            if (!PlayerTrigger)
            {
                print("Change");
                ds.missionNum = MissionIndex;
                ds.GetText();
                NPC.transform.position = nextNPCPos.position;
                PlayerTrigger = true;
                return;
            }
            if (PlayerTrigger)
            {
                ds.missionNum = 1;
                ds.GetText();
                NPC.transform.position = lastNPCPos.position;
                PlayerTrigger = false;
                return;
            }
        }
    }
}
