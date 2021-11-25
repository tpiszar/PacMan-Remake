using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PacMan")
        {
            Manager.score += 50;
            GetComponentInParent<Manager>().powerPellet = true;
            Destroy(this.gameObject);
        }
    }
}
