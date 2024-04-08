using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyMixer : MonoBehaviour
{
    [SerializeField] private GameObject mixedKeyPrefab;
    [SerializeField] private Transform spawnPoint;
    private Color orange = new Color(1, 90f/255, 0);
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

        GameObject mixedKey = Instantiate(mixedKeyPrefab, spawnPoint.position, Quaternion.identity);
        Renderer keyRenderer = mixedKey.GetComponentInChildren<Renderer>();
        keyRenderer.material.color = mixedColor;

    }


    private Color MixColors(Color color1, Color color2)
    {
        Color mixedColor;
        if (color1.CompareRGB(Color.yellow) &&  color2.CompareRGB(orange) || color1.CompareRGB(orange) && color2.CompareRGB(Color.yellow)) 
        {
            return mixedColor = new Color(1, 190f/255, 96f/255);
        }
        else
        {
            return 1;
        }
        
    }
}