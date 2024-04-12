using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableRotation : MonoBehaviour
{
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject separation;
    private void RotateTable()
    {
        LeanTween.rotateAroundLocal(table,
                Vector3.up,
                180,
                3).setEaseInOutCubic();
        LeanTween.rotateAroundLocal(separation,
                Vector3.up,
                180,
                3).setEaseInOutCubic();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            RotateTable();
        }
        
    }
}
