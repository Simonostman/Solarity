using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class AttractionPoint : MonoBehaviour
{
    public bool negativePolarity = true;
    [Range(0.0f, 20.0f)]
    public float gravityStrenght = 5.0f;

    private GameObject satellite;
    private GameObject waveIn;
    private GameObject waveOut;
    private PathCreator pathCreator;
    private LineRenderer lineRenderer;

    private float pathLenght;

    void Start()
    {
        satellite = transform.Find("Satellite").gameObject;
        waveIn = transform.Find("WaveIn").gameObject;
        waveOut = transform.Find("WaveOut").gameObject;
        pathCreator = GetComponentInChildren<PathCreator>();
        lineRenderer = GetComponentInChildren<LineRenderer>();

        if(lineRenderer != null)
        {
            lineRenderer.sortingOrder = -10;
            lineRenderer.SetPositions(pathCreator.path.localPoints);
        }

        if(pathCreator != null)
        {
            pathLenght = pathCreator.path.length;
            satellite.transform.position = pathCreator.path.GetPointAtDistance(pathLenght / 4, EndOfPathInstruction.Stop);
        }
    }

    private void FixedUpdate()
    {
        GetComponent<CircleCollider2D>().offset = new Vector2(satellite.transform.localPosition.x, 0);
        waveIn.transform.position = satellite.transform.position;
        waveOut.transform.position = satellite.transform.position;

        if(negativePolarity)
        {
            waveOut.SetActive(false);
            waveIn.SetActive(true);
        }
        else
        {
            waveOut.SetActive(true); 
            waveIn.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
            negativePolarity = !negativePolarity;

        if(Input.GetMouseButton(1))
        {
            
        }
    }


    void Update()
    {
        
    }
}