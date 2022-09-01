using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    #region Values
    [SerializeField] private Text _totalPoint_Txt;
    [SerializeField] private Text _earnedPoint_Txt;
    [SerializeField] private Text _coinPoint_Txt;
    [SerializeField] private GameObject _result_Panel;
    [SerializeField] private GameObject _tapToStart_Panel;
    [SerializeField] private Button _nextEvent_Button;
    [SerializeField] private Data data;
    [SerializeField] private GameObject[] levels;
    bool _isGameStarted = false;

    [HideInInspector] public int TotalPoint { get { return _totalPoint; } set { _totalPoint = value; } }
    [HideInInspector] public int EarnedPoint { get { return _earnedPoint; } set { _earnedPoint = value; } }
    [HideInInspector] public int CoinPoint { get { return _coinPoint; } set { _coinPoint = value; } }
    [HideInInspector] public bool IsLevelOver { get { return _isLevelOver; } set { _isLevelOver = value; } }
    [HideInInspector] public bool IslevelCompleted { get { return _islevelCompleted; } set { _islevelCompleted = value; } }
    [HideInInspector] public bool IsGameStarted { get { return _isGameStarted; } set { _isGameStarted = value; } }

    int _totalPoint;
    int _earnedPoint;
    int _coinPoint;
    bool _isLevelOver = false;
    bool _islevelCompleted = false;
    #endregion

    //ittirme, uzun ittirme, degerlerse oyuncuyu fýrlatýrlar biraz.

    #region Calling
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt("TotalPoint", _totalPoint);

        _nextEvent_Button.onClick.AddListener(NextEvent);

        for (int i = 0; i < data.islevel.Length; i++)
        {
            if (data.islevel[i])
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_islevelCompleted)
        {
            LevelCompletedEvent();
            Debug.Log(_totalPoint);
        }
        if (_isLevelOver)
        {
            LevelOverEvent();
        }
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            TapToStartEvent();
            _isGameStarted = true;
        }
    }

    void TapToStartEvent()
    {
        _tapToStart_Panel.SetActive(false);
    }

    public void WriteTexts()
    {
        _totalPoint_Txt.text = "Total Point = " + _totalPoint.ToString();
        _earnedPoint_Txt.text = "Earned Point = " + _earnedPoint.ToString();
    }

    void LevelCompletedEvent()
    {
        _totalPoint = _totalPoint + _earnedPoint;
        _result_Panel.SetActive(true);
        PlayerPrefs.SetInt("TotalPoint", _totalPoint);
        data.levelCount++;
        data.islevel[data.levelCount] = true;
        WriteTexts();
        IslevelCompleted = false;
    }

    void LevelOverEvent()
    {
        _result_Panel.SetActive(true);
        _totalPoint = _totalPoint + _earnedPoint;
        PlayerPrefs.SetInt("TotalPoint", _totalPoint);
        WriteTexts();
        _isLevelOver = false;
    }

    void NextEvent()
    {
        SceneManager.LoadScene("MainScene");
    }
    #endregion


}
