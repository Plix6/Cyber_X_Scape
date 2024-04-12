using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SafeScript : MonoBehaviour
{
    [SerializeField] private GameObject message1;
    [SerializeField] private GameObject message2;
    private Collider player;
    private bool hasSecretFile = false;
    private bool canInteractWithE = false; 


    private void OnTriggerEnter(Collider other)
    {
        player = other;
        if (other.CompareTag("Player"))
        {
            message1.SetActive(true);
            canInteractWithE = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        message1.SetActive(false);
        message2.SetActive(false);
        canInteractWithE = false;
    }

    private void Start()
    {
        message1.SetActive(false);
        message2.SetActive(false);
    }

    private void Update()
    {
        if (canInteractWithE && Input.GetKeyDown(KeyCode.E))
        {
            if (player.CompareTag("Player"))
            {
                CheckSecretFile(player.transform);
            }
        }
    }

    private void CheckSecretFile(Transform playerTransform)
    {
        message1.SetActive(false);
        Transform secretFile = FindChildWithTag(playerTransform, "SecretFile");
        Debug.Log(secretFile);
        if (secretFile != null)
        {
            hasSecretFile = true;
            if (playerTransform.TryGetComponent<ObjectInteraction>(out var objectInteraction))
            {
                objectInteraction.DropObject();
            }
            Destroy(secretFile.gameObject);
            playerTransform.GetComponent<ObjectInteraction>().setHasFinished();
        }
        else
        {
            message2.SetActive(true);
        }
    }

    private Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
            Transform result = FindChildWithTag(child, tag);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}
