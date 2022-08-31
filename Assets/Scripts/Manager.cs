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
    [SerializeField] private Button _nextEvent_Button;
    [SerializeField] private Data data;
    [SerializeField] private GameObject[] levels;

    [HideInInspector] public int TotalPoint { get { return _totalPoint; } set { _totalPoint = value; } }
    [HideInInspector] public int EarnedPoint { get { return _earnedPoint; } set { _earnedPoint = value; } }
    [HideInInspector] public int CoinPoint { get { return _coinPoint; } set { _coinPoint = value; } }
    [HideInInspector] public bool IsLevelOver { get { return _isLevelOver; } set { _isLevelOver = value; } }
    [HideInInspector] public bool IslevelCompleted { get { return _islevelCompleted; } set { _islevelCompleted = value; } }

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
        PlayerPrefs.GetInt("TotalPoint", TotalPoint);

        _nextEvent_Button.onClick.AddListener(NextEvent);

        for(int i = 0; i < data.islevel.Length; i++)
        {
            if(data.islevel[i])
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
        if(_islevelCompleted)
        {
            LevelCompletedEvent();
        }
        if(_isLevelOver)
        {
            LevelOverEvent();
        }
    }

    public void WriteTexts()
    {
        _totalPoint_Txt.text = _totalPoint.ToString();
        _earnedPoint_Txt.text = _earnedPoint.ToString();
    }

    void LevelCompletedEvent()
    {
        TotalPoint = _totalPoint + _earnedPoint;
        _result_Panel.SetActive(true);
        PlayerPrefs.SetInt("TotalPoint", TotalPoint);
        data.levelCount++;
        data.islevel[data.levelCount] = true;
        IslevelCompleted = false;
    }

    void LevelOverEvent()
    {
        _result_Panel.SetActive(true);
        _isLevelOver = false;
    }

    void NextEvent()
    {
        SceneManager.LoadScene("MainScene");
    }
    #endregion


}
