using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEntry : MonoBehaviour
{
    [SerializeField] private GameObject playerDialogObject;
    [SerializeField] private GameObject exitTrigger;
    private AudioSource playerDialog;

    private bool dialogStarted = false;

    private void Awake()
    {
        exitTrigger.GetComponent<OpenDoor>().active = false;
        playerDialog = playerDialogObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dialogStarted)
        {
            playerDialog.Play();
            StartCoroutine(Delay());
        }
        dialogStarted = true;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(playerDialog.clip.length);

        exitTrigger.GetComponent<OpenDoor>().active = true;

        yield return null;
    }
}
