using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject Animation;
    public GameObject Trigger;
    public AudioSource DoorSound;
    public bool Action = false;
    void Start()
    {
        Instruction.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ( collision.transform.tag == "Player")
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Action == true)
        {
            Instruction.SetActive(false);
            Animation.GetComponent<Animator>().Play("DoorOpen");
            Trigger.SetActive(false);
            DoorSound.Play();
            Action = false;
        }
    }
}
