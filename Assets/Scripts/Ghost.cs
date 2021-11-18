using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent ghost;
    public int type;
    public Vector3 curPlayerPos;
    public Vector3 targetPos;
    public bool scatter = true;
    public float scatterTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > scatterTime)
        {
            scatter = false;
        }
        
        if (scatter)
        {
            if (type == 1)
            {
                if (targetPos == curPlayerPos || aboutEquals(this.transform.position, new Vector3(9.5f, 1f, -12.5f), 0.001f))
                {
                    targetPos = new Vector3(9.5f, 1f, -7.5f);
                }
                else if(aboutEquals(this.transform.position, new Vector3(9.5f, 1f, -7.5f), 0.001f))
                {
                    targetPos = new Vector3(13.5f, 1f, -7.5f);
                }
                else if (aboutEquals(this.transform.position, new Vector3(13.5f, 1f, -7.5f), 0.001f))
                {
                    targetPos = new Vector3(13.5f, 1f, -12.5f);
                }
                else if (aboutEquals(this.transform.position, new Vector3(13.5f, 1f, -12.5f), 0.001f))
                {
                    targetPos = new Vector3(9.5f, 1f, -12.5f);
                }
            }
            else if (type == 2)
            {
                if (targetPos == curPlayerPos || aboutEquals(this.transform.position, new Vector3(9.5f, 1f, 12.5f), 0.001f))
                {
                    targetPos = new Vector3(9.5f, 1f, 7.5f);
                }
                else if (aboutEquals(this.transform.position, new Vector3(9.5f, 1f, 7.5f), 0.001f))
                {
                    targetPos = new Vector3(13.5f, 1f, 7.5f);
                }
                else if (aboutEquals(this.transform.position, new Vector3(13.5f, 1f, 7.5f), 0.001f))
                {
                    targetPos = new Vector3(13.5f, 1f, 12.5f);
                }
                else if (aboutEquals(this.transform.position, new Vector3(13.5f, 1f, 12.5f), 0.001f))
                {
                    targetPos = new Vector3(9.5f, 1f, 12.5f);
                }
            }
            else if (type == 3)
            {
                if (targetPos == curPlayerPos || aboutEquals(this.transform.position, new Vector3(-14.5f, 1f, -1.5f), 0.001f))
                {
                    targetPos = new Vector3(-8.5f, 1f, -6f);
                }
                else if (aboutEquals(this.transform.position, new Vector3(-8.5f, 1f, -6f), 0.001f))
                {
                    targetPos = new Vector3(-14.5f, 1f, -12.5f);
                }

                else if (aboutEquals(this.transform.position, new Vector3(-14.5f, 1f, -12.5f), 0.001f))
                {
                    targetPos = new Vector3(-14.5f, 1f, -1.5f);
                }
            }
            else if (type == 4)
            {
                if (targetPos == curPlayerPos || aboutEquals(this.transform.position, new Vector3(-14.5f, 1f, 1.5f), 0.001f))
                {
                    targetPos = new Vector3(-8.5f, 1f, 6f);
                }
                else if (aboutEquals(this.transform.position, new Vector3(-8.5f, 1f, 6f), 0.001f))
                {
                    targetPos = new Vector3(-14.5f, 1f, 12.5f);
                }

                else if (aboutEquals(this.transform.position, new Vector3(-14.5f, 1f, 12.5f), 0.001f))
                {
                    targetPos = new Vector3(-14.5f, 1f, 1.5f);
                }
            }
        }
        else
        {
            curPlayerPos = player.transform.position;
            targetPos = curPlayerPos;
            if (type == 2)
            {
                int direction = player.GetComponent<Movement>().direction;
                if (direction == 1)
                {
                    targetPos.x -= 4;
                }
                else if (direction == 2)
                {
                    targetPos.z += 4;
                }
                else if (direction == 3)
                {
                    targetPos.x += 4;
                }
                if (direction == 4)
                {
                    targetPos.z -= 4;
                }
            }
            else if (type == 3)
            {
                int direction = player.GetComponent<Movement>().direction;
                if (direction == 1)
                {
                    targetPos.x += 4;
                }
                else if (direction == 2)
                {
                    targetPos.z -= 4;
                }
                else if (direction == 3)
                {
                    targetPos.x -= 4;
                }
                if (direction == 4)
                {
                    targetPos.z += 4;
                }
            }
            else if (type == 4)
            {
                float xDist = Mathf.Abs(this.transform.position.x - curPlayerPos.x);
                float zDist = Mathf.Abs(this.transform.position.z - curPlayerPos.z);
                float dist = Mathf.Sqrt(Mathf.Pow(xDist, 2) + Mathf.Pow(zDist, 2));
                if (dist <= 8)
                {
                    scatter = true;
                    scatterTime = Time.time + 7;
                }
            }
        }
        ghost.SetDestination(targetPos);
    }

    bool aboutEquals(Vector3 a, Vector3 b, float accuracy)
    {
        if (Mathf.Abs(a.x - b.x) < accuracy && Mathf.Abs(a.z - b.z) < accuracy)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PacMan")
        {
            Destroy(other.gameObject);
            player = null;
        }
    }
}
