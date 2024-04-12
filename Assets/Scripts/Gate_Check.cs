using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Check : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform keyTransform = FindChildWithTag(other.transform, "PrivateKey");

            if (keyTransform != null)
            {
                other.GetComponent<ObjectInteraction>().DropObject();
                Destroy(keyTransform.gameObject);
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
