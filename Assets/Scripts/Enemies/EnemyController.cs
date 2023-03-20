using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnPoint;

    // Private variables
    private Transform _playerTransform;
    private Animator _animator;


    private void Start()
    {
        _playerTransform = Camera.main.transform;
        _animator = GetComponent<Animator>();
        LookPlayer();
        StartCoroutine(Shoot());
    }


    /// <summary>
    /// Enemy will look at the player
    /// </summary>
    private void LookPlayer()
    {
        transform.LookAt(_playerTransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }


    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            _animator.SetTrigger("Shoot");
            yield return new WaitForSeconds(0.15f);
            Instance_OnEnemyShoot();
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot")); // Wait until the animation is finished
        }
    }


    private void Instance_OnEnemyShoot()
    {
        Debug.Log("Enemy shoot");
        GameObject bullet = Instantiate(_bullet, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);
    }


    public void Die()
    {
        StopCoroutine(Shoot());
        _animator.SetTrigger("Die");
        EnemiesManager.Instance.RemoveEnemy(gameObject);
        Destroy(gameObject, 2f);
    }
}
