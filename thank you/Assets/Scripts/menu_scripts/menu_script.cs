using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_script : MonoBehaviour
{
    private runner player = null;
    private GameObject enemy = null;

    public GameObject pauseMenuUI = null;

    public GameObject lvlUpMenu = null;

    public GameObject Interface = null;

    public GameObject joystick;
    public GameObject joybutton;



    public bool menuPer = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<runner>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<runner>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        interfaceOff();


        if (enemy == null)
        {
            Invoke("lvlUpMenuStart", 1f);
        }

        if (player == null)
        {
            pause();
            if (pauseMenuUI == null)
            {
                Debug.Log("it's menu");
            }
        }


        

        else
        {
            Time.timeScale = 1f;
        }

        
    }

    public void pause()
    {
        Time.timeScale = 0f;

        joystick.SetActive(false);
        joybutton.SetActive(false);

        pauseMenuUI.SetActive(true);
    }

    void interfaceOff()
    {

        if (player != null)
        {
            Interface.SetActive(true);
        }

        else if (player== null)
        {
            Interface.SetActive(false);
        }
    }


    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void playGame()
    {
        SceneManager.LoadScene(0);
    }


    public void lvlUpMenuStart()
    {
        Time.timeScale = 0f;

        lvlUpMenu.SetActive(true);

        joystick.SetActive(false);
        joybutton.SetActive(false);
    }

    public void hpUp()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<runner>();
        player.maxHP += 2;
        lvlUpMenu.SetActive(false);

        joystick.SetActive(true);
        joybutton.SetActive(true);

        Time.timeScale = 1f;
        menuPer = false;
    }

    public void powerUp()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<runner>();
        player.damage += 2;
        lvlUpMenu.SetActive(false);

        joystick.SetActive(true);
        joybutton.SetActive(true);

        Time.timeScale = 1f;
        menuPer = false;
    }
}
