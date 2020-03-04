using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectPositionHandler : MonoBehaviour
{
    public GameObject effect;
    public GameObject target;

    private bool stopped = false;
    private float deathTime = 5.0f;
    private float deathTimer = 0;

    private void FixedUpdate()
    {
        if(stopped)
        {
            deathTimer += Time.deltaTime % 60;
            if(deathTimer > deathTime)
            {
                Debug.Log("Destroy");
                Destroy(gameObject);
            }
        }
    }

    public void Stop()
    {
        effect.GetComponent<VisualEffect>().Stop();
        stopped = true;
    }
}
