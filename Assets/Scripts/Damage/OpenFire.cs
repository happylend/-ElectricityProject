using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFire : MonoBehaviour
{
    public GameObject GunObj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GunObj.transform.GetComponent<Gun>().OpenFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GunObj.transform.GetComponent<Gun>().OpenFire = false;
        }
    }
}
