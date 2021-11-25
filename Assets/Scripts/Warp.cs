using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warp : MonoBehaviour
{
    public GameObject otherTp;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 newLocation = otherTp.transform.position;
        if (otherTp.transform.position.z > 0)
        {
            newLocation.z--;
        }
        else
        {
            newLocation.z++;
        }
        newLocation.y = 1f;
        if (other.gameObject.name != "PacMan")
        {
            other.gameObject.GetComponent<NavMeshAgent>().Warp(newLocation);
        }
        else
        {
            other.gameObject.transform.position = newLocation;
        }
    }
}
