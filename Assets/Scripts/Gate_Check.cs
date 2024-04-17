using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Check : MonoBehaviour
{
    [SerializeField] private GameObject instruction;
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponentInChildren<AudioSource>();
        instruction.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform keyTransform = FindChildWithTag(other.transform, "PrivateKey");
            Transform finaleKey = FindChildWithTag(other.transform,"FinaleKey");

            if (keyTransform != null)
            {
                other.GetComponentInChildren<Interaction>().DropObject();
                Destroy(keyTransform.gameObject);
                StartCoroutine(Delay());
                sound.Play();
            }
            if (finaleKey != null)
            {
                other.GetComponentInChildren<Interaction>().DropObject();
                Destroy(finaleKey.gameObject);
                StartCoroutine(Delay());
                sound.Play();

            }
        }
    }

    private IEnumerator Delay()
    {
        instruction.SetActive(true);
        yield return new WaitForSeconds(5);
        instruction.SetActive(false);
        yield return null;
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
