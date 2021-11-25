using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void LoadLevel(string name)
    {
        Manager.dead = false;
        Manager.score = 0;
        Manager.lives = 3;
        Manager.jumps = 5;
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}