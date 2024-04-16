using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject cipherManagerObject;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject triggerHallwayExit;
    [SerializeField] private GameObject playerDialogObject;

    [SerializeField] private AudioClip startDialog;
    [SerializeField] private AudioClip hintDialog;
    [SerializeField] private AudioClip endDialog;

    [SerializeField] private int minutesBeforeHint;

    private CipherManager cipherManager;
    private InteractScreen interactScreen;
    private HallwayTrigger hallwayTrigger;
    private AudioSource playerDialog;

    private bool roomStarted;
    private bool roomFinished;

    private void Awake()
    {
        cipherManager = cipherManagerObject.GetComponent<CipherManager>();
        interactScreen = playerObject.GetComponent<InteractScreen>();
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
            StartCoroutine(StartDelay());
            StartCoroutine(HintDelay());
        }
        roomStarted = true;
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDialog.length);

        cipherManager.InitializeCode();
        interactScreen.ToggleActive();

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

