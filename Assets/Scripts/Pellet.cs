using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PacMan")
        {
            Manager.score += 10;
            Destroy(this.gameObject);
        }
        
    }
}
