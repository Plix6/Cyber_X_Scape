using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text instruction;
    [SerializeField] private Camera camera_;

    private string TAG_AIMED = "ScreenInteractable";
    private float MAX_RANGE = 3f;

    private void Awake()
    {
        // camera_ = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit))
        {
            Debug.Log("Found an object - distance: " + hit.distance);
            Debug.DrawRay(camera_.transform.position, camera_.transform.forward * hit.distance, Color.white);
            if (hit.transform.CompareTag(TAG_AIMED) & hit.distance < MAX_RANGE)
            {
                instruction.gameObject.SetActive(true);
            }
            else
            {
                instruction.gameObject.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
