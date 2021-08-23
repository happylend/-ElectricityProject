using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    //BOSS电荷
    private bool PorN = true;
    //电荷槽被摧毁
    private bool StartBomb = false;

    public int Maxhp = 3;
    public int hp;
    public GameObject[] Gun;
    public GameObject[] Bomb;

    public GameObject PulseBall;

    public GameObject HPBox;
    public GameObject HPText;
    public GameObject EnergyBox;

    public GameObject hpBoxPhanel;
    public GameObject win;


    private bool cutOneLife = false;
    private bool OneBomb;

    public float maxTime;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        hp = Maxhp;
        if (this.transform.GetComponent<Electrodes>().EnergyCount > 0)
        {
            PorN = true;
        }
        else if(this.transform.GetComponent<Electrodes>().EnergyCount < 0)
        {
            PorN = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //生命显示
        HPBox.transform.GetComponent<Image>().fillAmount = (float)hp / (float)Maxhp;
        HPText.transform.GetComponent<Text>().text = hp.ToString();
        EnergyBox.transform.GetComponent<Image>().fillAmount = (float)Mathf.Abs(this.transform.GetComponent<Electrodes>().EnergyCount) / (float)this.transform.GetComponent<Electrodes>().EnergyMax;

        //攻击
        GunBomb();


        //能量槽清空
        DeadOnce();
    }

    void GunBomb()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            if (this.transform.GetComponent<Electrodes>().EnergyCount > 0)
            {
                for (int i = 0; i < Gun.Length; i++)
                {
                    Instantiate(Bomb[0], Gun[i].transform.position, Gun[i].transform.rotation);
                }
            }
            else if (this.transform.GetComponent<Electrodes>().EnergyCount < 0)
            {
                for (int i = 0; i < Gun.Length; i++)
                {
                    Instantiate(Bomb[1], Gun[i].transform.position, Gun[i].transform.rotation);
                }
            }
            else
                return;
            time = 0;
        }
    }

    void DeadOnce()
    {
        if (StartBomb == false && this.transform.GetComponent<Electrodes>().EnergyCount == 0)
        {
            cutOneLife = true;
            OneBomb = true;
        }

        if (cutOneLife)
        {
            hp--;
            CreatBall();
            NextLife();
            cutOneLife = false;
        }
        if (hp <= 0)
        {
            hpBoxPhanel.SetActive(false);
            win.SetActive(true);
            this.gameObject.SetActive(false);
            
        }
    }

    void CreatBall()
    {
        if (OneBomb)
        {
            Instantiate(PulseBall, this.transform.position, this.transform.rotation);
            StartBomb = true;
            OneBomb = false;
        }

    }

    void NextLife()
    {
        if (StartBomb)
        {
            if (PorN)
            {
                this.transform.GetComponent<Electrodes>().EnergyCount = (this.transform.GetComponent<Electrodes>().EnergyMax * -1);
                PorN = false;
                StartBomb = false;
            }
            else
            {
                this.transform.GetComponent<Electrodes>().EnergyCount = this.transform.GetComponent<Electrodes>().EnergyMax;
                PorN = true;
                StartBomb = false;
            }
        }
    }

}
