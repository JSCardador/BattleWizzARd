using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageSearcher : MonoBehaviour
{
    public static ImageSearcher Instance { get; private set; }

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

    [Header("AR")]
    [SerializeField] private ARTrackedImageManager _arTrackedImageManager;


    public void StartSearchImage()
    {
        _arTrackedImageManager.enabled = true;
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }


    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            ImageFound(trackedImage);
        }
    }

    private void ImageFound(ARTrackedImage trackedImage)
    {
        _arTrackedImageManager.enabled = false;
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

        GameManager.Instance.SetGameState(GameManager.GameState.InGame);
    }

}
