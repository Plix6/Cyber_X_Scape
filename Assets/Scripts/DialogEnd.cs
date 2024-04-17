using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEnd : MonoBehaviour
{
    [SerializeField] private GameObject playerDialogObject;
    private AudioSource playerDialog;

    private void Awake()
    {
        playerDialog = playerDialogObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerDialog.Play();
    }
}
