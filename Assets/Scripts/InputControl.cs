using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    private bool number = false;
    private bool shift = false;

    public void SetOptions(bool number,  bool shift)
    {
        this.number = number;
        this.shift = shift;
    }

    public char CustomValidator(string text, int charIndex, char addedChar)
    {
        if (!number && char.IsLetter(addedChar) && text.Length + 1 <= 10)
        {
            return char.ToUpper(addedChar);
        }
        else if (number && !shift && char.IsDigit(addedChar) && text.Length + 1 <= 3)
        {
            return addedChar;
        }
        else if (char.IsDigit(addedChar) && text.Length + 1 <= 2 && int.Parse(text + addedChar) <= 25 && int.Parse(text + addedChar) >= 0)
        {
            return addedChar;
        }
        else
        {
            return '\0';
        }
    }
}
