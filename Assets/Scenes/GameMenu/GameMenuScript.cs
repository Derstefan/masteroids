using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMenuScript : MonoBehaviour
{
    //VisualElements
    private VisualElement HUD;
    private VisualElement levelMenu;
    private Label highscoreUI;
    private VisualElement progressBar;

    private List<Button> buttons = new List<Button>(); 

    [Header("Events")]
    public GameEvent OnSkillSelected;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        SetUpHUD(root);        
        SetUpLevelMenu(root);        
    }

    private void SetUpHUD(VisualElement root)
    {
        HUD = root.Q<VisualElement>("HUD");
        highscoreUI = root.Q<Label>("score");
        progressBar = root.Q<VisualElement>("Foreground");
        root.Q<Button>("menu").clicked += LoadMenu;
    }

    private void SetUpLevelMenu(VisualElement root)
    {
        levelMenu = root.Q<VisualElement>("LevelUp_Menu");

        foreach (VisualElement element in levelMenu.hierarchy.Children())
        {
            if(element is Button)
            {
                buttons.Add((Button)element);
            }
        }

        foreach(Button button in buttons)
        {
            button.RegisterCallback<ClickEvent>(GetSkillFromButton);
            Debug.Log("Click event registered " + button.text);
        }

        SetLevelMenuInactive();
    }

    private void GetSkillFromButton(ClickEvent evt)
    {
        Button temp = evt.currentTarget as Button;
        string skill = temp.text;
        Debug.Log("Learn skill cklicked " + skill);
        OnSkillSelected.Raise(this, skill);
        SetLevelMenuInactive();
    }

    public void SetHighScore(Component sender, object data)
    {
        if(data is int)
        {
            highscoreUI.text = $"SCORE: {(int) data}";
        }
    }

    public void SetProgress(Component sender, object data)
    {
        if(data is float)
        {
            progressBar.transform.scale = new Vector3((float) data, 1, 1);
        }
        
    }

    public void SetLevelMenuActive(Component sender, object data)
    {
        if (data is Skill[])
        {
            //Show skill selection
            levelMenu.style.display = DisplayStyle.Flex;            
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

            //hide HUD
            int number = 0;
            foreach (VisualElement element in HUD.hierarchy.Children())
            {
                element.style.display = DisplayStyle.None;                
            }
        } 
    }

    public void SetLevelMenuInactive()
    {
        // hide skill selection
        foreach(Button button in buttons)
        {
            button.visible = false;
        }

        levelMenu.style.display = DisplayStyle.None;

        // show HUD
        foreach(VisualElement element in HUD.hierarchy.Children())
        {
            element.style.display = DisplayStyle.Flex;
        }

        Debug.Log("Level Menu inactive");
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
