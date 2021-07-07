using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    public Button lvl1;
    public Button lvl2;
    public Button lvl3;
    int levelComplite;
    public GameObject youSure;

    void Start()
    {
        levelComplite = PlayerPrefs.GetInt("LevelComplite");
        lvl1.interactable = false;
        lvl2.interactable = false;
        lvl3.interactable = false;
        switch (levelComplite)
        {
            case 1:
                lvl1.interactable = true;
                break;
            case 2:
                lvl1.interactable = true;
                lvl2.interactable = true;
                break;
            case 3:
                lvl1.interactable = true;
                lvl2.interactable = true;
                lvl3.interactable = true;
                break;
        }
    }

    public void resetbutton()
    {
        youSure.SetActive(true);
    }

    public void DA()
    {
        lvl1.interactable = false;
        lvl2.interactable = false;
        lvl3.interactable = false;
        PlayerPrefs.DeleteAll();
        youSure.SetActive(false);
    }

    public void Net()
    {
        youSure.SetActive(false);
    }

    public void LoadTo(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void exit()
    {
        Application.Quit();
    }
}
