using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public IUserInput pi;
    public LaunchMachine lm;
    public MissionControl mc;
    public Camera[] cam;
    public GameObject missionPanel;

    [Header("=====UI组件=====")]
    public GameObject TextPanel;
    public Text textLabel;

    [Header("=====文本文件=====")]
    public TextAsset[] textFile;
    public int index;
    public int missionNum = 0;

    public bool[] FinishMission;

    List<string> textList = new List<string>();
    // Start is called before the first frame update

    private void Start()
    {
        GetText();

    }
    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (missionNum == 1 && missionPanel.activeSelf == true && index <= 3)
        {
            cam[0].gameObject.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.F) && index == textList.Count)
        {
            cam[1].gameObject.SetActive(false);
            TextPanel.SetActive(false);
            pi.inputEnable = true;
            lm.InTalk = false;
            if (FinishMission[missionNum])
            {
                switch(missionNum)
                {
                    case 0: mc.index = 1;break;
                    case 1: mc.index = 4;break;
                    case 2:break;
                    case 3:break;
                }
                FinishMission[missionNum] = false;
            }
            index = 0;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            textLabel.text = textList[index];
            index++;
            if (missionNum == 1 && index > 3 && index < 6)
            {
                cam[0].gameObject.SetActive(false);
                cam[1].gameObject.SetActive(false);
            }
            else if (missionNum == 1 && index > 6)
            {
                cam[0].gameObject.SetActive(false);
                cam[1].gameObject.SetActive(true);
            }

        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        string[] lineData = file.text.Split('\n');

        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    public void GetText()
    {
        GetTextFromFile(textFile[missionNum]);
        index = 0;
        textLabel.text = textList[index];
        index++;
    }
}
