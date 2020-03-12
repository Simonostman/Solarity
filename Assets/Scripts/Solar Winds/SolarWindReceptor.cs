using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SolarWindReceptor : MonoBehaviour
{
    public float windIntensity;
    public float marginOfError;
    public float timeWindow;
    public float goalIntensity;

    private float timeTime;
    private float currentIntensity;
    private bool achievedTarget;

    private Aurora aurora;
    public GameObject northernLights;

    void Start()
    {
        aurora = new Aurora();
        //northernLights = transform.Find("Northern Lights").gameObject;

        timeTime = 0f;
        achievedTarget = false;
    }

    void Update()
    {
        if (timeTime < timeWindow)
        {
            timeTime += Time.deltaTime;
        }
        else
        {
            if (currentIntensity / timeWindow < goalIntensity + marginOfError && currentIntensity / timeWindow > goalIntensity - marginOfError)
            {
                achievedTarget = true;
                Debug.Log("Achieved target");
            }
            else
            {
                timeTime -= timeWindow;
                Debug.Log(currentIntensity);
                currentIntensity = 0f;
            }
        }

        //aurora.SetIntensity(currentIntensity / timeWindow);
        aurora.SetIntensity(currentIntensity/timeWindow);

        northernLights.GetComponent<VisualEffect>().SetGradient("Hue", aurora.GetGradient());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Wind")
        {
            currentIntensity += windIntensity;
            Debug.Log("DESTROYED");
            Destroy(other);
        }
    }

    public bool DidWeWin()
    {
        return achievedTarget;
    }
}
