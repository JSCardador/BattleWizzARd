using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }


    [Header("Game Objects")]
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private float _bulletSpeed = 5f;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        this.enabled = false;
    }


    public void StartGame()
    {
        this.enabled = true;
    }


    public void PauseGame()
    {
        this.enabled = false;
    }


    public void EndGame()
    {
        this.enabled = false;
    }
    
    
    private void Update()
    {
#if !UNITY_EDITOR
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
#else
        if (Input.GetMouseButtonUp(0))
#endif
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        GameObject newBullet = Instantiate(_bullet, _camera.transform.position, _camera.transform.rotation);

        newBullet.GetComponent<BulletController>().SetBulletSpeed(_bulletSpeed);
    }
}
