using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform holdArea;
    [SerializeField] private float pickupRange = 8.0f;
    [SerializeField] private float pickupForce = 150.0f;
    public bool isHolding = false;
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    public bool key_1 = false;
    public bool key_2 = false;

    public bool getIsHolding()
    {
        return isHolding;
    }

    public void setKey_1()
    {
        key_1 = true;
    }

    public bool getKey_1()
    {
        return key_1;
    }

    public void setKey_2()
    {
        key_2 = true;
    }

    public bool getKey_2()
    {
        return key_2;
    }

    private void setPhysics(bool useGravity, int dragForce, Transform parent)
    {
        heldObjRB.useGravity = useGravity;
        heldObjRB.drag = dragForce;

        if (heldObjRB.CompareTag("Key"))
        {
            heldObjRB.transform.parent.parent = parent;
        }
        else if (heldObjRB.CompareTag("SecretFile"))
        {
            heldObjRB.transform.parent = parent;
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if (heldObj != null)
        {
            MoveObject();
        }
    }


    private void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    private void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            setPhysics(false, 10, holdArea);
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObj = pickObj;
            isHolding = true;
        }
    }

    public void DropObject()
    {
        if (heldObjRB != null)
        {
            setPhysics(true, 1, null);
            heldObjRB.constraints = RigidbodyConstraints.None;
            heldObj = null;
            isHolding = false;
        }
    }

}
