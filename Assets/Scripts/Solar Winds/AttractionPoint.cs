using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionPoint : MonoBehaviour
{
    public bool activated = true;
    public float gravityStrenght = 10;

    void Start()
    {
        
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
            activated = !activated;
    }

    void Update()
    {
        
    }
}
