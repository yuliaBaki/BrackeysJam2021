using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    private Queue<Vector3> positions;
    
    private void FixedUpdate()
    {
        if (positions != null) {
            transform.position = positions.Dequeue();
            if (positions.Count == 0) gameObject.SetActive(false);
        } 

    }
    public void SetPositions(Queue<Vector3> p)
    {
        positions = p;
    }
}
