using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class WaitAndDown
{
    public int Wait;
    public int Down;
}

public class StateMentPanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _noData;
    [SerializeField]
    private GameObject _paper;
    [SerializeField]
    private StateMentOpinionManager _stateManager;
    [SerializeField]
    private StateMentOpinionTextReader _stateReader;
    [SerializeField]
    private GameObject _closeBtn;
    [SerializeField]
    private OpinionBtnReader _btnReader;

    public GameObject _statePanel;
    public WaitAndDown[] _waitAndDown;

    private int _maxTextNum;

    private void OnEnable()
    {
        _maxTextNum = _stateReader.maxNum;
        AllActiveOff();
        int stateNum = GameManager.Instance.stateNum;

        if (GameManager.Instance.canState)
        {
            Debug.Log("¿€µø");
            StateReady(stateNum, _waitAndDown[stateNum]);
            _closeBtn.SetActive(false);
            _btnReader.ButtonLoad();
        }
        else
        {
            _noData.SetActive(true);
            _closeBtn.SetActive(true);
        }
            
    }

    private void AllActiveOff()
    {
        _paper.SetActive(false);
        _noData.SetActive(false);
    }

    public void ClosePanel()
    {
        AllActiveOff();
        _statePanel.SetActive(false);
    }

    public void StateReady(int text, WaitAndDown secs)
    {
        _paper.SetActive(true);
        _stateReader.TextLoadbyNum(text);
        if (secs.Wait == 0 || secs.Down == 0) _stateManager.StartCameraMove();
        else _stateManager.StartCameraMove(secs.Wait, secs.Down);
    }
}
