using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CipherWheelAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] centerWheels;
    [SerializeField] private GameObject wheelSound;
    [SerializeField] private float duration;
    private Color[] wheelColors = { 
        new (205 / 255f, 28 / 255f, 19/255f, 0.8f), //red
        new (66 / 255f, 91 / 255f, 207 / 255f, 0.8f), //blue
        new (63 / 255f, 193 / 255f, 76 / 255f, 0.8f), //green
    };

    private int[] curShifts = { 0, 0, 0 };
    private float[] curRotations = { 0f, 0f, 0f };
    // gameObj.GetComponent<Renderer>().material.color = new Color(0, 204, 102);

    private void Awake()
    {
        // Random.InitState(45);

        for (int i = 0; i < centerWheels.Length; i++)
        {
            centerWheels[i].GetComponent<Renderer>().material.color = wheelColors[i];
        }

        NewRandomShifts(); // Initial rotation
    }

    public void NewRandomShifts()
    {
        for (int i = 0; i < centerWheels.Length; i++)
        {
            int newShift;
            do {
                newShift = Random.Range(1, 26);
            } while (newShift == curShifts[i]);
            curShifts[i] = newShift;
        }

        ChangeRotations();
    }

    private void ChangeRotations()
    {
        for (int i = 0; i < centerWheels.Length; i++)
        {
            float rotationChange = -1 * (360f / 26) * curShifts[i] - curRotations[i];
            curRotations[i] = rotationChange + curRotations[i];

            LeanTween.cancel(centerWheels[i]);
            wheelSound.GetComponent<AudioSource>().enabled = true;
            LeanTween.rotateAroundLocal(centerWheels[i],
                Vector3.back,
                rotationChange,
                duration).setEaseInOutCubic();
        }
    }

    public int[] GetShifts()
    {
        return curShifts;
    }

    private IEnumerator stopSound()
    {
        yield return new WaitForSeconds(duration);

        wheelSound.GetComponent<AudioSource>().enabled = false;

        yield return null;
    }
}
