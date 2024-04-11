using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text instruction;
    [SerializeField] private Camera camera_;
    [SerializeField] private GameObject screenPanel;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private GameObject inputControlManager;
    [SerializeField] private TMP_Text placeholder;
    [SerializeField] private GameObject cipherManagerObject;

    private string TAG_AIMED = "ScreenInteractable";
    private float MAX_RANGE = 3f;
    private PlayerMovement movement;
    private MouseLook mouse;
    private bool screenDetected = false;
    private bool screenActivated = false;
    private GameObject target;

    private RetrieveScreenText retriever;
    private InputControl inputControl;
    private CipherManager cipherManager;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        mouse = GetComponentInChildren<MouseLook>();
        inputControl = inputControlManager.GetComponent<InputControl>();
        input.onValidateInput = inputControl.CustomValidator; // sets custom validator
        cipherManager = cipherManagerObject.GetComponentInChildren<CipherManager>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit))
        {/*
            Debug.Log("Found an object - distance: " + hit.distance);
            Debug.DrawRay(camera_.transform.position, camera_.transform.forward * hit.distance, Color.white);*/
            if (hit.transform.CompareTag(TAG_AIMED) & hit.distance < MAX_RANGE)
            {
                instruction.gameObject.SetActive(true);
                screenDetected = true;
                target = hit.transform.gameObject;
            }
            else
            {
                instruction.gameObject.SetActive(false);
                screenDetected = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (screenDetected && Input.GetKeyUp(KeyCode.E) & !screenActivated)
        {
            SetupScreen();
        }

        if (screenActivated && Input.GetKeyUp(KeyCode.Escape))
        {
            RemoveScreen();
        }

        if (screenActivated && (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter)))
        {
            RemoveScreen();
            retriever.setText(input.text);
            cipherManager.ApplyPanels();
        }
    }

    private void SetupScreen()
    {
        retriever = target.GetComponent<RetrieveScreenText>();
        input.text = string.Empty;
        placeholder.text = retriever.getText();
        inputControl.SetOptions(retriever.isNumber(), retriever.isShift());
        ToggleScreen();
        input.ActivateInputField();
    }

    private void RemoveScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ToggleScreen();
        input.DeactivateInputField();
    }

    private void ToggleScreen()
    {
        movement.ToggleMovement();
        mouse.ToggleMovement();
        screenActivated = !screenActivated;
        screenPanel.SetActive(!screenPanel.activeSelf);
    }
}
