using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    //public Tween activeTween;
    private List<Tween> activeTweens = new List<Tween>();
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < activeTweens.Count; i++)
        {
            Tween activeTween = activeTweens[i];
            // Calculate distance and time
            float totalDistance = Vector3.Distance(activeTween.StartPos, activeTween.EndPos);
            float elapsedTime = Time.time - activeTween.StartTime;
            float fractionOfJourney = elapsedTime / activeTween.Duration;
            float currentDistance = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);
            if (currentDistance > 0.1f)
            {
                // Update position
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, fractionOfJourney);
            }
            else
            {
                // Finish tween
                activeTween.Target.position = activeTween.EndPos;
                activeTweens.RemoveAt(i);
            }
        }
    }

    public bool TweenExists(Transform target)
    {
        foreach (Tween tween in activeTweens)
        {
            if (tween.Target == target)
            {
                return true;
            }
        }
        return false;
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (!TweenExists(targetObject))
        {
            Tween newTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTweens.Add(newTween);
            return true;
        }
        else
        {
            return false;
        }
    }
}
