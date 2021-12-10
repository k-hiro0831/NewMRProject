using UnityEngine;

public class TestBullet: MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private Vector3 _v3 = new Vector3(0.0f, 0.0f, 1.0f);
    [SerializeField]
    private float speed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject,1f);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
