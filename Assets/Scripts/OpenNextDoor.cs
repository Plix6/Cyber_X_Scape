using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNextDoor : MonoBehaviour
{
    [SerializeField] private GameObject nextDoor;
    private Animator animator;

    private void Awake()
    {
        if (nextDoor != null)
        animator = nextDoor.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (animator != null)
        animator.SetBool("open", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (animator != null)
        animator.SetBool("open", false);
    }
}
