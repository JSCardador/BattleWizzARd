using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageSearcher : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager _arTrackedImageManager;
    
    
    private void Awake()
    {
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
        
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

}
