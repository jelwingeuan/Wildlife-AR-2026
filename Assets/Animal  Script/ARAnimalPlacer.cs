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
    [SerializeField] private float animalScale = 0.3f;
    [SerializeField] private float spawnYOffset = 0.02f;

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

        PlaceAnimalInFrontOfCamera();
    }

    private void PlaceAnimalInFrontOfCamera()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            Vector3 spawnPosition = hitPose.position;
            spawnPosition.y += spawnYOffset;

            Quaternion spawnRotation = GetAnimalFacingCameraRotation(spawnPosition);

            spawnedAnimal = Instantiate(animalPrefab, spawnPosition, spawnRotation);
            spawnedAnimal.transform.localScale = Vector3.one * animalScale;

            // Helps the animal stay anchored in AR space.
            spawnedAnimal.AddComponent<ARAnchor>();

            animalPlaced = true;

            HidePlanes();

            Debug.Log("Tiger spawned in front of camera.");
        }
        else
        {
            Debug.LogWarning("No detected AR surface in front of camera.");
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