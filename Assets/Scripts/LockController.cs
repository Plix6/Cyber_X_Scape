using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class LockController : MonoBehaviour
{
    [SerializeField] private GameObject message1; // Appuyer pour
    [SerializeField] private GameObject message2; // Vous n'avez pas la clé
    [SerializeField] private GameObject message3; // Vous avez désactivé la caméra
    [SerializeField] private GameObject camera_field; 
    private bool canInteractWith = false;
    private bool hasFinaleKey = false;
    private Collider player;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.transform.CompareTag("Player"))
        {
            player = other;
            message1.SetActive(true);
            canInteractWith = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        message1.SetActive(false);
        message2.SetActive(false);
        message3.SetActive(false);
        canInteractWith = false;
    }

    private void CheckFinaleKey(Transform playerTransform)
    {
        message1.SetActive(false);
        Transform FinaleKey = FindChildWithTag(playerTransform, "FinaleKey");
        Debug.Log(FinaleKey);
        if (FinaleKey != null)
        {
            hasFinaleKey = true;
            if (playerTransform.TryGetComponent<ObjectInteraction>(out var objectInteraction))
            {
                objectInteraction.DropObject();
            }
            Destroy(FinaleKey.gameObject);
            Destroy(camera_field);
        }
        else
        {
            message2.SetActive(true);
        }
    }

    void Start()
    {
        message1.SetActive(false);
        message2.SetActive(false);
        message3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteractWith)
        {
            if (player.CompareTag("Player"))
            {
                CheckFinaleKey(player.transform);
            }
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
