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

        _arTrackedImageManager.enabled = false;
    }

    [Header("AR")]
    [SerializeField] private ARTrackedImageManager _arTrackedImageManager;


    public void StartSearchImage()
    {
        _arTrackedImageManager.enabled = true;
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }


    /// <summary>
    ///  Called when the tracked images are changed.
    ///  If an image is found, the ARTrackedImageManager is disabled and the game starts.
    ///  The trackedImagesChanged event is unsubscribed.
    ///  The game state is set to InGame.
    /// </summary>
    /// <param name="eventArgs"></param>
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            ImageFound(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            ImageFound(trackedImage);
        }

    }

    /// <summary>
    /// When an image is found, the ARTrackedImageManager is disabled and the game starts.
    /// </summary>
    /// <param name="trackedImage"></param>
    private void ImageFound(ARTrackedImage trackedImage)
    {
        _arTrackedImageManager.enabled = false;
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

        GameManager.Instance.SetGameState(GameManager.GameState.InGame);
    }

}
