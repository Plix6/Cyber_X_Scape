using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverToggle : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject cablePrefab;
    private Renderer[] renderers;

    private void Awake()
    {
        renderers = cablePrefab.GetComponentsInChildren<Renderer>();
    }

    private void Start()
    {
        ChangeCableColor(Color.gray);
    }

    private void OnMouseUpAsButton()
    {
        animator.SetBool("Activated", !animator.GetBool("Activated"));
        StartCoroutine(updateCableColor());
    }

    private IEnumerator updateCableColor()
    {
        yield return new WaitForSeconds(1);
        if (animator.GetBool("Activated"))
        {
            ChangeCableColor(Color.yellow);
        } else
        {
            ChangeCableColor(Color.gray);
        }
        yield return null;
    }

    private void ChangeCableColor(Color newColor)
    {
        foreach (Renderer rd in renderers)
        {
            rd.material.color = newColor;
        }
    }
}
