using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ElectrodesState
{
    None,
    In,
    Out,
}

public class Electrodes : MonoBehaviour
{
    [Header("=====是否处于充能状态=====")]
    public bool InEnergy;

    public ElectrodesState state;
    public float EnergyCount = 0;
    public float EnergyMax = 5;
    public Transform CanvasBox;

    Transform[] Img;//2:Positive;3::Negative
    // Start is called before the first frame update
    void Start()
    {
        Img = CanvasBox.GetChild(0).GetComponentsInChildren<Transform>();
        Img[3].GetComponent<Image>().fillAmount = 0;
        Img[2].GetComponent<Image>().fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EnergyCounter();
        EnergyUpdate();

    }

    public void EnergyUpdate()
    {
        if (EnergyCount > 0)
        {
            Img[2].gameObject.SetActive(true);
            Img[3].gameObject.SetActive(false);

            Img[2].GetComponent<Image>().fillAmount = (EnergyCount / EnergyMax);
        }
        if (EnergyCount < 0)
        {
            Img[2].gameObject.SetActive(false);
            Img[3].gameObject.SetActive(true);

            Img[3].GetComponent<Image>().fillAmount = (Mathf.Abs(EnergyCount) / EnergyMax);
        }
        if (EnergyCount == 0)
        {
            Img[2].GetComponent<Image>().fillAmount = 0;
            Img[3].GetComponent<Image>().fillAmount = 0;
        }
    }

    public void EnergyCounter()
    {
        if (EnergyCount < EnergyMax || EnergyCount > (-1 * EnergyMax))
        {
            InEnergy = false;
            state = ElectrodesState.None;
        }
        if (EnergyCount == EnergyMax)
        {
            InEnergy = true;
            state = ElectrodesState.In;
        }
        if (EnergyCount == (EnergyMax * -1))
        {
            InEnergy = true;
            state = ElectrodesState.Out;
        }

        if (EnergyCount > EnergyMax)
        {
            EnergyCount = EnergyMax;
        }
        if (EnergyCount < (EnergyMax * -1))
        {
            EnergyCount = (EnergyMax * -1);
        }
    }
}
