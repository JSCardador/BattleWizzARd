using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }


    [Header("Game Objects")]
    [SerializeField] private Camera _camera;

    [SerializeField] private Image _liveBar;


    private int Lives = 10;


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

        Lives = 10;
        _liveBar.fillAmount = (float)Lives / 10;
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


    /// <summary>
    /// Shoots a raycast from the camera to the mouse position (to test in the Editor) or the first touch position.
    /// If the raycast hits an enemy, it calls the Die() method of the EnemyController script.
    /// If the raycast hits an enemy bullet, it destroys the bullet.
    /// </summary>
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
            else if (hit.collider.tag == ("EnemyBullet"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }


    /// <summary>
    /// If the player collides with an enemy bullet, it destroys the bullet and decreases the player's lives.
    /// If the player has no more lives, it calls the SetGameState() method of the GameManager script.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            Lives--;
            _liveBar.fillAmount = (float)Lives / 10;

            if (Lives <= 0)
            {
                GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
            }
        }
    }
}
