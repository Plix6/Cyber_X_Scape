using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverToggle : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnMouseUpAsButton()
    {
        animator.SetBool("Activated", !animator.GetBool("Activated"));
    }
}
