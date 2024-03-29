using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CipherWheelAnimation : MonoBehaviour
{
    [SerializeField] private GameObject topWheel;
    [SerializeField] private float duration;
    private int curShift;
    private float curRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        NewRandomShift(); // Initial rotation
    }

    public void NewRandomShift()
    {
        int newShift;
        do
        {
            newShift = Random.Range(1, 26);
        } while (newShift == curShift);

        curShift = newShift;
        ChangeRotation();
    }

    private void ChangeRotation()
    {
        float rotationChange = (360f / 26) * curShift - curRotation;
        curRotation = rotationChange + curRotation;

        LeanTween.cancel(topWheel);
        Debug.Log("Shift : " + curShift + "; Rotation : " + curRotation);
        LeanTween.rotateAroundLocal(topWheel,
            Vector3.back,
            rotationChange, 
            duration);
    }

    public int GetShift()
    {
        return curShift;
    }
}
