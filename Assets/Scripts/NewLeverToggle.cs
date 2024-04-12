using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLeverToggle : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Rajoute tes gameObjects à changer ici

    public bool startingPositionOn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (startingPositionOn)
        {
            ToggleLeverState();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) // Change la condition selon ce que tu veux
        {
            ToggleLeverState();
        }
    }

    private void ToggleLeverState()
    {
        animator.SetBool("Activated", !animator.GetBool("Activated"));
        StartCoroutine(updateObjects());
    }

    private IEnumerator updateObjects()
    {
        yield return new WaitForSeconds(1);

        // Change tes objects ici
        // (la coroutine sert à effectuer les changements après la fin de l'animation)

        yield return null;
    }
}
