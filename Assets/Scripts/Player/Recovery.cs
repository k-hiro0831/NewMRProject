using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _par;
    [SerializeField]
    private ParticleSystem _par2;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera" && _par.gameObject.activeSelf == true)
        {
            _par.gameObject.SetActive(false);
            _par2.gameObject.SetActive(true);
            _player.Recovery();
            Invoke("False", 1.5f);
        }
    }

    private void False()
    {
        _par2.gameObject.SetActive(false);
    }
}
