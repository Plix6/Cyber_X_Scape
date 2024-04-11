using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CipherManager : MonoBehaviour
{
    [SerializeField] private TMP_Text input;
    [SerializeField] private TMP_Text shift;
    [SerializeField] private TMP_Text output;
    [SerializeField] private TMP_Text code;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject[] notes;
    [SerializeField] private GameObject CipherWheelAnimatorParent;
    private TMP_Text[] noteTexts;
    private CipherWheelAnimation cipherAnimation;

    private int[] shifts;
    private string[] possibilities = { "ZERO", "DEUX", "CINQ", "SEPT", "HUIT", "NEUF" };
    private string[] correspondingValues = { "0", "2", "5", "7", "8", "9" };
    private int[] indexCode = new int[3];
    private string doorCode = "xxx";

    private Color[] noteColors = {
        new (1, 0, 0, 0.8f), //red
        new (0, 0, 1, 0.8f), //blue
        new (0, 1, 0, 0.8f), //green
    };

    private void Awake()
    {
        Random.InitState(46);

        noteTexts = new TMP_Text[notes.Length];

        for (int i = 0; i < notes.Length; i++)
        {
            noteTexts[i] = notes[i].GetComponentInChildren<TMP_Text>();
            notes[i].GetComponent<Renderer>().material.color = noteColors[i];
        }

        cipherAnimation = CipherWheelAnimatorParent.GetComponentInChildren<CipherWheelAnimation>();

        ChooseCode();
    }

    private void Start()
    {
        shifts = cipherAnimation.GetShifts();
        doorCode = correspondingValues[indexCode[0]] + correspondingValues[indexCode[1]] + correspondingValues[indexCode[2]];

        for (int i = 0; i < notes.Length; i++)
        {
            noteTexts[i].text = ConvertString(possibilities[indexCode[i]], 26 - shifts[i]);
        }

        Debug.Log("Shifts : " + shifts[0] + ";" + shifts[1] + ";" + shifts[2]);
        Debug.Log("Code : " + doorCode);
        Debug.Log("Don't look here");
    }

    private void ChooseCode()
    {
        for (int i = 0; i < indexCode.Length; i++)
        {
            indexCode[i] = Random.Range(0, possibilities.Length);
        }
    }

    private string ConvertString(string input, int shift)
    {
        byte[] codes = Encoding.ASCII.GetBytes(input);
        byte[] limits = Encoding.ASCII.GetBytes("AZ"); // Get ASCII limit codes

        for (int i = 0; i < codes.Length; i++)
        {
            codes[i] += (byte)shift; // Add shift
            if (codes[i] > limits[1])
            {
                codes[i] += (byte)(limits[0] - limits[1] - 1); // If goes over Z, go back to A
            }
        }

        string result = Encoding.ASCII.GetString(codes);

        return result;
    }

    public void ApplyPanels()
    {
        output.text = ConvertString(input.text, int.Parse(shift.text));

        if (code.text == doorCode)
        {
            door.GetComponent<Animator>().SetBool("open", true);
        }
        else
        {
            door.GetComponent<Animator>().SetBool("open", false);
        }
    }
}
