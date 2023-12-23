using UnityEngine;

public class DeactivateUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    bool panelClosed = false;

    private void Update()
    {
        if (!panelClosed)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        panelClosed = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}