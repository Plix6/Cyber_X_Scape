using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HallwayTrigger : MonoBehaviour
{
    [SerializeField] private bool isExit = false;
    [SerializeField] private string newSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (isExit && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(newSceneName);
        }
    }

    public void ToggleExit()
    {
        isExit = !isExit;
    }
}
