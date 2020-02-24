using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SolarGenerator : MonoBehaviour
{
    public int pathLength;
    public float spaceBetweenPoints;

    [Header("Testing")]
    public GameObject objectToMove;
    public EndOfPathInstruction end;
    public float speed;
    private float dstTravelled;

    private PathCreator pathCreator;
    private PathSpace pathSpace;
    private Vector2[] pathPoints;


    void Start()
    {   
        pathPoints = new Vector2[pathLength];
        pathCreator = GetComponent<PathCreator>();
        pathCreator.bezierPath = GeneratePath();
    }

   void Update()
   {
       dstTravelled += speed * Time.deltaTime;
       objectToMove.transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
       objectToMove.transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);
   }

    BezierPath GeneratePath()
    {
        for (int i = 0; i < pathLength; i++)
        {
            pathPoints[i] = new Vector2(- (i * spaceBetweenPoints), Mathf.Sin(i) * 2);
            Debug.Log(pathPoints[i]);
        }
        BezierPath path = new BezierPath(pathPoints, false, PathSpace.xy);
        return path;
    }
}
