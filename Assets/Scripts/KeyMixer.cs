using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyMixer : MonoBehaviour
{
    [SerializeField] private GameObject mixedKeyPrefab;
    [SerializeField] private Transform spawnPoint;
    private Color orange = new Color(1, 90f/255, 0);
    private Color cyan = new Color(93f/255, 202f / 255, 197f/255);
    private Color blue = new Color(147f / 255, 187f / 255, 1);
    private Color orange2 = new Color(1, 190f / 255, 96f / 255);
    private Color brown = new Color(121f/255, 99f/255, 0);
    private GameObject[] keys = new GameObject[2];
    private int keysInside = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
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

    private void MixAndSpawnKey()
    {
        if (keys.Length < 2)
        {
            Debug.LogError("Il faut au moins deux clés pour les mélanger.");
            return;
        }

        KeyColor keyColor1 = keys[0].GetComponentInParent<KeyColor>();
        KeyColor keyColor2 = keys[1].GetComponentInParent<KeyColor>();

        if (keyColor1 == null || keyColor2 == null)
        {
            Debug.LogError("L'un des objets clés ne possède pas le composant KeyColor.");
            return;
        }

        Color mixedColor = MixColors(keyColor1.GetColor(), keyColor2.GetColor());
        Debug.Log(mixedColor.ToString());
        if (mixedColor == Color.black)
        {
            Debug.Log("Not working");
            Destroy(keys[0]);
            Destroy(keys[1]);
        }
        else
        {
            GameObject mixedKey = Instantiate(mixedKeyPrefab, spawnPoint.position, Quaternion.identity);
            Renderer keyRenderer = mixedKey.GetComponentInChildren<Renderer>();
            keyRenderer.material.color = mixedColor;
            mixedKey.transform.localScale = new Vector3(2f, 2f, 2f);
            mixedKey.tag = "PrivateKey";
            Destroy(keys[0]);
            Destroy(keys[1]);
        }
    }


    private Color MixColors(Color color1, Color color2)
    {
        if (color1 == Color.yellow &&  color2 == orange || color1 == orange && color2 == Color.yellow) 
        {
            return orange2;
        }
        else if (color1 == Color.yellow && color2 == cyan || color1 == cyan && color2 == Color.yellow)
        {
            return blue;
        }
        else if (color1 == blue && color2 == orange || color1 == orange && color2 == blue)
        {
            return brown;
        }
        else if (color1 == orange2 && color2 == cyan || color1 == cyan && color2 == orange2)
        {
            return brown;
        }
        else
        {
            Debug.Log("You can't mix those color");
            return Color.black;
        }

    }
}