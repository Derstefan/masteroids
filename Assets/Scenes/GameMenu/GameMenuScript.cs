using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMenuScript : MonoBehaviour
{
    private GameController gameController;
    private Label HighscoreUI;
    private VisualElement ProgressBar;
    private List<Button> buttons = new List<Button>(); 
    private int selectableSkills = 3;

    [Header("Events")]
    public GameEvent OnSkillSelected;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        HighscoreUI = root.Q<Label>("score");
        ProgressBar = root.Q<VisualElement>("Foreground");
        root.Q<Button>("menu").clicked += LoadMenu;
        SetUpLevelMenu(root);
        
    }

    private void SetUpLevelMenu(VisualElement root)
    {
        for(int i = 0; i < selectableSkills; i++)
        {
            Button button = new Button();
            button = root.Q<Button>("Skill_" + i.ToString());
            Debug.Log("initialized " + button.text);
            button.visible = false;
            buttons.Add(button);
        }

        foreach(Button button in buttons)
        {
            button.RegisterCallback<ClickEvent>(GetSkillFromButton);
            Debug.Log("Click event registered " + button.text);
        }        
    }

    private void GetSkillFromButton(ClickEvent evt)
    {
        Button temp = evt.currentTarget as Button;
        string skill = temp.text;
        Debug.Log("Learn skill cklicked " + skill);
        OnSkillSelected.Raise(this, skill);
        SetLevelMenuInactive();
    }

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void SetHighScore(Component sender, object data)
    {
        if(data is int)
        {
            HighscoreUI.text = $"SCORE: {(int) data}";
        }
    }

    public void SetProgress(Component sender, object data)
    {
        if(data is float)
        {
            ProgressBar.transform.scale = new Vector3((float) data, 1, 1);
        }
        
    }

    public void SetLevelMenuActive(Component sender, object data)
    {
        if(data is Skill[])
        {
            //Debug.Log("activates level menu");
            Skill[] wheapons = (Skill[])data;
            int i = 0;
            foreach(Button button in buttons)
            {
                if(i < wheapons.Length)
                {
                    button.text = wheapons[i].name;
                    buttons[i].visible = true;
                    i++;
                }
            }            
        }
    }

    public void SetLevelMenuInactive()
    {
        /*
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].visible = false;
        }
        */

        foreach(Button button in buttons)
        {
            button.visible = false;
        }
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
