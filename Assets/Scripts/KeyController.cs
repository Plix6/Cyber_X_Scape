using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private GameObject Instruction;
    [SerializeField] private GameObject Distributor;
    [SerializeField] private GameObject Key;
    [SerializeField] private GameObject SpawnPoint;
    public bool Action = false;

    // Start is called before the first frame update
    void Start()
    {
        Instruction.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }

    private void GenerateKey(GameObject distributor, GameObject keyPrefab, GameObject spawnPoint)
    {
        string distributorColor = distributor.gameObject.name;
        Color keyColor;
        

        switch (distributorColor)
        {
            case "yellow_1":
                keyColor = Color.yellow;
                break;
            case "orange":
                keyColor = new Color(1, 90f/255, 0);
                break;
            case "blue":
                keyColor = Color.blue;
                break;
            case "yellow_2":
                keyColor = Color.yellow;
                break;
            default:
                Debug.LogError("Couleur de distributeur inconny: " + distributorColor);
                return; 
        }

        GameObject newKey = Instantiate(keyPrefab, spawnPoint.transform.position, Quaternion.identity, Distributor.transform);
        Renderer keyRenderer = newKey.GetComponentInChildren<Renderer>();
        keyRenderer.material.color = keyColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                GenerateKey(Distributor, Key, SpawnPoint);
                Instruction.SetActive(false);
                Action = false;
            }
        }
    }
}
