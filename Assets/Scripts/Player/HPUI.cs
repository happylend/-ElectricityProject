using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public GameObject Player;
    public GameObject TextHp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<HP>() != null)
        {
            this.GetComponent<Image>().fillAmount = (float)Player.GetComponent<HP>().hp / (float)Player.GetComponent<HP>().Maxhp;
            if (TextHp != null)
            {
                TextHp.GetComponent<Text>().text = Player.GetComponent<HP>().hp.ToString();
            }
        }
        else if (Player.GetComponent<BossManager>() != null)
        {
            this.GetComponent<Image>().fillAmount = (float)Player.GetComponent<BossManager>().hp / (float)Player.GetComponent<BossManager>().Maxhp;
            if (TextHp != null)
            {
                TextHp.GetComponent<Text>().text = Player.GetComponent<BossManager>().hp.ToString();
            }
        }
        else
        {
            this.GetComponent<Image>().fillAmount = 0;
            if (TextHp != null)
            {
                TextHp.GetComponent<Text>().text = "0";
            }
        }


    }
}
