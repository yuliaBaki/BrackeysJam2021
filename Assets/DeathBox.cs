using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var p = other.GetComponent<PlayerMovement>();
        if (p)
        {
            p.Death();
        }
    }
}
