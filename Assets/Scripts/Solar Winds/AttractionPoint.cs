using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class AttractionPoint : MonoBehaviour
{
    public bool negativePolarity = true;
    [Range(0.0f, 20.0f)]
    public float gravityStrenght = 5.0f;
    
    [HideInInspector] public GameObject satellite;
    private GameObject waveIn;
    private GameObject waveOut;
    private PathCreator pathCreator;
    private LineRenderer lineRenderer;

    private float pathLenght, currentPathPos;
    private Vector3 prevMousePos, currentMousePos;
    private bool dragging = false;

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
            Vector3[] points = pathCreator.path.localPoints;

            float angle = transform.eulerAngles.z;
            if(angle > 180)
                angle -= 360;
            if(angle % 360 > 90 || angle % 360 < -90)
            {
                Array.Reverse(points);
            }

            Debug.Log(angle);
            
            lineRenderer.SetPositions(points);

        }

        if(pathCreator != null)
        {
            pathLenght = pathCreator.path.length;
            currentPathPos = pathLenght / 2;
        }
    }

    private void FixedUpdate()
    {
        Vector3 point = pathCreator.path.GetPointAtDistance(currentPathPos, EndOfPathInstruction.Stop);
        satellite.transform.position = new Vector3(point.x, point.y, 0);

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

        if(dragging)
        {
            currentMousePos = Input.mousePosition;
            if(currentMousePos.x > prevMousePos.x && currentPathPos < pathLenght)
            {
                currentPathPos += Vector3.Distance(Camera.main.ScreenToWorldPoint(currentMousePos), Camera.main.ScreenToWorldPoint(prevMousePos));
                prevMousePos = currentMousePos;
            }
            if(currentMousePos.x < prevMousePos.x && currentPathPos > 0)
            {
                currentPathPos -= Vector3.Distance(Camera.main.ScreenToWorldPoint(currentMousePos), Camera.main.ScreenToWorldPoint(prevMousePos));
                prevMousePos = currentMousePos;
            }
        }
    }

    private void Update() {
        if(Input.GetMouseButtonUp(0))
            dragging = false;
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
            negativePolarity = !negativePolarity;

        if(Input.GetMouseButtonDown(0))
        {
            prevMousePos = currentMousePos = Input.mousePosition;
            dragging = true;
        }
    }
}