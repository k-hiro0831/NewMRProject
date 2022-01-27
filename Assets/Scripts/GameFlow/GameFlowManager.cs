using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    private enum Flow
    {
        start,main,clear,gameover
    }
    [SerializeField]
    Flow flow = Flow.start;

    private ScoreManager _scoreManage;

    private int _gameState;
    [SerializeField]
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switch (flow)
        {
            case Flow.start:
                break;
            case Flow.main:
                break;
            case Flow.clear:
                break;
            case Flow.gameover:
                break;
        }

        if (flow == Flow.main && _gameState == 1)
        {
            _scoreManage.ScoreAdd(_score);
            flow = Flow.clear;
        }
        if (flow == Flow.main && _gameState == 2)
        {
            _scoreManage.ScoreAdd(_score);
            flow = Flow.gameover;
        }
    }

    public void Click()
    {
        if (flow == Flow.start)
        {
            Invoke("SceneMove", 1.0f);
            Invoke("ScoreSearch", 3.0f);
        }
    }

    public void End(int _value)
    {
        _gameState = _value;
    }

    private void SceneMove()
    {
        flow = Flow.main;
        SceneManager.LoadScene("EnemyNaviTest");
    }

    private void ScoreSearch()
    {
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }
}
