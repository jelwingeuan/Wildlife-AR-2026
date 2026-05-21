using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARAnimalPlacer : MonoBehaviour
{
    [Header("AR References")]
    [SerializeField] private Camera arCamera;
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private ARPlaneManager arPlaneManager;

    [Header("Animal")]
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private float animalScale = 1f;

    private GameObject spawnedAnimal;
    private bool animalPlaced = false;

    private static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Update()
    {
        if (animalPlaced)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        TryPlaceAnimal(touch.position);
    }

    private void TryPlaceAnimal(Vector2 touchPosition)
    {
        Debug.Log("Screen touched. Trying to place animal...");

        if (animalPrefab == null)
        {
            Debug.LogError("Animal Prefab is missing.");
            return;
        }

        if (arRaycastManager == null)
        {
            Debug.LogError("AR Raycast Manager is missing.");
            return;
        }

        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            Quaternion rotation = GetAnimalFacingCameraRotation(hitPose.position);

            spawnedAnimal = Instantiate(animalPrefab, hitPose.position, rotation);
            spawnedAnimal.transform.localScale = Vector3.one * animalScale;

            animalPlaced = true;

            HidePlanes();

            Debug.Log("Animal placed successfully.");
        }
        else
        {
            Debug.LogWarning("No AR plane detected at touch position.");
        }
    }

    private Quaternion GetAnimalFacingCameraRotation(Vector3 animalPosition)
    {
        Vector3 direction = arCamera.transform.position - animalPosition;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return Quaternion.identity;

        return Quaternion.LookRotation(direction);
    }

    private void HidePlanes()
    {
        if (arPlaneManager == null)
            return;

        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        arPlaneManager.enabled = false;
    }
}