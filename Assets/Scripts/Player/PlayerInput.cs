using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IUserInput
{
    [Header("===== Key settings =====")]
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;
    public string keyE;
    public string keyF;

    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;

    [Header("===== Mouse settings =====")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseEnable == true)
        {
            Jup = Input.GetAxis("Mouse Y") * 2.7f * mouseSensitivityY;
            Jright = Input.GetAxis("Mouse X") * 2.5f * mouseSensitivityX;
        }
        else
        {
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        }


        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (inputEnable == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(keyA);
        defense = Input.GetKey(keyD);

        bool newJump = Input.GetKey(keyB);
        if (newJump != lastJump && newJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

        //×ó¼ü¹¥»÷
        bool leftattack = Input.GetKey(keyC);
        if (leftattack != lastLAttack && leftattack == true)
        {
            leftAttack = true;
        }
        else
        {
            leftAttack = false;
        }
        lastLAttack = leftattack;

        //ÓÒ¼ü¹¥»÷
        bool rightattack = Input.GetKey(keyD);
        if (rightattack != lastRAttack && rightattack == true)
        {
            rightAttack = true;
        }
        else
        {
            rightAttack = false;
        }
        lastRAttack = rightattack;

        //Éè±¸½»»¥
        interact = Input.GetKeyDown(keyE);

        bool exitgame = Input.GetKeyDown(keyF);
        if(exitgame)
        {
            quitGame = true;
        }
    }

}
