using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }


    [Header("Game Objects")]
    [SerializeField] private Camera _camera;


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
#if UNITY_EDITOR
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
#else
        Ray ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
#endif

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == ("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().Die();
            }
        }
    }
}
