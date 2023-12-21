using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour
{
    [SerializeField] bool hasChildren = false;
    [Header("Base Trap Settings")]
    [SerializeField] float activationTime = 30f;
    [SerializeField] float cost = 100f;
    [SerializeField] Image cooldownImage;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;
    [SerializeField] AudioSource audioSource;
    [SerializeField] MeshRenderer meshRenderer;
    MeshRenderer[] meshRenderers;
    public bool active { get; private set; } = false;
    public float Cost => cost;

    private void Awake()
    {
        if (!hasChildren && meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = inactiveMaterial;
        }
        else
        {
            meshRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.material = inactiveMaterial;
            }
        }
    }

    public void Activate()
    {
        StartCoroutine(ActiveTrap());
    }

    IEnumerator ActiveTrap()
    {
        float timer = activationTime;
        active = true;
        if (activeMaterial != null) 
        {
            if (!hasChildren)
            {
                meshRenderer.material = activeMaterial;
            }
            else
            {
                foreach (MeshRenderer renderer in meshRenderers)
                {
                    renderer.material = activeMaterial;
                }
            }
        }
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (cooldownImage != null) { cooldownImage.fillAmount = timer / activationTime; }
            yield return null;
        }
        if (inactiveMaterial != null) 
        {
            if (!hasChildren)
            {
                meshRenderer.material = inactiveMaterial;
            }
            else
            {
                foreach (MeshRenderer renderer in meshRenderers)
                {
                    renderer.material = inactiveMaterial;
                }
            }
        }
        active = false;
        if (audioSource != null) { audioSource.Play(); }
    }
}