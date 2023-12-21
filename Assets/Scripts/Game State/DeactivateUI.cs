using UnityEngine;

public class DeactivateUI : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}