using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPos : MonoBehaviour
{
    private void Update()
    {
        this.transform.rotation = this.transform.parent.rotation;
    }
}
