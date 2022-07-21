using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class DeliveryCollision : MonoBehaviour
{
    private bool hasPackage = false;
    [SerializeField] private float delayToDestroyPackage = 0.2f;
    [SerializeField] private Color32 hasPackageColor = new Color32(255,0,186,255);
    [SerializeField] private Color32 noPackageColor = new Color32(255,255,255,255);
    [SerializeField] private int packagesToDeliver = 1;
    [SerializeField] private float delayToLoadNextLevel = 1f;
    [SerializeField] private TextMeshProUGUI displayPackages;
    [SerializeField] private TextMeshProUGUI displayHits;
    private SpriteRenderer spriteRenderer;
    private int currentPackagesDelivered = 0;
    private int hitCounter = 0;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ManageDisplayPackageInformation();
        ManageDisplayHitsInformation();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log($"Collision {other.gameObject.name}");
        hitCounter++;
        ManageDisplayHitsInformation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package") && !hasPackage)
        {
            ManagePackagePickup(hasPackageColor, true);
            Destroy(other.gameObject,delayToDestroyPackage);
        }
        if (other.CompareTag("Customer") && hasPackage)
        {
            ManagePackagePickup(noPackageColor, false);
            ManagePackageDelivery();
        }
    }
    
    private void ManagePackagePickup(Color32 color, bool hasPackageStatus)
    {
        spriteRenderer.color = color;
        hasPackage = hasPackageStatus;
    }
    
    private void ManagePackageDelivery()
    {
        currentPackagesDelivered++;
        ManageDisplayPackageInformation();
        if (currentPackagesDelivered >= packagesToDeliver)
        {
            GetComponent<Driver>().enabled = false;
            Invoke(nameof(LoadNextLevel), delayToLoadNextLevel);
        }
    }
    
    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    private void ManageDisplayPackageInformation()
    {
        displayPackages.text = $"{currentPackagesDelivered}/{packagesToDeliver}";
    }

    private void ManageDisplayHitsInformation()
    {
        displayHits.text = $"Crashes: {hitCounter}";
    }
}
