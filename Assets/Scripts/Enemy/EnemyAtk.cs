using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtk : MonoBehaviour
{
    private GameObject _enemy;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
