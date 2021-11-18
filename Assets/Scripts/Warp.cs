using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        other.gameObject.transform.position = newLocation;
    }
}
