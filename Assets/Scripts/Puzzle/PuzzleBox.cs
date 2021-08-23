using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleBox : MonoBehaviour
{
    public GameControl gc;

    public Direction boxDirection;
    public float speed = 3;
    Ray ray;
    RaycastHit hit;

    public GameObject MagnetTarget;

    public bool pull;//À­
    public bool push;//ÍÆ


    public bool getEnd;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(pull)
        {
            MoveDirectionPull();
            if (pull)
            {
                switch (boxDirection)
                {
                    case Direction.forward:
                        transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
                        break;
                    case Direction.back:
                        transform.position += Vector3.back * speed * Time.fixedDeltaTime;
                        break;
                    case Direction.left:
                        transform.position += Vector3.left * speed * Time.fixedDeltaTime;
                        break;
                    case Direction.right:
                        transform.position += Vector3.right * speed * Time.fixedDeltaTime;
                        break;
                }
            }
        }

        if(push)
        {
            MoveDirectionPush();
            if (push)
            {
                switch (boxDirection)
                {
                    case Direction.forward:
                        transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
                        break;
                    case Direction.back:
                        transform.position += Vector3.back * speed * Time.fixedDeltaTime;
                        break;
                    case Direction.left:
                        transform.position += Vector3.left * speed * Time.fixedDeltaTime;
                        break;
                    case Direction.right:
                        transform.position += Vector3.right * speed * Time.fixedDeltaTime;
                        break;
                }
            }
        }
        if (!push && !pull)
            this.transform.position = new Vector3(Mathf.Round(this.transform.position.x), this.transform.position.y, Mathf.Round(this.transform.position.z));
    }


    public void MoveDirectionPush()
    {
        if (boxDirection == Direction.forward)
        {
            ray = new Ray(transform.position, Vector3.forward);
            Debug.DrawRay(transform.position, Vector3.forward * Mathf.Infinity, Color.blue);
        }
        else if (boxDirection == Direction.back)
        {
            ray = new Ray(transform.position, Vector3.back);
            Debug.DrawRay(transform.position, Vector3.back * Mathf.Infinity, Color.blue);
        }
        else if (boxDirection == Direction.left)
        {
            ray = new Ray(transform.position, Vector3.left);
            Debug.DrawRay(transform.position, Vector3.left * Mathf.Infinity, Color.blue);
        }
        else if (boxDirection == Direction.right)
        {
            ray = new Ray(transform.position, Vector3.right);
            Debug.DrawRay(transform.position, Vector3.right * Mathf.Infinity, Color.blue);
        }

        if (Physics.Raycast(ray, out hit, 0.56f))
        {
            
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Floor" && hit.collider.gameObject.tag != "Launch")
            {
                Debug.Log(hit.collider.transform.name + ": " + hit.distance);
                if (hit.distance <= 0.5f)
                    push = false;
            }

        }
    }
    public void MoveDirectionPull()
    {
        if (boxDirection == Direction.forward)
        {
            boxDirection = Direction.back;
            ray = new Ray(transform.position, Vector3.back);
            Debug.DrawRay(transform.position, Vector3.back * Mathf.Infinity, Color.blue);
        }
        else if (boxDirection == Direction.back)
        {
            boxDirection = Direction.forward;
            ray = new Ray(transform.position, Vector3.forward);
            Debug.DrawRay(transform.position, Vector3.forward * Mathf.Infinity, Color.blue);
        }
        else if (boxDirection == Direction.left)
        {
            boxDirection = Direction.right;
            ray = new Ray(transform.position, Vector3.right);
            Debug.DrawRay(transform.position, Vector3.right * Mathf.Infinity, Color.blue);
        }
        else if (boxDirection == Direction.right)
        {
            boxDirection = Direction.left;
            ray = new Ray(transform.position, Vector3.left);
            Debug.DrawRay(transform.position, Vector3.left * Mathf.Infinity, Color.blue);
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Floor" && hit.collider.gameObject.tag != "Launch")
            {
                Debug.Log(hit.collider.transform.name + ": " + hit.distance);
                if (hit.distance <= 0.5f)
                    pull = false;
            }

        }
    }

    void MovementCompensation()
    {
        if (boxDirection == Direction.forward)
        {
            this.transform.position += new Vector3(0, 0, 0.12f);
        }
        else if (boxDirection == Direction.back)
        {
            this.transform.position -= new Vector3(0, 0, 0.12f);
        }
        else if (boxDirection == Direction.left)
        {
            this.transform.position -= new Vector3(0.12f, 0, 0);
        }
        else if (boxDirection == Direction.right)
        {
            this.transform.position += new Vector3(0.12f, 0, 0);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PuzzleEnd")
        {
            this.GetComponent<Renderer>().material.color = Color.green;
            gc.BoxCount++;
            getEnd = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "PuzzleEnd")
        {
            this.GetComponent<Renderer>().material.color = Color.white;
            gc.BoxCount--;
            getEnd = false;
        }
    }
}
