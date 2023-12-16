using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour
{
    [SerializeField] float activationTime = 30f;
    [SerializeField] float cost = 100f;
    [SerializeField] Image cooldownImage;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;
    [SerializeField] AudioSource audioSource;
    MeshRenderer meshRenderer;
    public bool active { get; private set; } = false;
    public float Cost => cost;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Activate()
    {
        StartCoroutine(ActiveTrap());
    }

    IEnumerator ActiveTrap()
    {
        float timer = activationTime;
        active = true;
        if (activeMaterial != null) { meshRenderer.material = activeMaterial; }
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (cooldownImage != null) { cooldownImage.fillAmount = timer / activationTime; }
            yield return null;
        }
        if (inactiveMaterial != null) { meshRenderer.material = inactiveMaterial; }
        active = false;
        if (audioSource != null) { audioSource.Play(); }
    }
}