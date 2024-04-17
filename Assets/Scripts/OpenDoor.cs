using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] public GameObject instruction;
    [SerializeField] public GameObject Animation;
    private Animator animator;
    //public AudioSource DoorSound;
    public bool action = false;
    public bool active = false;

    private void Awake()
    {
        animator = Animation.GetComponent<Animator>();
    }

    void Start()
    {
        instruction.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (active && other.CompareTag("Player"))
        {
            instruction.SetActive(true);
            action = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        instruction.SetActive(false);
        action = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && action)
        {
            instruction.SetActive(false);
            action = false;
            animator.SetBool("open", !animator.GetBool("open"));
        }
    }
}
