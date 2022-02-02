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

    private EnemyValueManager _scoreManage;
    [SerializeField]
    private int _gameState;
    [SerializeField]
    private int _score;
    [SerializeField]
    private string[] _nextSceneName = null;
    [SerializeField]
    private GameObject[] _button;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _button[0].SetActive(false);
        _button[2].SetActive(false);
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

        if (flow == Flow.main && _gameState == 1){
            _gameState = 0;
            _score = _scoreManage.ScoreTotal(_score);
            Invoke("GameFinish", 1.0f);
            flow = Flow.clear;
        }

        if (flow == Flow.main && _gameState == 2){
            _gameState = 0;
            _score = _scoreManage.ScoreTotal(_score);
            Invoke("GameFinish", 1.0f);
            flow = Flow.gameover;
        }
    }

    public void StartClick()
    {
        if (flow == Flow.clear || flow == Flow.gameover)
        {
            _score = 0;
            flow = Flow.main;
        }
        _button[0].SetActive(false);
        _button[1].SetActive(false);
        _button[2].SetActive(false);
        Invoke("SceneMove", 1.0f);
    }

    public void RestartClick()
    {
        if (flow == Flow.clear || flow == Flow.gameover)
        {
            _score = 0;
            flow = Flow.main;
        }
        _button[0].SetActive(false);
        _button[1].SetActive(false);
        _button[2].SetActive(false);
        Invoke("SceneMove", 1.0f);
    }

    public void ExitClick()
    {

    }

    public void End(int _value)
    {
        _gameState = _value;
    }

    private void SceneMove()
    {
        Invoke("ScoreSearch", 2.0f);
        flow = Flow.main;
        SceneManager.LoadScene(_nextSceneName[0]);

    }

    private void GameFinish()
    {
        if (flow == Flow.clear)
        {
            _button[1].SetActive(true);
            SceneManager.LoadScene(_nextSceneName[1]);
        }

        if (flow == Flow.gameover)
        {
            _button[1].SetActive(true);
            SceneManager.LoadScene(_nextSceneName[2]);
        }
    }

    private void ScoreSearch()
    {
        _scoreManage = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<EnemyValueManager>();
    }


}
