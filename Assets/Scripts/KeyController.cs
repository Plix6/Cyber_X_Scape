using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private GameObject Instruction;


    // Start is called before the first frame update
    void Start()
    {
        Instruction.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Instruction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Instruction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
