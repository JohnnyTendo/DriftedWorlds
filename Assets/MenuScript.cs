using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    #region Singleton
    public static MenuScript instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public int activePageIndex;
    public int activeButtonIndex;
    public int activeOptionsIndex;
    public Font activeFont;
    public Font inactiveFont;

    public GameObject[] pages;
    public GameObject[] buttons;
    public GameObject[] options;
    public GameObject[] saveValueLabels;



    // Start is called before the first frame update
    void Start()
    {
        activePageIndex = 0;
        activeButtonIndex = 0;
        activeOptionsIndex = 0;
        UpdateItemCollection(activePageIndex, pages);
        UpdateTextEffects(activeButtonIndex, buttons);
        UpdateTextEffects(activeOptionsIndex, options);
    }

    // Update is called once per frame
    void Update()
    {
        switch (activePageIndex)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    activePageIndex++;
                    UpdateItemCollection(activePageIndex, pages);
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    activePageIndex++;
                    UpdateItemCollection(activePageIndex, pages);
                }
                break;
            case 2:
                NavigateMainMenu();
                break;
            case 3:
                NavigateOptionsMenu();
                break;

            default:
                break;
        }
    }

    private void NavigateMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            buttons[activeButtonIndex].GetComponent<Button>().Select();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (activeButtonIndex + 1 < buttons.Length)
            {
                activeButtonIndex = activeButtonIndex + 1;
                UpdateTextEffects(activeButtonIndex, buttons);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (activeButtonIndex - 1 >= 0)
            {
                activeButtonIndex = activeButtonIndex - 1;
                UpdateTextEffects(activeButtonIndex, buttons);
            }
        }
    }

    private void NavigateOptionsMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
        }
    }


    #region MenuButtons
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        activePageIndex = 2;
        UpdateItemCollection(activePageIndex, pages);
    }

    public void Options()
    {
        activePageIndex = 3;
        UpdateItemCollection(activePageIndex, pages);
    }

    public void Credits()
    {
        activePageIndex = 4;
        UpdateItemCollection(activePageIndex, pages);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Tools
    void UpdateTextEffects(int _index, GameObject[] _collection)
    {
        foreach (GameObject go in _collection)
        {
            Text goText = go.GetComponentInChildren<Text>();
            goText.font = inactiveFont;
        }
        GameObject activeTarget = _collection[_index];
        Text text = activeTarget.GetComponentInChildren<Text>();
        text.font = activeFont;
    }

    public void UpdateItemCollection(int _index, GameObject[] _collection)
    {
        GameObject activeTarget = _collection[_index];
        for (int i = 0; i < pages.Length; i++)
        {
            if (_collection[i] != activeTarget)
            {
                _collection[i].SetActive(false);
            }
            else
            {
                _collection[i].SetActive(true);
            }
        }
    }
    #endregion
}
