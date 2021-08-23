using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public IUserInput pi;
    public MissionControl mc;
    public GameObject Player;
    public GameObject BossDoor;
    public GameObject[] PuzzleBox;
    public int BoxCount;

    public GameObject EndScreen;
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1;
        EndScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BoxCount == PuzzleBox.Length)
        {
            mc.index++;
            Destroy(BossDoor);
        }

        if (Player.GetComponent<HP>().hp <= 0)
        {
            pi.inputEnable = false;
            EndScreen.SetActive(true);
        }
    }
}
