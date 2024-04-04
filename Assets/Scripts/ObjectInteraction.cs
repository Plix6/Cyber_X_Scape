using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private float throwForce = 500f;

    private Transform playerTransform;
    private Rigidbody objectRigidbody;
    private bool isHolding = false;

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
    }

    void PickUpObject()
    {
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log("Objet touché : " + hit.collider.gameObject.name); 
            if (hit.collider.TryGetComponent(out objectRigidbody) && objectRigidbody != null)
            {
                objectRigidbody.isKinematic = true;
                objectRigidbody.position = playerTransform.position + playerTransform.forward * 2f;
                objectRigidbody.transform.parent = playerTransform;
                isHolding = true;
            }
        }
        else
        {
            Debug.Log("Aucun objet touché."); 
        }
    }

    void DropObject()
    {
        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = false;
            objectRigidbody.transform.parent = null;
            isHolding = false;
        }
    }

    void ThrowObject()
    {
        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = false;
            objectRigidbody.transform.parent = null;
            objectRigidbody.velocity = playerTransform.forward * throwForce;
            isHolding = false;
        }
    }

    void Update()
    {
        if (!isHolding && Input.GetMouseButtonDown(0))
        {
            PickUpObject();
        }
        else if (isHolding && Input.GetMouseButtonDown(1))
        {
            DropObject();
        }
        else if (isHolding && Input.GetMouseButtonDown(0))
        {
            ThrowObject();
        }
    }

}
