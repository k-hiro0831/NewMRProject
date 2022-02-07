using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _hit = false;
    private bool _atk = false;
    /// <summary>
    /// プレイヤーHP外部参照元
    /// </summary>
    private float _playerHitPoint = 100;
    /// <summary>
    /// プレイヤーHP外部参照用
    /// </summary>
    public float _playerHp{
        get { return _playerHitPoint; }
    }
    [SerializeField]
    private HpGauge _hpGauge;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "EnemyAtk")
        {
            //Atk();
        }
    }

    public void Atk(int _value)
    {
        if (!_hit)
        {
            _hit = true;
            _hpGauge.MinusHp(_value);
            Invoke("Hit", 2.0f);
        }
    }

    private void HPDown(){
        _playerHitPoint = _playerHitPoint - 1;
    }

    private void Hit(){
        _hit = false;
    }
}
