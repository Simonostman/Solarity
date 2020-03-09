using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuroraTest : MonoBehaviour
{
    public GameObject aurora;
    public int hue;

    void Update()
    {
        Color[] c = aurora.GetComponent<AuroraTest2>().GetColours();

        GetComponent<Image>().color = c[hue];
    }
}
