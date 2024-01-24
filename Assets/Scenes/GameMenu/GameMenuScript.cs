using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;
using System;

public class GameMenuScript : MonoBehaviour
{
    private GameController gameController;
    private Label HighscoreUI;
    private VisualElement ProgressBar;
    private Button[] buttons; 
    private Button leftOption;
    private Button rightOption;

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
        leftOption = root.Q<Button>("Left_Option");
        rightOption = root.Q<Button>("Right_Option");
        buttons = new[] { leftOption, rightOption };

        foreach(Button button in buttons)
        {
            button.RegisterCallback<ClickEvent>(GetSkillFromButton);
        }        
    }

    private void GetSkillFromButton(ClickEvent evt)
    {
        Button temp = evt.currentTarget as Button;
        string skill = temp.text;
        Debug.Log("Learn skill " + skill);
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
            Skill[] wheapons = (Skill[])data;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].text = wheapons[i].name;
                buttons[i].visible = true;
            }
        }

       

    }

    public void SetLevelMenuInactive()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].visible = false;
        }
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
