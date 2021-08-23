using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    [Header("===== Output signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;

    public bool run;
    public bool defense;
    public bool jump;
    protected private bool lastJump;
    public bool leftAttack;
    public bool rightAttack;
    protected private bool lastLAttack;
    protected private bool lastRAttack;
    public bool roll;
    public bool lockon;
    public bool quitGame;
    public bool interact;//»úÆ÷½»»¥

    [Header("===== Others =====")]
    public bool inputEnable = true;

    protected private float targetDup;
    protected private float targetDright;
    protected private float velocityDup;
    protected private float velocityDright;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected private Vector2 SquareToCircle(Vector2 input)
    {

        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}
