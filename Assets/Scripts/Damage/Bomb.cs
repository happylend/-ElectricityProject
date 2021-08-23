using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Player;
    public float speed = 10;
    public int damg;
    // Start is called before the first frame update
    void Start()
    {
        if (Player == null) { Player = GameObject.FindGameObjectWithTag("Player"); }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Electrodes>().EnergyCount == 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (this.GetComponent<Electrodes>().EnergyCount > 0)
            {
                this.GetComponent<Renderer>().material.color = new Color(0.7f, 0.2f, 0.5f, 0.7f);
            }
            else
            {
                this.GetComponent<Renderer>().material.color = new Color(0.2f, 0.5f, 0.7f, 0.7f);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + 1, Player.transform.position.z), speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Gun" && other.gameObject.tag != "ElectromagneticGun" && other.gameObject.tag != "PlayerBullet" && other.gameObject.tag != "BOSS" && other.gameObject.tag != "Bomb" && other.gameObject.tag != "PulseBall")
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<HP>().hp -= damg;
            }
            if (other.gameObject.tag == "Piles")
            {
                if (this.GetComponent<Electrodes>().EnergyCount > 0)
                {
                    other.transform.GetComponent<Electrodes>().EnergyCount += damg;
                }
                else
                {
                    other.transform.GetComponent<Electrodes>().EnergyCount -= damg;
                }

            }
            Destroy(this.gameObject);
        }
    }
}
