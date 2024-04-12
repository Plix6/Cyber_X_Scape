using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject message;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform keyTransform = FindChildWithTag(other.transform, "SecretFile");

            if (keyTransform != null)
            {
                message.SetActive(true); // Rajouter un wait ou qqch du genre
                other.GetComponent<ObjectInteraction>().DropObject();
                Destroy(keyTransform.gameObject);
                GameObject newKey = Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity, parent.transform);
            }
        }
    }

    private void Start()
    {
        message.SetActive(false);
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
