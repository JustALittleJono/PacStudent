using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public Transform Target {get; private set;}
    public Vector3 StartPos {get; private set;}
    public Vector3 EndPos {get; private set;}
    public float StartTime {get; private set;}
    public float Duration {get; private set;}

    public Tween(Transform t, Vector3 v1, Vector3 v2, float sT, float dur)
    {
        Target = t;
        StartPos = v1;
        EndPos = v2;
        StartTime = sT;
        Duration = dur;
    }
    
}
