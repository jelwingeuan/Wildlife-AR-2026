using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARAnimalPlacer : MonoBehaviour
{
    [Header("AR References")]
    [SerializeField] private Camera arCamera;
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private ARPlaneManager arPlaneManager;

    [Header("Baby Animal")]
    [SerializeField] private GameObject animalPrefab;
    [SerializeField] private float animalScale = 0.05f;
    [SerializeField] private float spawnYOffset = 0.02f;

    [Header("Adult Animal")]
    [SerializeField] private GameObject adultAnimalPrefab;
    [SerializeField] private float adultAnimalScale = 0.08f;

    [Header("Heart UI")]
    [SerializeField] private Image heartImage;
    [SerializeField] private Sprite blankHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite fullHeartSprite;

    [Header("Food Buttons")]
    [SerializeField] private Button chickenButton;
    [SerializeField] private Button meatButton;
    [SerializeField] private Button bananaButton;

    [Header("Animation Trigger Names")]
    [SerializeField] private string jumpTriggerName = "JumpTrigger";
    [SerializeField] private string runTriggerName = "RunTrigger";

    private GameObject spawnedAnimal;
    private Animator animalAnimator;

    private bool animalPlaced = false;
    private bool isAdult = false;
    private int lifePoint = 0;

    private static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        UpdateHeartUI();
        SetFoodButtons(true);
    }

    private void Update()
    {
        if (animalPlaced)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
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

            spawnedAnimal.AddComponent<ARAnchor>();

            animalAnimator = spawnedAnimal.GetComponentInChildren<Animator>();

            animalPlaced = true;

            HidePlanes();

            Debug.Log("Baby tiger spawned.");
        }
        else
        {
            Debug.LogWarning("No detected AR surface in front of camera.");
        }
    }

    public void FeedChicken()
    {
        FeedCorrectFood();
    }

    public void FeedMeat()
    {
        FeedCorrectFood();
    }

    public void FeedBanana()
    {
        FeedWrongFood();
    }

    private void FeedCorrectFood()
    {
        if (!animalPlaced)
        {
            Debug.Log("Place the tiger first.");
            return;
        }

        if (isAdult)
        {
            Debug.Log("Tiger is already adult. Feeding disabled.");
            return;
        }

        lifePoint += 50;
        lifePoint = Mathf.Clamp(lifePoint, 0, 100);

        PlayJumpAnimation();
        UpdateHeartUI();

        Debug.Log("Correct food. Life Point: " + lifePoint);

        if (lifePoint >= 100)
        {
            StartCoroutine(EvolveToAdult());
        }
    }

    private void FeedWrongFood()
    {
        if (!animalPlaced)
        {
            Debug.Log("Place the tiger first.");
            return;
        }

        if (isAdult)
        {
            Debug.Log("Tiger is already adult. Feeding disabled.");
            return;
        }

        lifePoint -= 50;
        lifePoint = Mathf.Clamp(lifePoint, 0, 100);

        PlayRunAnimation();
        UpdateHeartUI();

        Debug.Log("Wrong food. Life Point: " + lifePoint);
    }

    private void PlayJumpAnimation()
    {
        if (animalAnimator != null)
        {
            animalAnimator.SetTrigger(jumpTriggerName);
        }
    }

    private void PlayRunAnimation()
    {
        if (animalAnimator != null)
        {
            animalAnimator.SetTrigger(runTriggerName);
        }
    }

    private IEnumerator EvolveToAdult()
    {
        isAdult = true;
        SetFoodButtons(false);

        yield return new WaitForSeconds(1.2f);

        Vector3 position = spawnedAnimal.transform.position;
        Quaternion rotation = spawnedAnimal.transform.rotation;

        Destroy(spawnedAnimal);

        spawnedAnimal = Instantiate(adultAnimalPrefab, position, rotation);
        spawnedAnimal.transform.localScale = Vector3.one * adultAnimalScale;

        spawnedAnimal.AddComponent<ARAnchor>();

        animalAnimator = spawnedAnimal.GetComponentInChildren<Animator>();

        Debug.Log("Tiger evolved into adult.");
    }

    private void UpdateHeartUI()
    {
        if (heartImage == null)
            return;

        if (lifePoint <= 0)
        {
            heartImage.sprite = blankHeartSprite;
        }
        else if (lifePoint == 50)
        {
            heartImage.sprite = halfHeartSprite;
        }
        else if (lifePoint >= 100)
        {
            heartImage.sprite = fullHeartSprite;
        }
    }

    private void SetFoodButtons(bool enabled)
    {
        if (chickenButton != null)
            chickenButton.interactable = enabled;

        if (meatButton != null)
            meatButton.interactable = enabled;

        if (bananaButton != null)
            bananaButton.interactable = enabled;
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