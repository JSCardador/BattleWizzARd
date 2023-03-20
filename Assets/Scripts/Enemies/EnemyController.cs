using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _playerTransform;


    private void Start()
    {
        _playerTransform = Camera.main.transform;
        LookPlayer();
    }


    /// <summary>
    /// Enemy will look at the player
    /// </summary>
    private void LookPlayer()
    {
        transform.LookAt(_playerTransform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
