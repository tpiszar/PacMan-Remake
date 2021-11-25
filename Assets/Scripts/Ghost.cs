using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//https://gameinternals.com/understanding-pac-man-ghost-behavior
//https://stackoverflow.com/questions/32306704/how-to-pass-data-and-references-between-scenes-in-unity
//https://strategywiki.org/wiki/Pac-Man/Gameplay

public class Ghost : MonoBehaviour
{
    public GameObject player;
    public GameObject manager;
    public GameObject Tpr;
    public GameObject Tpl;
    public NavMeshAgent ghost;
    public Material color;
    public Material blue;
    public Material white;
    public int type;
    public float speed = 6;
    public Vector3 curPlayerPos;
    public Vector3 targetPos;
    public bool stall = false;
    public bool home = false;
    public bool scatter = true;
    public float scatterTime;
    public bool powerPellet = false;
    public float powerTime;

    // Update is called once per frame
    void Update()
    {
        if (scatter && Time.time > scatterTime)
        {
            scatter = false;
        }
        if (powerPellet && Time.time > powerTime)
        {
            powerPellet = false;
            ghost.speed = speed;
            GetComponent<MeshRenderer>().material = color;
        }
        else if (powerPellet)
        {
            if (aboutEquals(powerTime - Time.time, 4f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = white;
            }
            else if (aboutEquals(powerTime - Time.time, 3.75f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = blue;
            }
            else if (aboutEquals(powerTime - Time.time, 3.5f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = white;
            }
            else if (aboutEquals(powerTime - Time.time, 3.25f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = blue;
            }
            else if (aboutEquals(powerTime - Time.time, 3f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = white;
            }
            else if (aboutEquals(powerTime - Time.time, 2.75f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = blue;
            }
            else if (aboutEquals(powerTime - Time.time, 2.5f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = white;
            }
            else if (aboutEquals(powerTime - Time.time, 2.25f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = blue;
            }
            else if (aboutEquals(powerTime - Time.time, 2f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = white;
            }
            else if (aboutEquals(powerTime - Time.time, 1.75f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = blue;
            }
            else if (aboutEquals(powerTime - Time.time, 1.5f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = white;
            }
            else if (aboutEquals(powerTime - Time.time, 1.25f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = blue;
            }
            else if (aboutEquals(powerTime - Time.time, 1f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = white;
            }
            else if (aboutEquals(powerTime - Time.time, .75f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = blue;
            }
            else if (aboutEquals(powerTime - Time.time, .5f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = white;
            }
            else if (aboutEquals(powerTime - Time.time, .25f, 0.1f))
            {
                GetComponent<MeshRenderer>().material = blue;
            }
        }
        if (home == true && aboutEquals(this.transform.position, new Vector3(2, 1, 0), .1f))
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            GetComponent<MeshRenderer>().material = color;
            ghost.speed = speed;
            scatter = false;
            powerPellet = false;
            home = false;
        }
        
        if (home)
        {
            ghost.speed = speed + 4;
            targetPos = new Vector3(2, 1, 0);
        }
        else if (stall)
        {
            if (targetPos == curPlayerPos || aboutEquals(this.transform.position, new Vector3(0.5f, 1f, -2f), .1f))
            {
                targetPos = new Vector3(0.5f, 1f, 2f);
            }
            else if (aboutEquals(this.transform.position, new Vector3(0.5f, 1f, 2f), .1f))
            {
                targetPos = new Vector3(0.5f, 1f, -2f);
            }
        }
        else if (scatter)
        {
            float accuracy = .1f;
            Vector3 pos1 = new Vector3();
            Vector3 pos2 = new Vector3();
            Vector3 pos3 = new Vector3();
            Vector3 pos4 = new Vector3();

            if (type == 1)
            {
                pos1 = new Vector3(9.5f, 1f, -7.5f);
                pos2 = new Vector3(13.5f, 1f, -7.5f);
                pos3 = new Vector3(13.5f, 1f, -12.5f);
                pos4 = new Vector3(9.5f, 1f, -12.5f);
            }
            else if (type == 2)
            {
                pos1 = new Vector3(9.5f, 1f, 7.5f);
                pos2 = new Vector3(13.5f, 1f, 7.5f);
                pos3 = new Vector3(13.5f, 1f, 12.5f);
                pos4 = new Vector3(9.5f, 1f, 12.5f);
            }
            else if (type == 3)
            {
                pos1 = new Vector3(-8.5f, 1f, -6f);
                pos2 = new Vector3(-14.5f, 1f, -12.5f);
                pos3 = new Vector3(-14.5f, 1f, -1.5f);
                pos4 = pos1;
            }
            else if (type == 4)
            {
                pos1 = new Vector3(-8.5f, 1f, 6f);
                pos2 = new Vector3(-14.5f, 1f, 12.5f);
                pos3 = new Vector3(-14.5f, 1f, 1.5f);
                pos4 = pos1;
            }

            if (aboutEquals(this.transform.position, pos1, accuracy))
            {
                targetPos = pos2;
            }
            else if (aboutEquals(this.transform.position, pos2, accuracy))
            {
                targetPos = pos3;
            }
            else if (aboutEquals(this.transform.position, pos3, accuracy))
            {
                targetPos = pos4;
            }
            else if (!aboutEquals(ghost.destination, pos2, accuracy) && 
                !aboutEquals(ghost.destination, pos3, accuracy) && !aboutEquals(ghost.destination, pos2, accuracy))
            {
                targetPos = pos1;
            }
        }
        else
        {
            curPlayerPos = player.transform.position;
            targetPos = curPlayerPos;

            if (type == 2)
            {
                float xDist = Mathf.Abs(this.transform.position.x - player.transform.position.x);
                float zDist = Mathf.Abs(this.transform.position.z - player.transform.position.z);
                float dist = Mathf.Sqrt(Mathf.Pow(xDist, 2) + Mathf.Pow(zDist, 2));

                int direction = player.GetComponent<Movement>().direction;
                if (direction == 1 && dist > 5)
                {
                    targetPos.x -= 4;
                }
                else if (direction == 2 && dist > 5)
                {
                    targetPos.z += 4;
                }
                else if (direction == 3 && dist > 5)
                {
                    targetPos.x += 4;
                }
                if (direction == 4 && dist > 5)
                {
                    targetPos.z -= 4;
                }
            }
            else if (type == 3)
            {
                float xDist = Mathf.Abs(this.transform.position.x - player.transform.position.x);
                float zDist = Mathf.Abs(this.transform.position.z - player.transform.position.z);
                float dist = Mathf.Sqrt(Mathf.Pow(xDist, 2) + Mathf.Pow(zDist, 2));

                int direction = player.GetComponent<Movement>().direction;
                if (direction == 1 && dist > 5)
                {
                    targetPos.x += 4;
                }
                else if (direction == 2 && dist > 5)
                {
                    targetPos.z -= 4;
                }
                else if (direction == 3 && dist > 5)
                {
                    targetPos.x -= 4;
                }
                if (direction == 4 && dist > 5)
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
                    scatterTime = Time.time + 3;
                }
            }

            NavMeshPath toTpr = new NavMeshPath();
            NavMesh.CalculatePath(this.transform.position, Tpr.transform.position, NavMesh.AllAreas, toTpr);
            NavMeshPath toTpl = new NavMeshPath();
            NavMesh.CalculatePath(this.transform.position, Tpl.transform.position, NavMesh.AllAreas, toTpl);
            NavMeshPath toTfR = new NavMeshPath();
            NavMesh.CalculatePath(Tpr.transform.position, targetPos, NavMesh.AllAreas, toTfR);
            NavMeshPath toTfL = new NavMeshPath();
            NavMesh.CalculatePath(Tpl.transform.position, targetPos, NavMesh.AllAreas, toTfL);
            NavMeshPath toTarget = new NavMeshPath();
            NavMesh.CalculatePath(this.transform.position, targetPos, NavMesh.AllAreas, toTarget);

            float TprDist = Distance(toTpr.corners) + Distance(toTfL.corners);
            float TplDist = Distance(toTpl.corners) + Distance(toTfR.corners);
            float regDist = Distance(toTarget.corners);

            if (TprDist < regDist && TprDist < TplDist)
            {
                targetPos = Tpr.transform.position;
            }
            else if (TplDist < regDist && TplDist < TprDist)
            {
                targetPos = Tpl.transform.position;
            }
        }
        ghost.SetDestination(targetPos);

    }

    public float Distance(Vector3[] points)
    {
        if (points.Length < 2)
        {
            return 0;
        }
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
        {
            distance += Vector3.Distance(points[i], points[i + 1]);
        }
        return distance;
    }

    bool aboutEquals(Vector3 a, Vector3 b, float accuracy)
    {
        if (Mathf.Abs(a.x - b.x) < accuracy && Mathf.Abs(a.z - b.z) < accuracy)
        {
            return true;
        }
        return false;
    }

    bool aboutEquals(float a, float b, float accuracy)
    {
        if (Mathf.Abs(a - b) < accuracy)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PacMan")
        {
            if (powerPellet)
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                manager.GetComponent<Manager>().killedGhost();
                home = true;
            }
            else
            {
                if (!Manager.dead)
                {
                    Manager.dead = true;
                }

            }
        }
    }
}
