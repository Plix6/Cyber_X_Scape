using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController1 : MonoBehaviour
{
    [SerializeField] private GameObject message1; // Appuyer pour
    [SerializeField] private GameObject message2; // Vous n'avez pas la clé
    [SerializeField] private GameObject message3; // Vous avez désactivé la caméra
    [SerializeField] private GameObject camera_field;
    private AudioSource[] audio = new AudioSource[2];
    private bool canInteractWith = false;
    private bool hasFinaleKey = false;
    private Collider player;

    private void Awake()
    {
        audio = GetComponentsInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.transform.CompareTag("Player"))
        {
            player = other;
            message1.SetActive(true);
            canInteractWith = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        message1.SetActive(false);
        message2.SetActive(false);
        message3.SetActive(false);
        canInteractWith = false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(6);
        DestroyCamera();
        yield return null;
    }

    private void DestroyCamera()
    {
        Destroy(camera_field);
    }

    private void CheckFinaleKey(Transform playerTransform)
    {
        message1.SetActive(false);
        Transform holdArea = FindChildWithTag(playerTransform, "holdArea");

        Transform FinaleKey = FindChildWithTag(holdArea, "FinaleKey");
        Debug.Log("Finale key is :",FinaleKey);

        if (FinaleKey == null || player.GetComponentInChildren<Interaction>().getKey_2())
        {
            message2.SetActive(true);
            return;
        }

        hasFinaleKey = true;
        var objectInteraction = playerTransform.GetComponentInChildren<Interaction>();
        if (objectInteraction != null)
        {
            objectInteraction.DropObject();
            Destroy(FinaleKey.gameObject);
            audio[0].Play();
            player.GetComponentInChildren<Interaction>().setKey_2();

            if (player.GetComponentInChildren<Interaction>().getKey_1())
            {
                audio[1].Play();
                StartCoroutine(Delay());
            }
        }

    }

    void Start()
    {
        message1.SetActive(false);
        message2.SetActive(false);
        message3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteractWith)
        {
            if (player.CompareTag("Player"))
            {
                CheckFinaleKey(player.transform);
            }
        }
    }

    private Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
            Transform result = FindChildWithTag(child, tag);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}
