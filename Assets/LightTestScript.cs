using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LightTestScript : MonoBehaviour
{
    private VisualEffect effect;
    private Aurora aurora;

    private void Start()
    {
        effect = GetComponent<VisualEffect>();

        aurora = new Aurora();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            aurora.AddIntensity(0.002f);
        }

        effect.SetGradient("Hue", aurora.GetGradient());
    }
}
