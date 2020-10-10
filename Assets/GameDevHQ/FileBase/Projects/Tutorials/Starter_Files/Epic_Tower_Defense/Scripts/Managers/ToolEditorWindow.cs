using UnityEngine;
using UnityEditor;
using Scripts.Managers;

public class ToolEditorWindow : EditorWindow
{
    bool _pauseButton;
    bool _resumeButton;
    bool _fastForwardButton;
    bool _setWarFunds;
    int _warFundsAmountToAdd;
    string _warFundsText;

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
        GUILayout.Label("Tower Defence - Debug Tool", EditorStyles.boldLabel);

        GUILayout.Space(_lineBreak);

        //PLAYBACK BUTTONS
        GUILayout.Label("Playback Controls", EditorStyles.boldLabel);
        _pauseButton = GUI.Button(new Rect(position.width/9f, position.height/7, 60, 50), "Pause");
        _resumeButton = GUI.Button(new Rect(position.width /2.55f, position.height / 7, 60, 50), "Resume");
        _fastForwardButton = GUI.Button(new Rect(position.width /1.5f, position.height / 7, 60, 50), "FF");

        GUILayout.Space(_extendedLineBreak);
        //GUILayout.Space(_extendedLineBreak);

        //SET WAR FUNDS
        GUILayout.Label("Set War Funds", EditorStyles.boldLabel);
        _warFundsText = GUI.TextField(new Rect(20, 170, 200, 20), _warFundsText);
        //_warFundsText = GUILayout.TextField(_warFundsText);
        _warFundsAmountToAdd = int.Parse(_warFundsText);
        _setWarFunds = GUI.Button(new Rect(position.width / 3.25f, position.height /2, 100, 50), "Set War Funds");

        //Button with merthod inside to start the game
        PauseGame();
        ResumeGame();
        SpeedGameUp();
        AddWarFunds();

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
        if (_resumeButton == true)
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

    private void AddWarFunds()
    {
        if (_setWarFunds == true)
        {
            GameManager.Instance.SetWarFundsDebug(_warFundsAmountToAdd);
        }
        
    }
}
