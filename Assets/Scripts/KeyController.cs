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
    private AudioSource sound;
    private Color orange = new Color(1, 90f / 255, 0);
    private Color cyan = new Color(93f / 255, 202f / 255, 197f / 255);
    public bool Action = false;
    private bool canInteractWith = false;

    private void Awake()
    {
        sound = GetComponentInChildren<AudioSource>();
    }

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
            canInteractWith = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
        Action = false;
        canInteractWith = false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        yield return null;
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
                keyColor = orange;
                break;
            case "blue":
                keyColor = cyan;
                break;
            case "yellow_2":
                keyColor = Color.yellow;
                break;
            default:
                Debug.LogError("Couleur de distributeur inconnu: " + distributorColor);
                return; 
        }
        StartCoroutine(Delay());
        sound.Play();
        GameObject newKey = Instantiate(keyPrefab, spawnPoint.transform.position, Quaternion.identity, Distributor.transform);
        newKey.GetComponent<KeyColor>().SetColor(keyColor);
        Renderer keyRenderer = newKey.GetComponentInChildren<Renderer>();
        keyRenderer.material.color = keyColor;
        newKey.tag = "Key";
        if (keyColor == orange || keyColor == cyan)
        {
            newKey.tag = "PrivateKey";
        }
        Transform child = newKey.transform.GetChild(0);
        child.gameObject.tag = "Key";
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
