using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [HideInInspector]
    public int hp;
    public int Maxhp;
    // Start is called before the first frame update
    void Start()
    {
        hp = Maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("ÓÎÏ·Ê§°Ü");
        }
    }
}
