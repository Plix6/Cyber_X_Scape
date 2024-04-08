using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl
{
    private bool number;
    private bool shift;

    public void SetOptions(bool number,  bool shift)
    {
        this.number = number;
        this.shift = shift;
    }

    public void Verification(string value)
    {
        if (!number)
        {

        }
    }
}
