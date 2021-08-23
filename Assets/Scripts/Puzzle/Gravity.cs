using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    none,
    forward,
    back,
    left,
    right
}

public class Gravity : MonoBehaviour
{
    [HideInInspector]
    public bool startRay;
    [Header("=====灯条对象=====")]
    public GameObject Light;
    [Header("=====射线方向=====")]
    public Direction direction;

    Ray ray;
    private Electrodes electrodes;

    // Start is called before the first frame update
    void Start()
    {
        if (electrodes == null) { electrodes = this.transform.GetComponent<Electrodes>(); }//获取自己的充能状态
    }

    // Update is called once per frame
    void Update()
    {
        if (startRay)
        {
            Light.GetComponent<Renderer>().material.color = new Color(0.4f, 0.75f, 0.4f);
        }
        else
        {
            Light.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }

        if (electrodes.EnergyCount < electrodes.EnergyMax && electrodes.EnergyCount > (-1 * electrodes.EnergyMax))
        {
            startRay = false;
        }

        if (startRay)
        {
            RayHitBox();
            //startRay = false;
        }
    }

    private void FixedUpdate()
    {

        
    }

    public void RayHitBox()
    {
        GravityRay();
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 11))
        {
            GameObject puzzlebox = hit.collider.gameObject;
            puzzlebox.GetComponent<PuzzleBox>().MagnetTarget = this.gameObject;

            if (electrodes.state != hit.collider.GetComponent<Electrodes>().state && hit.collider.GetComponent<Electrodes>().state != ElectrodesState.None && electrodes.state != ElectrodesState.None)
            {
                Debug.Log("Pull!");
                puzzlebox.GetComponent<PuzzleBox>().pull = true;
                puzzlebox.GetComponent<PuzzleBox>().boxDirection = this.direction;
                return;

            }
            else if(electrodes.state == hit.collider.GetComponent<Electrodes>().state && hit.collider.GetComponent<Electrodes>().state != ElectrodesState.None && electrodes.state != ElectrodesState.None)
            {
                Debug.Log("Push!");
                puzzlebox.GetComponent<PuzzleBox>().push = true;
                puzzlebox.GetComponent<PuzzleBox>().boxDirection = this.direction;
                return;
            }
        }

    }

    public void GravityRay()
    {
        if (direction == Direction.forward) 
        {
            ray = new Ray(transform.position, Vector3.forward);
            Debug.DrawRay(transform.position, Vector3.forward, Color.red);
        }
        else if (direction == Direction.back)
        {
            ray = new Ray(transform.position, Vector3.back);
            Debug.DrawRay(transform.position, Vector3.back, Color.red);
        }
        else if (direction == Direction.left)
        {
            ray = new Ray(transform.position, Vector3.left);
            Debug.DrawRay(transform.position, Vector3.left, Color.red);
        }
        else if (direction == Direction.right)
        {
            ray = new Ray(transform.position, Vector3.right);
            Debug.DrawRay(transform.position, Vector3.right, Color.red);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "puzzleBox")
        {
            electrodes.EnergyCount = 0;
        }
    }
}
