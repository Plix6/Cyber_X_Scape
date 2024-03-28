using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] public GameObject Instruction;
    [SerializeField] public GameObject Animation;
    private Animator animator;
    //public AudioSource DoorSound;
    public bool Action = false;

    private void Awake()
    {
        animator = Animation.GetComponent<Animator>();
    }

    void Start()
    {
        Instruction.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Instruction.SetActive(true);
            Action = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false) ;
        Action = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (Action == true)
            {
                Instruction.SetActive(false);
                //DoorSound.Play();
                Action = false;
                animator.SetBool("open", !animator.GetBool("open"));
            }

        }
       
    }
}
