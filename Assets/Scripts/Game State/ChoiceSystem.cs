using UnityEngine;

public class ChoiceSystem : MonoBehaviour
{
    [SerializeField] GameObject choicePanel;
    [SerializeField] GameObject[] choices;
    [SerializeField] int numberOfSelections = 2;
    Player player;
    int decisionsMade = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        choicePanel.SetActive(true);
    }

    public void IncreaseDamage(float percentage)
    {
        player.IncreaseDamage(percentage);
        decisionsMade++;
    }

    public void IncreaseSpeed(float percentage)
    {
        player.IncreaseSpeed(percentage);
        decisionsMade++;
    }

    public void IncreaseEnergy(float percentage)
    {
        player.IncreaseEnergy(percentage);
        decisionsMade++;
    }

    void Update()
    {
        if (decisionsMade == numberOfSelections)
        {
            choicePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}