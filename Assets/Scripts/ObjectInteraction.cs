using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private Camera camera_;
    private Rigidbody objectRigidbody;
    private bool isHolding = false;

    protected void SetObjectPhysics(bool isKinematic, bool useGravity, Transform parent)
    {
        objectRigidbody.isKinematic = isKinematic;
        objectRigidbody.useGravity = useGravity;
        objectRigidbody.transform.parent.parent = parent;
    }

    private void PickUpObject()
    {
        float maxRaycastDistance = 5f;
        Vector3 cameraDirection = camera_.transform.forward;

        if (Physics.Raycast(camera_.transform.position, cameraDirection, out RaycastHit hit, maxRaycastDistance))
        {
            Debug.Log("Objet touché : " + hit.collider.gameObject.name);

            if (hit.collider.TryGetComponent(out objectRigidbody) && objectRigidbody != null)
            {
                SetObjectPhysics(true, false, camera_.transform);
                objectRigidbody.position = camera_.transform.position + camera_.transform.forward * .5f;
                isHolding = true;
            }
        }
        else
        {
            Debug.Log("Aucun objet touché.");
        }
    }

    public void DropObject()
    {
        if (objectRigidbody != null)
        {
            SetObjectPhysics(false, true, null);
            isHolding = false;
        }
    }

    private void ThrowObject()
    {
        if (objectRigidbody != null)
        {
            SetObjectPhysics(false, true, null);
            objectRigidbody.velocity = camera_.transform.forward;
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
