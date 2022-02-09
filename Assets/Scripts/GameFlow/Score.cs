using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _score;

    private GameFlowManager _game;

    private int _scores;
    // Start is called before the first frame update
    void Start()
    {
        _game = GameObject.FindGameObjectWithTag("GameFlow").GetComponent<GameFlowManager>();
        _scores = _game.ScoreCl();
        _score.text = "YOUR SCORE..." + _scores+"!!!"; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
