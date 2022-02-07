using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkMethod : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private int _enemyAtk;

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
        if (other.gameObject.tag == "MainCamera")
        {
            _player.Atk(_enemyAtk);
        }
    }

    public void EnemyAtk(int _value)
    {
        _enemyAtk = _value;
    }
}
