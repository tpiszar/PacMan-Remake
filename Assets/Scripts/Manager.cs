using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class Manager : MonoBehaviour
{
    public static int score = 0;
    public static bool dead = false;
    public static int lives = 3;
    public static int jumps = 5;

    public float startTime;
    public bool starting;
    public float nextLife;
    public bool powerPellet;
    public int deadGhosts = 0;
    public GameObject player;
    public GameObject Blinky;
    public Ghost Bg;
    public GameObject Pinky;
    public Ghost Pg;
    public GameObject Inky;
    public Ghost Ig;
    public GameObject Clyde;
    public Ghost Cg;
    public Material Blue;

    public GameObject newGame;
    public GameObject quit;
    public Canvas canvas;
    public Text scoreTxt;
    public Text livesTxt;
    public Text jumpsTxt;
    public Text countDown;

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = "Score: " + score.ToString();
        jumpsTxt.text = "Remaining Jumps: " + jumps.ToString();
        livesTxt.text = "Lives: " + lives.ToString();

        Time.timeScale = 1.0f;
        canvas.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        newGame.SetActive(false);
        quit.SetActive(false);
        nextLife = score + 10000;
        startTime = Time.time;
        starting = true;

        Bg = Blinky.GetComponent<Ghost>();
        Pg = Pinky.GetComponent<Ghost>();
        Ig = Inky.GetComponent<Ghost>();
        Cg = Clyde.GetComponent<Ghost>();
        Bg.stall = false;
        Pg.stall = true;
        Ig.stall = true;
        Cg.stall = true;
        scatter(7);
    }

    // Update is called once per frame
    void Update()
    {
        if (starting)
        {
            startUp();
        }
        else
        {
            scoreTxt.text = "Score: " + score.ToString();
            jumpsTxt.text = "Remaining Jumps: " + jumps.ToString();

            if (aboutEquals(Time.time - startTime, 3.01f, 0.5f))
            {
                Pg.stall = false;
            }
            if (transform.childCount <= 210)
            {
                Ig.stall = false;
            }
            if (transform.childCount <= 160)
            {
                Cg.stall = false;
            }
            if (aboutEquals(Time.time - startTime, 27, 0.5f))
            {
                scatter(7);
            }
            if (aboutEquals(Time.time - startTime, 47, 0.5f))
            {
                scatter(5);
            }
            if (aboutEquals(Time.time - startTime, 74, 0.5f))
            {
                scatter(5);
            }
            if (powerPellet)
            {
                deadGhosts = 0;
                scatter(8);
                Bg.powerPellet = true;
                Bg.powerTime = Time.time + 8;
                Bg.ghost.speed = Bg.speed - 2;
                Bg.GetComponent<MeshRenderer>().material = Blue;
                Pg.powerPellet = true;
                Pg.powerTime = Time.time + 8;
                Pg.ghost.speed = Pg.speed - 2;
                Pg.GetComponent<MeshRenderer>().material = Blue;
                Ig.powerPellet = true;
                Ig.powerTime = Time.time + 8;
                Ig.ghost.speed = Ig.speed - 2;
                Ig.GetComponent<MeshRenderer>().material = Blue;
                Cg.powerPellet = true;
                Cg.powerTime = Time.time + 8;
                Cg.ghost.speed = Cg.speed - 2;
                Cg.GetComponent<MeshRenderer>().material = Blue;
                powerPellet = false;
            }
            if (nextLife <= score)
            {
                lives++;
                nextLife += 10000;
            }
            if (dead)
            {
                if (lives == 1)
                {
                    lives--;
                    livesTxt.text = "Lives: " + lives.ToString();
                    newGame.SetActive(true);
                    quit.SetActive(true);
                    canvas.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0.34509803921f);
                    Time.timeScale = 0.0f;
                }
                else if (lives > 1)
                {
                    lives--;
                    livesTxt.text = "Lives: " + lives.ToString();
                    Bg.ghost.Warp(new Vector3(3.5f, 1, 0));
                    Pg.ghost.Warp(new Vector3(.5f, 1, 0));
                    Ig.ghost.Warp(new Vector3(.5f, 1, 2));
                    Cg.ghost.Warp(new Vector3(.5f, 1, -2));
                    player.transform.position = new Vector3(-8.5f, 1, 0);
                    player.GetComponent<Movement>().direction = 4;
                    Start();
                    Bg.powerPellet = false;
                    Bg.ghost.speed = Bg.speed;
                    Bg.GetComponent<MeshRenderer>().material = Bg.color;
                    Pg.powerPellet = false;
                    Pg.ghost.speed = Pg.speed;
                    Pg.GetComponent<MeshRenderer>().material = Pg.color;
                    Ig.powerPellet = false;
                    Ig.ghost.speed = Ig.speed;
                    Ig.GetComponent<MeshRenderer>().material = Ig.color;
                    Cg.powerPellet = false;
                    Cg.ghost.speed = Cg.speed;
                    Cg.GetComponent<MeshRenderer>().material = Cg.color;
                }
            }
            if (transform.childCount == 0)
            {
                SceneManager.LoadScene("Level");
            }
        }
    }

    void scatter(int scatterTime)
    {
        Bg.scatter = true;
        Bg.scatterTime = Time.time + scatterTime;
        Pg.scatter = true;
        Pg.scatterTime = Time.time + scatterTime;
        Ig.scatter = true;
        Ig.scatterTime = Time.time + scatterTime;
        Cg.scatter = true;
        Cg.scatterTime = Time.time + scatterTime;
    }

    bool aboutEquals(float a, float b, float accuracy)
    {
        if (Mathf.Abs(a - b) < accuracy)
        {
            return true;
        }
        return false;
    }

    public void killedGhost()
    {
        deadGhosts += 1;
        if (deadGhosts == 1)
        {
            score += 200;
        }
        else if (deadGhosts == 2)
        {
            score += 400;
        }
        else if (deadGhosts == 3)
        {
            score += 800;
        }
        else if (deadGhosts == 4)
        {
            score += 1600;
        }
    }

    public void startUp()
    {
        if (Time.time - startTime < 1)
        {
            player.GetComponent<Movement>().enabled = false;
            Bg.enabled = false;
            Pg.enabled = false;
            Ig.enabled = false;
            Cg.enabled = false;
            Blinky.GetComponent<NavMeshAgent>().enabled = false;
            Pinky.GetComponent<NavMeshAgent>().enabled = false;
            Inky.GetComponent<NavMeshAgent>().enabled = false;
            Clyde.GetComponent<NavMeshAgent>().enabled = false;
            countDown.text = "3";
        }
        else if (Time.time - startTime < 2)
        {
            countDown.text = "2";
        }
        else if (Time.time - startTime < 3)
        {
            countDown.text = "1";
        }
        else
        {
            starting = false;
            dead = false;
            countDown.text = "";
            startTime = Time.time;
            player.GetComponent<Movement>().enabled = true;
            Bg.enabled = true;
            Pg.enabled = true;
            Ig.enabled = true;
            Cg.enabled = true;
            Blinky.GetComponent<NavMeshAgent>().enabled = true;
            Pinky.GetComponent<NavMeshAgent>().enabled = true;
            Inky.GetComponent<NavMeshAgent>().enabled = true;
            Clyde.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
