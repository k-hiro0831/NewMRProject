using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gaugeCanvas;
    [SerializeField]
    private GameObject _parentHand;

    [SerializeField]
    private Vector3 _offsetPos = Vector3.zero;
    [SerializeField]
    private Vector3 _offsetRote = Vector3.zero;

    private void Start()
    {
        this.transform.parent = _parentHand.transform;
        this.transform.position = _parentHand.transform.position + _offsetPos;
    }

    private void Update()
    {
        
    }

    public void GaugeOpen()
    {
        _gaugeCanvas.SetActive(true);
    }
    public void GaugeClose()
    {
        _gaugeCanvas.SetActive(false);
    }

}
