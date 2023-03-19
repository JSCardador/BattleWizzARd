using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FloorDetect : MonoBehaviour
{
    public FloorDetect Instance { get; private set; }

    [Header("AR")]
    [SerializeField] private ARPlaneManager _arPlaneManager;


    [Header("UI")]
    [SerializeField] private TMPro.TMP_Text _floorSizeText;


    // Private variables
    private Vector2 _currentPlaneSize
    {
        get => _currentPlaneSize;
        set
        {
            _floorSizeText.text = $"Floor size: {_floorSizePercentage}%";
        }
    }
    private float _floorSizeMin = 2.5f;
    private int _floorSizePercentage
    {
        get
        {
            return (int)((_currentPlaneSize.x * _currentPlaneSize.y) / _floorSizeMin * 100);
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
    }


    private void Start()
    {
        _floorSizeText.text = "Scan the floor";
    }


    public void StartSearchFloor()
    {
        _arPlaneManager.enabled = true;
        _arPlaneManager.planesChanged += OnPlanesChanged;
    }


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


    private void OnPlaneUpdated(ARPlane plane)
    {
        _currentPlaneSize = plane.size;

        if (_floorSizePercentage >= 100)
        {
            _arPlaneManager.enabled = false;
            _arPlaneManager.planesChanged -= OnPlanesChanged;

            GameManager.Instance.SetGameState(GameManager.GameState.ImageSearch);
        }
    }


    private void FloorFound(ARPlane plane)
    {
        _currentPlaneSize = plane.size;
    }
}
