using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public CameraController camcon;
    public IUserInput pi;
    public float walkSpeed = 2.0f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 1.0f;

    [Header("===== Friction Settings =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool canAttack;

    public bool canLaunch;
    public GameObject TriggerObject = null;

    private bool trackDirection = false;

    private bool lockPlanar = false;

    private CapsuleCollider col;
    private float lerpTarget;
    private Vector3 deltaPos;

    [Header("===== bullet Settings =====")]
    public GameObject Positivebullet;
    public GameObject Negativebullet;
    public GameObject bulletPos;

    // Start is called before the first frame update
    void Awake()
    {
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach(var input in inputs)
        {
            if (input.enabled == true)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (camcon.lockState == false)
        {
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.run) ? 2.0f : 1.0f), 0.5f));
        }

        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }
        if (pi.leftAttack && CheckState("ground") && canAttack)
        {
            LeftShoot();
            anim.SetTrigger("attack");
        }

        if (pi.rightAttack && CheckState("ground") && canAttack)
        {
            RightShoot();
            anim.SetTrigger("attack");
        }


        if (camcon.lockState == false)
        {
            if (pi.Dmag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            }
            if (lockPlanar == false)
            {
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
        else
        {
            if (trackDirection == false)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }

            if (lockPlanar == false)
            {
                planarVec = pi.Dvec * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }

        }

        if(pi.quitGame)
        {
            Application.Quit();
        }

        if (pi.interact && canLaunch)
        {
            if (TriggerObject != null)
            {
                TriggerObject.GetComponent<LaunchMachine>().launch();
            }
        }
    }

    void FixedUpdate()
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    public void LeftShoot()
    {
        Instantiate(Positivebullet, bulletPos.transform.position, bulletPos.transform.rotation);
    }
    public void RightShoot()
    {
        Instantiate(Negativebullet, bulletPos.transform.position, bulletPos.transform.rotation);
    }


    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    public void IsGround()
    {
        anim.SetBool("IsGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("IsGround", false);
    }

    public void OnJumpEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
        trackDirection = true;
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }

    public void OnGroundExit()
    {
        pi.inputEnable = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionZero;
    }

    /*
    public void OnFallEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, rollVelocity, 0);
        trackDirection = true;
    }
    */
    public void OnJabEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }



    public void OnAttack1hAEnter()
    {
        pi.inputEnable = false;
        lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");

        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.4f));
    }

    public void OnAttackIdleEnter()
    {
        pi.inputEnable = true;
        lerpTarget = 0f;
    }

    public void OnAttackIdleUpdate()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.4f));
    }

    public void OnUpdateRM(object _deltaPos)
    {
        if(CheckState("attack1hC","attack"))
        {
            deltaPos += (0.8f * deltaPos + 0.2f * (Vector3)_deltaPos) / 1.0f;
        }
    }
}
