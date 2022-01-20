using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _hit = false;
    private bool _atk = false;
    /// <summary>
    /// �v���C���[HP�O���Q�ƌ�
    /// </summary>
    private float _playerHitPoint = 20;
    /// <summary>
    /// �v���C���[HP�O���Q�Ɨp
    /// </summary>
    public float _playerHp{
        get { return _playerHitPoint; }
    }

    private HpGauge _hpGauge;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "EnemyAtk" && !_hit)
        {
            _hit = true;
            _hpGauge.MinusHp(1);
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
