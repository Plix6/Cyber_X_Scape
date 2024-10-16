using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallWayDoorDetection : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private Animator animator;

    private void Awake()
    {
        animator = door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);

        animator.SetBool("open", false);

        yield return null;
    }
}
