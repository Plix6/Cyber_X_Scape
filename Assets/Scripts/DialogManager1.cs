using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager1 : MonoBehaviour
{
    [SerializeField] private GameObject triggerHallwayExit;
    [SerializeField] private GameObject playerDialogObject;
    [SerializeField] private GameObject instruction;

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
        instruction.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!roomStarted)
        {
            StartCoroutine(Delay());
            playerDialog.clip = startDialog;
            playerDialog.Play();
            StartCoroutine(HintDelay());
        }
        roomStarted = true;
    }

    private IEnumerator Delay()
    {
        instruction.SetActive(true);
        yield return new WaitForSeconds(6);
        instruction.SetActive(false);
        yield return null;
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

