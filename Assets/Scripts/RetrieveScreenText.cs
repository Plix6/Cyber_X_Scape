using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RetrieveScreenText : MonoBehaviour
{
    [SerializeField] private TMP_Text linkedText;
    [SerializeField] private bool number;
    [SerializeField] private bool shift;

    public string getText()
    {
        return linkedText.text;
    }

    public bool isNumber()
    {
        return number;
    }

    public bool isShift()
    {
        return shift;
    }
}
