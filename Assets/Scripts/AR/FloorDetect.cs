using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FloorDetect : MonoBehaviour
{
    public static FloorDetect Instance { get; private set; }

    [Header("AR")]
    [SerializeField] private ARPlaneManager _arPlaneManager;


    [Header("UI")]
    [SerializeField] private TMPro.TMP_Text _floorSizeText;


    // Private variables

    // We will use these 3 variables to calculate a surface of 2.5 m2 (or more) and express it as a percentage.
    private Vector2 _currentPlaneSize
    {
        get => _PlaneSize;
        set
        {
            _PlaneSize = value;
            _floorSizeText.text = $"Floor size: {_floorSizePercentage}%";
        }
    }
    private Vector2 _PlaneSize;
    private ARPlane _currentPlane;
    private float _floorSizeMin = 2.5f;
    private int _floorSizePercentage
    {
        get
        {
            Debug.Log("Floor size: " + ((_currentPlaneSize.x * _currentPlaneSize.y) / _floorSizeMin) * 100);
            return (int)(((_currentPlaneSize.x * _currentPlaneSize.y) / _floorSizeMin) * 100);
        }
    }



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

        _arPlaneManager.enabled = false;
    }


    private void Start()
    {
        _floorSizeText.text = "Scan the floor";
    }


    /// <summary>
    /// We begin the process of scanning the ground to then place the enemies.
    /// If we already have a scanned plan, we skip this step.
    /// </summary>
    public void StartSearchFloor()
    {
        if (_floorSizePercentage >= 100 && _currentPlane != null)
        {
            Debug.Log("Floor already found");
            OnFinishScan();
        }
        else
        {
            _floorSizeText.text = "Scan the floor";
            _arPlaneManager.enabled = true;
            _arPlaneManager.planesChanged += OnPlanesChanged;
        }
    }


    /// <summary>
    ///  Called when the ARPlaneManager detects a change in the planes. 
    /// </summary>
    /// <param name="eventArgs"></param>
    private void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        foreach (var plane in eventArgs.added)
        {
            FloorFound(plane);
        }

        foreach (var updatedPlane in eventArgs.updated)
        {
            OnPlaneUpdated(updatedPlane);
        }

    }


    /// <summary>
    ///  Called when the size of the plane is updated.
    /// If the floor is big enough, the search is finished.
    /// </summary>
    /// <param name="plane"></param>
    private void OnPlaneUpdated(ARPlane plane)
    {
        _currentPlaneSize = plane.size;

        if (_floorSizePercentage >= 100)
        {
            OnFinishScan();
        }
    }


    /// <summary>
    ///  Called when the floor is found.
    /// </summary>
    /// <param name="plane"></param>
    private void FloorFound(ARPlane plane)
    {
        _currentPlane = plane;
        _currentPlaneSize = plane.size;
    }


    /// <summary>
    /// Called when the floor is big enough.
    /// Disables the ARPlaneManager and changes the game state to ImageSearch.
    /// </summary>
    private void OnFinishScan()
    {
        _arPlaneManager.enabled = false;
        _arPlaneManager.planesChanged -= OnPlanesChanged;

        GameManager.Instance.SetGameState(GameManager.GameState.ImageSearch);
        HidePlanes();
    }

    /// <summary>
    /// Hide all the planes so that they do not obstruct the game.
    /// </summary>
    private void HidePlanes()
    {
        foreach (var plane in _arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    ///  Returns the height of the floor.
    /// </summary>
    /// <returns></returns>
    public float GetFloorHeight()
    {
#if UNITY_EDITOR
        return 0f;
#else
        return _currentPlane.transform.position.y;
#endif
    }
}
