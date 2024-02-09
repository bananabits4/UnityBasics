using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Interpolator : MonoBehaviour
{
    [SerializeField] private float timeElapsed = 0f;
    [SerializeField] private float timeToReachTarget = 0.05f;
    [SerializeField] private float movementThreshold = 0.05f;
    private readonly List<Transformupdate> futureTransformUpdates = new List<Transformupdate>();
    private float squareMovementThreshold;
    private Transformupdate to;
    private Transformupdate from;
    private Transformupdate previous;
    // Start is called before the first frame update
    void Start()
    {
        squareMovementThreshold = movementThreshold * movementThreshold;
        to = new Transformupdate(NetworkManager.Singleton.ServerTick, false, transform.position);
        from = new Transformupdate(NetworkManager.Singleton.InterpolationTick, false, transform. position);
        previous = new Transformupdate(NetworkManager.Singleton.InterpolationTick, false, transform. position);
        
    }

    // Update is called once per frame
    void Update()
    {
       for (int i = 0; i < futureTransformUpdates.Count ;i++)
       {
            if (futureTransformUpdates[i].IsTeleport)
            {
                to = futureTransformUpdates[i];
                from = to;
                previous = to;
                transform.position = to.Position;
            }
            else
            {
                previous = to;
                to = futureTransformUpdates[i];
                from = new Transformupdate(NetworkManager.Singleton.InterpolationTick,false,transform.position);
            }
            futureTransformUpdates.RemoveAt(i);
            i--;
            timeElapsed = 0f;
            timeToReachTarget = (to.Tick - from.Tick)*Time.fixedDeltaTime;

       }

       timeElapsed = Time.deltaTime;
       Interpolateposition(timeElapsed/timeToReachTarget);

    }

    private void Interpolateposition(float learamount)
    {
        if ((to.Position - previous.Position).sqrMagnitude<squareMovementThreshold)
        {
            if (to.Position != from.Position)
            {
                transform.position = Vector3.Lerp(from.Position ,to.Position,learamount);

            }
            return;
        }
        transform.position = Vector3.LerpUnclamped(from.Position,to.Position,learamount);


    }

    public void NewUpdate(ushort tick,bool isTeleport , Vector3 position)
    {
        if (tick <= NetworkManager.Singleton.InterpolationTick && !isTeleport)
        return;
        for (int i = 0 ; i < futureTransformUpdates.Count;i++)  
        {
            if (tick<futureTransformUpdates[i].Tick)
            {
                futureTransformUpdates.Insert(i,new Transformupdate(tick,isTeleport,position));
                return;
            }
        }
        futureTransformUpdates.Add(new Transformupdate(tick,isTeleport,position));
    }
}
