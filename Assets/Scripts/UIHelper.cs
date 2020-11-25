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

    public AudioSource buttonSound;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject level6;
    
    
    void Start()
    {
        coins.text = saveData.coins.ToString();

        if (saveData.coins > 0 && saveData.currentLevel >= 0)
        {
            level1.GetComponent<Image>().color = Color.green;
        }
        
        if (saveData.coins > 5 && saveData.currentLevel >= 0)
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
        }
    }
}
