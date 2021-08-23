using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    private void OnAnimatorMove()
    {
        GetComponentInParent<ActorController>().OnUpdateRM((object)anim.deltaPosition);
    }
}
