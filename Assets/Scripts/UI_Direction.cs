using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Direction : MonoBehaviour
{
    private Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.GetChild(0).LookAt(transform.position + cam.forward);
    }
}
