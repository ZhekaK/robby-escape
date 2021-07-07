using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Monetization;


public class player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float speed;
    public float jumpforce;
    float codespeed;
    bool isGrounded;
    bool death;
    public Transform GroundCheck;
    public float checkRadius;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsGround2;
    public int health;
    public int numberOfLives;
    public Image[] lives;
    public Sprite fullLive;
    public Sprite emptyLive;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject death1;
    public GameObject death2;
    public GameObject finish0;
    public static player instance = null;
    int sceneIndex;
    int levelComplite;
    public int key;
    public int allkeys;
    public Image[] keys;
    public Sprite keyi;
    public Sprite noKey;
    public GameObject failpan;
    public AudioSource jumpaud;
    public AudioSource tpaud;
    public AudioSource damageaud;
    public AudioSource heartaud;
    public AudioSource keyaud;
    public float failcount;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplite = PlayerPrefs.GetInt("LevelComplite");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (Monetization.isSupported) Monetization.Initialize("3590619", true);
        failcount = PlayerPrefs.GetFloat("Failcount");
    }

    public void isEndGame()
    {
        if(sceneIndex == 4)
        {
            finish0.SetActive(true);
        }
        else
        {
            if (levelComplite < sceneIndex)
                PlayerPrefs.SetInt("LevelComplite", sceneIndex);
            Invoke("NextLevel", 1f);
        }
    }
    void NextLevel()
    {
        finish0.SetActive(true);
        codespeed = 0;
    }

    public void Nextbutton()
    {
        PlayerPrefs.SetFloat("Failcount", failcount);
        SceneManager.LoadScene(sceneIndex + 1);
        finish0.SetActive(false);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("menu");
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(sceneIndex);
        PlayerPrefs.SetFloat("Failcount", failcount);
    }

    void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.tag == "enemy")
        {
            health--;
            damageaud.Play();
        }
    }

    public void RestartButton()
    {
        Invoke("ReloadLevel", 0.1f);
        finish0.SetActive(false);
        failcount++;
    }

    void Update()
    {
        if (failcount == 4)
        {
            failcount = 0;
        }
        if(death == true)
        {
            death2.SetActive(true);
            codespeed = 0;
            anim.SetInteger("mati", 1);
            health = -1;
            damageaud.Play();
        }
        if(health == 0)
        {
            death1.SetActive(true);
            codespeed = 0;
            anim.SetInteger("mati", 1);
        }
        if(health > numberOfLives)
        {
            health = numberOfLives;
        }
        for (int i = 0; i < lives.Length; i++)
        {
            if(i < health)
            {
                lives[i].sprite = fullLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }
            if(i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
        for (int c = 0; c < keys.Length; c++)
        {
            if (c < key)
            {
                keys[c].sprite = keyi;
            }
            else
            {
                keys[c].sprite = noKey;
            }
            if (c < allkeys)
            {
                keys[c].enabled = true;
            }
            else
            {
                keys[c].enabled = false;
            }
        }
        if (Input.GetAxis ("Horizontal") != 0)
        {
            Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D heart)
    {
        if (heart.gameObject.tag == "heart")
        {
            heartaud.Play();
            health++;
            Destroy(heart.gameObject);
        }
        if(heart.gameObject.tag == "Finish")
        {
            if (key == 3)
            {
                tpaud.Play();
                isEndGame();
            }
            else
            {
                Invoke("failpann", 1f);
            }
        }
        if (heart.gameObject.tag == "destroy")
        {
            Destroy(heart.gameObject);
        }
        if (heart.gameObject.tag == "key")
        {
            keyaud.Play();
            key++;
            Destroy(heart.gameObject);
        }
    }

    void failpann()
    {
        failpan.SetActive(true);
        codespeed = 0;
    }


    public void RightButton()
    {
        codespeed = speed;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        anim.SetInteger("mati", 2);
    }

    public void LeftButton()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        anim.SetInteger("mati", 2);
        codespeed = speed;
    }
    
    public void stop()
    {
        codespeed = 0;
        anim.SetInteger("mati", 1);
    }
    
    public void jumpbutton()
    {
        jump();
    }

    public void jumpbuttonD()
    {

    }

    public void exit()
    {
        if(GameIsPaused == false)
        {
            pause();
        }
    }

    public void play()
    {
        resume();
    }

    public void menu()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        Application.LoadLevel("menu");
        finish0.SetActive(false);
    }

    public void exitgame()
    {
        Application.Quit();
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void FixedUpdate()
    {
        death = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, WhatIsGround2);
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, WhatIsGround);
        transform.Translate(codespeed, 0, 0);
    }
    void jump()
    {
        if (isGrounded == true)
        {
            jumpaud.Play();
            rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
        }
    }
    void Flip()
    {
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public void continuebutton()
    {
        failpan.SetActive(false);
    }
    public void rewardHealth()
    {
        if (Monetization.IsReady("rewardedVideo"))
        {
            ShowAdCallbacks options = new ShowAdCallbacks();
            options.finishCallback = HandleShowResult;
            ShowAdPlacementContent ad = Monetization.GetPlacementContent("rewardedVideo") as ShowAdPlacementContent;
            ad.Show(options);
        }
    }
    void death1active()
    {
        death1.SetActive(false);
    }
    void losevideo()
    {
        if (Monetization.IsReady("video"))
        {
            ShowAdCallbacks options = new ShowAdCallbacks();
            options.finishCallback = HandleShowResult2;
            ShowAdPlacementContent ad = Monetization.GetPlacementContent("video") as ShowAdPlacementContent;
            ad.Show(options);
        }
        failcount = 0;
    }
    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            death1active();
            health++;
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.Log("skipped");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.Log("failed");
        }
    }
    void HandleShowResult2(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("finished");
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.Log("skipped");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.Log("failed");
        }
    }
}
