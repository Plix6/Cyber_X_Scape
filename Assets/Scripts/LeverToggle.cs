using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverToggle : MonoBehaviour
{
    private Color ON_COLOR = Color.yellow;
    private Color OFF_COLOR = Color.gray;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject serverSideCable;
    [SerializeField] private GameObject panelSideCable;
    private Renderer[] renderers;

    public bool startingPositionOn = false;

    private void Awake()
    {
        renderers = panelSideCable.GetComponentsInChildren<Renderer>();
    }

    private void Start()
    {
        serverSideCable.GetComponentInChildren<Renderer>().material.color = ON_COLOR;

        if (startingPositionOn)
        {
            ToggleLeverState();
        }
    }

    private void OnMouseUpAsButton()
    {
        ToggleLeverState();
    }

    private IEnumerator updateCableColor()
    {
        yield return new WaitForSeconds(1);

        ChangeCableColor(animator.GetBool("Activated"));

        yield return null;
    }

    private void ChangeCableColor(bool on)
    {
        foreach (Renderer rd in renderers)
        {
            if (on)
            {
                rd.material.color = ON_COLOR; 
            }
            else
            {
                rd.material.color = OFF_COLOR;
            }
        }
    }

    private void ToggleLeverState()
    {
        animator.SetBool("Activated", !animator.GetBool("Activated"));
        StartCoroutine(updateCableColor());
    }
}
