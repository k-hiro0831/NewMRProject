using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private EnemControl _enem = new EnemControl();

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enem = GameObject.FindGameObjectWithTag("EnemyCnt").GetComponent<EnemControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ÉvÉåÉCÉÑÅ[ÇÃî≠éÀÇ≥ÇÍÇΩíeÇ…ìñÇΩÇ¡ÇΩîªíË
        if (collision.gameObject.tag == "Bullet") {
            _enem.Removed(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
