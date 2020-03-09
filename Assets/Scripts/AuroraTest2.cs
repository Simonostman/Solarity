using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AuroraTest2 : MonoBehaviour
{
    private Aurora aurora;

    private void Start()
    {
        aurora = new Aurora();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            aurora.AddIntensity(0.002f);
        }
    }

    public Color[] GetColours()
    {
        return aurora.GetColours();
    }
}
