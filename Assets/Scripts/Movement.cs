using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject groundCheck;
    private GroundCheck checker;
    public float speed;
    public Vector3 velocity;
    public float jumpHeight = 3.0f;
    public float gravity = -9.81f;
    public int direction = 4;
    private int quedDir = 1;
    private bool qued = false;

    void Start()
    {
        checker = groundCheck.GetComponent<GroundCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool up = !Physics.Raycast(new Vector3(this.transform.position.x, 1f, this.transform.position.z + .75f), Vector3.right, out hit, 1.5f)
            && !Physics.Raycast(new Vector3(this.transform.position.x, 1f, this.transform.position.z - .75f), Vector3.right, out hit, 1.5f)
            && !Physics.Raycast(this.transform.position, Vector3.right, out hit, 1.5f);
        bool down = !Physics.Raycast(new Vector3(this.transform.position.x, 1f, this.transform.position.z + .75f), Vector3.left, out hit, 1.5f)
            && !Physics.Raycast(new Vector3(this.transform.position.x, 1f, this.transform.position.z - .75f), Vector3.left, out hit, 1.5f)
            && !Physics.Raycast(this.transform.position, Vector3.left, out hit, 1.5f);
        bool right = !Physics.Raycast(new Vector3(this.transform.position.x + .75f, 1f, this.transform.position.z), Vector3.back, out hit, 1.5f)
            && !Physics.Raycast(new Vector3(this.transform.position.x - .75f, 1f, this.transform.position.z), Vector3.back, out hit, 1.5f)
            && !Physics.Raycast(this.transform.position, Vector3.back, out hit, 1.5f);
        bool left = !Physics.Raycast(new Vector3(this.transform.position.x + .75f, 1f, this.transform.position.z), Vector3.forward, out hit, 1.5f)
            && !Physics.Raycast(new Vector3(this.transform.position.x - .75f, 1f, this.transform.position.z), Vector3.forward, out hit, 1.5f)
            && !Physics.Raycast(this.transform.position, Vector3.forward, out hit, 1.5f);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (up || !checker.isGrounded)
            {
                direction = 1;
                qued = false;
            }
            else
            {
                quedDir = 1;
                qued = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (right || !checker.isGrounded)
            {
                direction = 2;
                qued = false;
            }
            else
            {
                quedDir = 2;
                qued = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (down || !checker.isGrounded)
            {
                direction = 3;
                qued = false;
            }
            else
            {
                quedDir = 3;
                qued = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (left || !checker.isGrounded)
            {
                direction = 4;
                qued = false;
            }
            else
            {
                quedDir = 4;
                qued = true;
            }
        }
        else if (qued)
        {
            if (quedDir == 1 && (up || !checker.isGrounded))
            {
                direction = 1;
                qued = false;
            }
            else if (quedDir == 2 && (right || !checker.isGrounded))
            {
                direction = 2;
                qued = false;
            }
            else if (quedDir == 3 && (down || !checker.isGrounded))
            {
                direction = 3;
                qued = false;
            }
            else if (quedDir == 4 && (left || !checker.isGrounded))
            {
                direction = 4;
                qued = false;
            }
        }

        Vector3 movement = new Vector3(0, 0, 0);
        if (direction == 1)
        {
            movement.x = speed;
        }
        else if (direction == 2)
        {
            movement.z = speed * -1;
        }
        else if (direction == 3)
        {
            movement.x = speed * -1;
        }
        else if (direction == 4)
        {
            movement.z = speed;
        }

        movement = movement.normalized * speed * Time.deltaTime;

        controller.Move(movement);

        if (checker.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if (velocity.y > 0)
        {
            checker.isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && checker.isGrounded && Manager.jumps > 0)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            Manager.jumps--;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
