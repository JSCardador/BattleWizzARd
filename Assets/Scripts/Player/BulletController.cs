
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float _bulletSpeed = 5f;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 10);
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    public void SetBulletSpeed(float bulletSpeed)
    {
        _bulletSpeed = bulletSpeed;
    }

}
