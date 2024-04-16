using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager1 : MonoBehaviour
{
    [SerializeField] private GameObject triggerHallwayExit;
    [SerializeField] private GameObject playerDialogObject;

    [SerializeField] private AudioClip startDialog;
    [SerializeField] private AudioClip hintDialog;
    [SerializeField] private AudioClip endDialog;

    [SerializeField] private int minutesBeforeHint;

    private HallwayTrigger hallwayTrigger;
    private AudioSource playerDialog;

    private bool roomStarted;
    private bool roomFinished;

    private void Awake()
    {
        hallwayTrigger = triggerHallwayExit.GetComponent<HallwayTrigger>();
        playerDialog = playerDialogObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!roomStarted)
        {
            playerDialog.clip = startDialog;
            playerDialog.Play();
            StartCoroutine(HintDelay());
        }
        roomStarted = true;
    }

    private IEnumerator HintDelay()
    {
        yield return new WaitForSeconds(minutesBeforeHint * 60);

        if (!roomFinished)
        {
            playerDialog.clip = hintDialog;
            playerDialog.Play();
        }

        yield return null;
    }

    public void FinishRoom()
    {
        if (!roomFinished)
        {
            playerDialog.Stop();
            playerDialog.clip = endDialog;
            playerDialog.Play();
            hallwayTrigger.ToggleExit();
        }
        roomFinished = true;
    }
}

