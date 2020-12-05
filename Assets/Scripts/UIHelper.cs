using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    public GameObject firstMenu;
    public GameObject secondMenu;
    public SaveData saveData;
    public Text coins;
    public GameObject helpWindow;
    public GameObject thirdMenu;
    public Text record;

    public AudioSource buttonSound;
    public GameObject soundOn;
    public GameObject soundOff;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject level6;
    public GameObject level7;
    public GameObject level8;
    public GameObject level9;


    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (AudioListener.volume == 0)
            {
                soundOn.SetActive(false);
                soundOff.SetActive(true);
            }

            else if (AudioListener.volume == 1)
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
            }
        }
    }
    
    void Start()
    {
        coins.text = saveData.coins.ToString();

        if (saveData.coins > 0 && saveData.currentLevel >= 0)
        {
            level1.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 5 && saveData.currentLevel >= 1)
        {
            level2.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 10 && saveData.currentLevel >= 1)
        {
            level3.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 15 && saveData.currentLevel >= 2)
        {
            level4.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 20 && saveData.currentLevel >= 3)
        {
            level5.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 25 && saveData.currentLevel >= 4)
        {
            level6.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 30 && saveData.currentLevel >= 5)
        {
            level7.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 35 && saveData.currentLevel >= 6)
        {
            level8.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 40 && saveData.currentLevel >= 7)
        {
            level9.GetComponent<Image>().color = Color.green;
        }
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenLevelsMenu()
    {
        buttonSound.Play();
        firstMenu.SetActive(false);
        secondMenu.SetActive(true);
    }

    public void OpenHelpWindow()
    {
        buttonSound.Play();
        helpWindow.SetActive(true);
        StartCoroutine(CloseHelpWindow());
    }

    public void OpenInfinityWindow()
    {
        buttonSound.Play();
        firstMenu.SetActive(false);
        thirdMenu.SetActive(true);
        record.text = saveData.record.ToString();
    }

    public void SoundOn()
    {
        soundOff.SetActive(false);
        soundOn.SetActive(true);
        AudioListener.volume = 1;
    }
    
    public void SoundOff()
    {
        soundOff.SetActive(true);
        soundOn.SetActive(false);
        AudioListener.volume = 0;
    }

    public void StartInfinityMode()
    {
        SceneManager.LoadScene("InfinityMode");
    }

    IEnumerator CloseHelpWindow()
    {
        yield return new WaitForSeconds(5.30f);
        helpWindow.SetActive(false);
    }

    public void SelectLevel(int number)
    {
        buttonSound.Play();
        switch (number)
        {
            case 1:
            {
                if (saveData.coins >= 0 && saveData.currentLevel >= 0)
                    saveData.level = 0;
                break;
            }
            case 2:
            {
                if (saveData.coins >= 5 && saveData.currentLevel >= 0)
                    saveData.level = 1;
                break;
            }
            case 3:
            {
                if (saveData.coins >= 10 && saveData.currentLevel >= 1)
                    saveData.level = 2;
                break;
            }
            case 4:
            {
                if (saveData.coins >= 15 && saveData.currentLevel >= 2)
                    saveData.level = 3;
                break;
            }
            case 5:
            {
                if (saveData.coins >= 20 && saveData.currentLevel >= 3)
                    saveData.level = 4;
                break;
            }
            case 6:
            {
                if (saveData.coins >= 25 && saveData.currentLevel >= 4)
                    saveData.level = 5;
                break;
            }
            case 7:
            {
                if (saveData.coins >= 30 && saveData.currentLevel >= 5)
                    saveData.level = 6;
                break;
            }
            case 8:
            {
                if (saveData.coins >= 35 && saveData.currentLevel >= 6)
                    saveData.level = 7;
                break;
            }
            case 9:
            {
                if (saveData.coins >= 40 && saveData.currentLevel >= 7)
                    saveData.level = 8;
                break;
            }
        }
    }
}
