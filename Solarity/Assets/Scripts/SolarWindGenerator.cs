using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarWindGenerator : MonoBehaviour
{

    public class SolarWind {
        ParticleSystem particle;
        Vector3 forwardVector;
        Transform transform;
    }


    void Start()
    {
        SolarWind solarWind = new SolarWind();
    }

    void Update()
    {
        
    }
}
