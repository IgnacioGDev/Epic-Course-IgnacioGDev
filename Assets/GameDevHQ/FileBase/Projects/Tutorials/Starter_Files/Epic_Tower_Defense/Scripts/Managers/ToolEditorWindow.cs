using UnityEngine;
using UnityEditor;
using System;
using Scripts;
using Scripts.Managers;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class ToolEditorWindow : EditorWindow
{
    bool _playGame;
    bool _stopGame;
    bool _pauseButton;
    bool _resumeButton;
    bool _fastForwardButton;
    bool _restartButton;
    bool _setWarFunds;
    bool _setLivesButton;
    bool _enableEnemyColliders;
    bool _disableEnemyColliders;

    //Call mechs in scene
    public static event Action<bool, bool> actionTest;


    int _warFundsAmountToAdd;
    string _warFundsText = "0";
    int _livesAmountToSet;
    string _setLivesText = "0";

    float _lineBreak = 15f;
    float _middleLineBreak = 50f;
    float _extendedLineBreak = 75f;
    bool _groupEnabled;
    bool _testBool = true;
    float _testFloat = 1.23f;


    [MenuItem("Window/Tool Editor Window")]
    static void Init()
    {
        ToolEditorWindow window = (ToolEditorWindow)EditorWindow.GetWindow(typeof(ToolEditorWindow));
        window.Show();
    }

    //public static void ShowWindow()
    //{
    //    GetWindow<ToolEditorWindow>("Tool Editor Window");
    //}


    private void OnGUI()
    {
        //Captures enemies in scene


        GUILayout.Label("Tower Defence - Debug Tool", EditorStyles.boldLabel);

        _playGame = GUILayout.Button("Start Game", GUILayout.Width(100));
        _stopGame = GUILayout.Button("Stop Game", GUILayout.Width(100));

        GUILayout.Space(_lineBreak);

        //PLAYBACK BUTTONS
        GUILayout.Label("Playback Controls", EditorStyles.boldLabel);
        _pauseButton = GUILayout.Button("Pause", GUILayout.Width(100));
        _resumeButton = GUILayout.Button("Resume", GUILayout.Width(100));
        _fastForwardButton = GUILayout.Button("FF", GUILayout.Width(100));

        //RESTART GAME
        _restartButton = GUILayout.Button("Restart Scene", GUILayout.Width(100));


        GUILayout.Space(_lineBreak);
        //GUILayout.Space(_extendedLineBreak);

        //SET WAR FUNDS
        GUILayout.Label("Set War Funds", EditorStyles.boldLabel);
        _warFundsText = GUILayout.TextField(_warFundsText);
        //_warFundsText = GUILayout.TextField(_warFundsText);
        _warFundsAmountToAdd = int.Parse(_warFundsText);
        _setWarFunds = GUILayout.Button("Set War Funds");

        GUILayout.Space(_lineBreak);

        //SET LIVES
        GUILayout.Label("Set amount of lives", EditorStyles.boldLabel);
        _setLivesText = GUILayout.TextArea(_setLivesText);
        _livesAmountToSet = int.Parse(_setLivesText);
        _setLivesButton = GUILayout.Button("Set Lives");

        GUILayout.Space(_lineBreak);

        //ENABLE ENEMY COLLIDERS
        GUILayout.Label("Enemy Colliders", EditorStyles.boldLabel);
        _enableEnemyColliders = GUILayout.Button("Enable Colliders", GUILayout.Width(110));
        _disableEnemyColliders = GUILayout.Button("Disable Colliders", GUILayout.Width(110));


        //Button with merthod inside to start the game
        PauseGame();
        ResumeGame();
        SpeedGameUp();
        SetWarFunds();
        SetLives();
        RestartScene();
        StartGame();
        StopGame();

        EnableEnemyColliders();

        //EXAMPLES

        //_groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", _groupEnabled);
        //_testBool = EditorGUILayout.Toggle("Toogle", _testBool);
        //_testFloat = EditorGUILayout.Slider("Slider", _testFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();



    }

    private void PauseGame()
    {
        if (_pauseButton == true)
        {
            GameManager.Instance.PauseGame();
            Debug.Log("GAme Paused");
        }
    }

    private void ResumeGame()
    {
        if (_resumeButton == true || _restartButton == true)
        {
            GameManager.Instance.ResumeGame();
        }
    }

    private void SpeedGameUp()
    {
        if (_fastForwardButton == true)
        {
            GameManager.Instance.AccelerateGameSpeed();
        }
    }

    private void SetWarFunds()
    {
        if (_setWarFunds == true)
        {
            GameManager.Instance.SetWarFundsDebug(_warFundsAmountToAdd);
        }

    }

    private void SetLives()
    {
        if (_setLivesButton == true)
        {
            GameManager.Instance.SetLivesDebug(_livesAmountToSet);
        }
    }

    private void RestartScene()
    {
        if (_restartButton == true)
        {
            SceneManager.Instance.RestartScene();

        }
    }

    public void StartGame()
    {
        if (_playGame == true)
            EditorApplication.EnterPlaymode();
    }

    public void StopGame()
    {
        if (_stopGame)
        {
            EditorApplication.ExitPlaymode();
        }
    }

    public void EnableEnemyColliders()
    {
        if (actionTest != null)
            actionTest(_enableEnemyColliders, _disableEnemyColliders);
    }
}

