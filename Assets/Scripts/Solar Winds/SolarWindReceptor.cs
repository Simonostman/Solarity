using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarWindReceptor : MonoBehaviour
{
    public float windIntensity;
    public float marginOfError;
    public float timeWindow;
    public float goalIntensity;

    private float timeTime;
    private float currentIntensity;
    private bool achievedTarget;

    void Start()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Wind")
        {
            currentIntensity += windIntensity;
            Destroy(other);
        }
    }

    public bool DidWeWin()
    {
        return achievedTarget;
    }
}
