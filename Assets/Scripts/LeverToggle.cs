using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverToggle : MonoBehaviour
{
    [SerializeField] private GameObject Knob;

    private void Start()
    {
        Knob.transform.eulerAngles = new Vector3(110, Knob.transform.eulerAngles.y, Knob.transform.eulerAngles.z);
        if (Knob.transform.eulerAngles.x == 70)
        {
            Knob.transform.eulerAngles += new Vector3(40, 0, 0);
        }

        //TODO : rotation of object in scene # data
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("Lever clicked, know has rotation : " + Knob.transform.eulerAngles);
        if (Knob.transform.eulerAngles.x == 10)
        {
            Knob.transform.eulerAngles += new Vector3(100, 0, 0);
        } 
        else if (Knob.transform.eulerAngles.x == 110) 
        {
            Knob.transform.eulerAngles -= new Vector3(100, 0, 0);
        }
    }
}
