using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private IUserInput pi;
    public float horizontalSpeed = 20.0f;
    public float verticalSpeed = 20.0f;
    public float cameraDampValue = 0.5f;
    public Image lockDot;
    public bool lockState;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private new GameObject camera;
    private Vector3 cameraDampVelocity;
    private LockTarget lockTarget;

    // Start is called before the first frame update
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        ActorController ac = playerHandle.GetComponent<ActorController>();
        model = ac.model;
        pi = ac.pi;
        camera = Camera.main.gameObject;
        lockDot.enabled = false;
        lockState = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockTarget == null) 
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -30, 30);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight / 2, 0));
        }


        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        camera.transform.LookAt(cameraHandle.transform);
    }
    private void Update()
    {
        if (lockTarget != null)
        {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)
            {
                lockTarget = null;
                lockDot.enabled = false;
                lockState = false;
            }
        }
    }

    public void LockUnLock()
    {
        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        if (cols.Length==0)
        {
            lockTarget = null;
            lockDot.enabled = false;
            lockState = false;
        }
        else
        {
            foreach (var col in cols)
            {
                if (lockTarget != null && lockTarget.obj == col.gameObject)
                {
                    lockTarget = null;
                    lockDot.enabled = false;
                    lockState = false;
                    break;
                }
                lockTarget = new LockTarget(col.gameObject,col.bounds.extents.y);
                lockDot.enabled = true;
                lockState = true;
                break;
            }
        }
    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;

        public LockTarget(GameObject _obj, float _halfHeight)
        {
            halfHeight = _halfHeight;
            obj = _obj;
        }
    }
}
