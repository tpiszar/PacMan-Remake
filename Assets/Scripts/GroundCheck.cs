using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            isGrounded = true;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
