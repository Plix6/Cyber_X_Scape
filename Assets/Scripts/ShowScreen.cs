using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active & Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        active = false;
    }
}
