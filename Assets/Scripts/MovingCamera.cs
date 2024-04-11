using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private float speed = 2f;

    private Transform currentTarget; 

    void Start()
    {
        currentTarget = target1;
    }

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


void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if (transform.position == currentTarget.position)
        {
            currentTarget = (currentTarget == target1) ? target2 : target1;
        }
    }
}
