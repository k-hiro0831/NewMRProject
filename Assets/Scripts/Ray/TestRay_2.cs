using UnityEngine;
using MRProject.Ray;

public class TestRay_2 : MonoBehaviour
{
    RayShot _rayShot = new RayShot();
    
    [SerializeField]
    private float _rayLength = 10f;

    private GameObject _hitObj = null;

    private void Start()
    {
        _rayShot.Initialize(this.gameObject, _rayLength);
    }

    private void Update()
    {

        _hitObj = _rayShot.ReturnHitObj("Enemy");
        if (_hitObj != null)
        {
            Debug.Log("ìñÇΩÇ¡ÇƒÇ¢ÇÈÅI");
        }
        else
        {

        }
    }

    private void OnDrawGizmos()
    {
        _rayShot.GizmosRayView(this.gameObject, _rayLength);
    }
}
