using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KeyMixer : MonoBehaviour
{
    [SerializeField] private GameObject mixedKeyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject player;
    private Color orange = new Color(1, 90f/255, 0);
    private Color cyan = new Color(93f/255, 202f / 255, 197f/255);
    private Color blue = new Color(147f / 255, 187f / 255, 1);
    private Color orange2 = new Color(1, 190f / 255, 96f / 255);
    private Color brown = new Color(121f/255, 99f/255, 0);
    private AudioSource sound;
    private GameObject[] keys = new GameObject[2];
    private int keysInside = 0;
    private ArrayList colorList = new();

    private void Awake()
    {
        sound = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (player.GetComponentInChildren<Interaction>().getIsHolding())
            {
                Debug.Log("Le joueur est en train de tenir une clé");
            }
            else
            {
                GameObject temp = other.gameObject;
                keys[keysInside] = temp;
                keysInside++;
                if (keysInside == 2)
                {
                    MixAndSpawnKey();
                    keysInside = 0;
                }
            }
        }
    }

    private void MixAndSpawnKey()
    {
        if (keys.Length < 2)
        {
            Debug.LogError("Il faut au moins deux clés pour les mélanger.");
            return;
        }

        KeyColor keyColor1 = keys[0].GetComponentInParent<KeyColor>();
        KeyColor keyColor2 = keys[1].GetComponentInParent<KeyColor>();

        colorList.Add(keyColor1.GetColor());
        colorList.Add(keyColor2.GetColor());

        Debug.Log("Color 1 : " + colorList[0] + "; Color 2 : " + colorList[1]);

        if (keyColor1 == null || keyColor2 == null)
        {
            Debug.LogError("One key color missing");
            return;
        }

        Color mixedColor = MixColors(keyColor1.GetColor(), keyColor2.GetColor());
        colorList.Clear();
        Debug.Log("New key color : " + mixedColor.ToString());

        if (mixedColor == Color.black)
        {
            Debug.Log("New key is black");
            Destroy(keys[0]);
            Destroy(keys[1]);
            return;
        }

        GameObject mixedKey = Instantiate(mixedKeyPrefab, spawnPoint.position, Quaternion.identity);
        sound.Play();

        if (mixedColor == brown)
        {
            mixedKey.tag = "FinaleKey";
        }else if (mixedColor == blue || mixedColor == orange2)
        {
            mixedKey.tag = "PersonalKey";
        }
        else
        {
            mixedKey.tag = "PrivateKey"; 
        }

        Renderer keyRenderer = mixedKey.GetComponentInChildren<Renderer>();
        keyRenderer.material.color = mixedColor;
        mixedKey.GetComponent<KeyColor>().SetColor(mixedColor);
        mixedKey.transform.localScale = new Vector3(2f, 2f, 2f);
        Destroy(keys[0]);
        Destroy(keys[1]);
    }


    private Color MixColors(Color color1, Color color2)
    {
        if (colorList.Contains(Color.yellow) && colorList.Contains(orange)) // yellow & orange
        {
            Debug.Log("yellow + orange = orange2 (+ cyan = brown)");
            return orange2;
        }
        else if (colorList.Contains(Color.yellow) && colorList.Contains(cyan)) //Yellow & cyan
        {
            Debug.Log("yellow + cyan = blue (+ orange = brown)");
            return blue;
        }
        else if (colorList.Contains(blue) && colorList.Contains(orange)) // blue & orage
        {
            Debug.Log("blue + orange");
            return brown;
        }
        else if (colorList.Contains(orange2) && colorList.Contains(cyan)) // orange2 & cyan
        {
            Debug.Log("cyan + orange2");
            return brown;
        }
        else
        {
            Debug.Log("You can't mix those color");
            return Color.black;
        }

    }
}