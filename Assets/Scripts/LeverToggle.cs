using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeverToggle : MonoBehaviour
{
    private Color ON_COLOR = Color.yellow;
    private Color OFF_COLOR = Color.gray;

    private string INTERACTABLE_TAG = "ScreenInteractable";

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject serverSideCable;
    [SerializeField] private GameObject panelSideCable;
    private Renderer[] renderers;

    public bool startingPositionOn = false;

    [SerializeField] private TMP_Text connectedText;
    [SerializeField] private GameObject interactor;

    private void Awake()
    {
        if (panelSideCable != null)
        {
            renderers = panelSideCable.GetComponentsInChildren<Renderer>();
        }
    }

    private void Start()
    {
        if (serverSideCable != null)
        {
            serverSideCable.GetComponentInChildren<Renderer>().material.color = ON_COLOR;
        }

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
        ToggleConnectedObjects();

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

    private void ToggleConnectedObjects()
    {
        if (connectedText != null)
        {
            connectedText.gameObject.SetActive(!connectedText.gameObject.activeSelf);
        }
        if (interactor != null)
        {
            if (interactor.CompareTag(INTERACTABLE_TAG))
            {
                interactor.tag = "Untagged";
            }
            else
            {
                interactor.tag = INTERACTABLE_TAG;
            }
        }
    }

    private void ToggleLeverState()
    {
        animator.SetBool("Activated", !animator.GetBool("Activated"));
        if (serverSideCable != null && panelSideCable != null) 
        {
            StartCoroutine(updateCableColor());
        }
    }
}
