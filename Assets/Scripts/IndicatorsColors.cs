using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorsColors : MonoBehaviour
{
    [SerializeField] private GameObject[] indicators;

    private Renderer[] renderers = new Renderer[3];

    private Color[] indicatorColors = {
        new (205 / 255f, 28 / 255f, 19/255f, 0.8f), //red
        new (66 / 255f, 91 / 255f, 207 / 255f, 0.8f), //blue
        new (63 / 255f, 193 / 255f, 76 / 255f, 0.8f), //green
    };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            renderers[i] = indicators[i].GetComponent<Renderer>();
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = indicatorColors[i];
        }
    }
}
