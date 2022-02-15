using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandExplanationUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _explanationUI;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ExplanationOpen();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ExplanationClose();
        }
    }

    public void ExplanationOpen()
    {
        _explanationUI.SetActive(true);
    }

    public void ExplanationClose()
    {
        _explanationUI.SetActive(false);
    }
}
