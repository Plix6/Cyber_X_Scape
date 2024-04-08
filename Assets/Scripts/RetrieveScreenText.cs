using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RetrieveScreenText : MonoBehaviour
{
    [SerializeField] private TMP_Text linkedText;

    public string getText()
    {
        return linkedText.text;
    }
}
