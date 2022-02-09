using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageEffect : MonoBehaviour
{
    private List<SpriteRenderer> _damageSprites = new List<SpriteRenderer>();

    private float _alpha = 1;

    private void Start()
    {
        foreach(Transform _child in this.transform)
        {
            _damageSprites.Add(_child.gameObject.GetComponent<SpriteRenderer>());
        }

        for (int i = 0; i < _damageSprites.Count; i++)
        {
            _damageSprites[i].color = new Color(255, 255, 255, 0);
        }

        _alpha = 0;
        StartCoroutine(DamageEffectFadeOut());
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StartEffect();
    //    }
    //}

    /// <summary>
    /// ダメージエフェクトを出す
    /// </summary>
    public void StartEffect()
    {
        //透明度をリセット
        for (int i = 0; i < _damageSprites.Count; i++)
        {
            _damageSprites[i].color = new Color(255, 255, 255, 1);
        }
        _alpha = 1;

        //StartCoroutine(DamageEffectFadeOut());
    }

    private IEnumerator DamageEffectFadeOut()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            //少しづつ透明度を落としていく
            _alpha = _alpha - 0.1f;
            for (int i = 0; i < _damageSprites.Count; i++)
            {
                _damageSprites[i].color = new Color(255, 255, 255, _alpha);
            }
        }
    }
}
