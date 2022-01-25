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
    }

    public void Click()
    {
        if (flow == Flow.start)
        {
            flow = Flow.main;
            SceneManager.LoadScene("Handtracking");
        }
    }
}
