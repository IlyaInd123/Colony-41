﻿using TMPro;
using UnityEngine;

public class TrapPurchaser : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip purchaseSound;
    [SerializeField] AudioClip insufficientFundsSound;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] float energy = 1000f;
    [SerializeField] KeyCode purchaseKey = KeyCode.E;
    [SerializeField] float activationRange = 10f;
    [SerializeField] GameObject trapUI;
    [SerializeField] Vector3 minScale = Vector3.one / 2;
    [SerializeField] Vector3 maxScale = Vector3.one;
    Trap currentTrap;
    Vector3 hitPoint;

    private void Update()
    {
        if (energyText != null) { energyText.text = $"Energy: {energy}"; }

        Trap trapInView = FindTrapInView();
        if (trapInView != null && trapInView != currentTrap)
        {
            currentTrap = trapInView;
            ShowTrapUI(true);
        }
        else if (trapInView == null)
        {
            currentTrap = null;
            ShowTrapUI(false);
        }

        if (currentTrap != null)
        {
            UpdateTrapUIPosition();
            if (Input.GetKeyDown(purchaseKey))
            {
                PurchaseTrap();
            }
        }
    }

    void UpdateTrapUIPosition()
    {
        if (trapUI == null || currentTrap == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(currentTrap.transform.position);
        float distance = Vector3.Distance(hitPoint, Camera.main.transform.position);
        Vector3 scale = Vector3.Lerp(maxScale, minScale, distance / activationRange);
        trapUI.transform.localScale = scale;
        if (screenPos.z < 0)
        {
            trapUI.SetActive(false);
        }
        else
        {
            trapUI.transform.position = screenPos;
            trapUI.SetActive(true);
        }
    }

    Trap FindTrapInView()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, activationRange))
        {
            Trap trapHit = hit.collider.GetComponent<Trap>();
            if (trapHit != null && !trapHit.active)
            {
                hitPoint = hit.point;
                return trapHit;
            }
        }

        return null;
    }

    void ShowTrapUI(bool show)
    {
        if (trapUI == null) { return; }
        trapUI.SetActive(show);
    }

    private void PurchaseTrap()
    {
        if (energy >= currentTrap.Cost && !currentTrap.active)
        {
            if (audioSource != null && purchaseSound != null) { audioSource.PlayOneShot(purchaseSound); }
            energy -= currentTrap.Cost;
            currentTrap.Activate();
            ShowTrapUI(false);
        }
        else
        {
            if (audioSource != null && insufficientFundsSound != null) { audioSource.PlayOneShot(insufficientFundsSound); }
        }
    }

    public void IncreaseEnergy(float amount)
    {
        energy += amount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}