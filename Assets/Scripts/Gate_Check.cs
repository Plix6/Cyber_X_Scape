using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Check : MonoBehaviour
{
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform keyTransform = FindChildWithTag(other.transform, "PrivateKey");
            Transform finaleKey = FindChildWithTag(other.transform,"FinaleKey");

            if (keyTransform != null)
            {
                other.GetComponent<ObjectInteraction>().DropObject();
                Destroy(keyTransform.gameObject);
                sound.Play();
            }
            if (finaleKey != null)
            {
                other.GetComponent<ObjectInteraction>().DropObject();
                Destroy(finaleKey.gameObject);
                sound.Play();

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
