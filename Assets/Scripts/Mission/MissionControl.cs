using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionControl : MonoBehaviour
{
    public Text contentText;
    public string[] missionContent;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contentText.text = missionContent[index];
    }
}
