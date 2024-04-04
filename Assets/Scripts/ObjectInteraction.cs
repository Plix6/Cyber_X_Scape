using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private float throwForce = 0.1f;
    [SerializeField] private Camera camera_;
    private Transform playerTransform;
    private Rigidbody objectRigidbody;
    private bool isHolding = false;

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
    }

    void PickUpObject()
    {
        float maxRaycastDistance = 5f;
        Camera cam = Camera.main;
        Vector3 cameraDirection = cam.transform.forward;


        if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out RaycastHit hit, maxRaycastDistance))
        {
            Debug.Log("Objet touché : " + hit.collider.gameObject.name);
            Debug.DrawRay(playerTransform.position, playerTransform.forward, Color.white);
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
