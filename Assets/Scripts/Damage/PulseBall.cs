using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBall : MonoBehaviour
{
    private float time = 0;
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > speed)
        {
            this.transform.localScale += new Vector3(1, 1, 1);
            time = 0;
        }
        if (this.transform.localScale == new Vector3(40, 40, 40))
        {
            Destroy(this.gameObject);
        }
    }
}
