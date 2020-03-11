using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class AttractionPoint : MonoBehaviour
{
    public bool positivePolarity = true;
    [Range(0.0f, 20.0f)]
    public float gravityStrenght = 5.0f;

    private GameObject satellite;
    private PathCreator pathCreator;
    private LineRenderer lineRenderer;

    private float pathLenght;

    void Start()
    {
        satellite = transform.Find("Satellite").gameObject;
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
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
            positivePolarity = !positivePolarity;

        if(Input.GetMouseButton(1))
        {
            
        }
    }


    void Update()
    {
        
    }
}