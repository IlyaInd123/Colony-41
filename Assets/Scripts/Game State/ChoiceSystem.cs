using UnityEngine;

public class ChoiceSystem : MonoBehaviour
{
    [SerializeField] GameObject choicePanel;
    [SerializeField] GameObject[] choices;
    [SerializeField] int numberOfSelections = 2;
    [SerializeField] [Range(0f, 1f)] float defenderBaseHPReduction = 0.5f;
    Player player;
    int decisionsMade = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        choicePanel.SetActive(true);
    }

    public void IncreaseDamage(float percentage)
    {
        choices[1].SetActive(false);
        player.IncreaseDamage(percentage);
        decisionsMade++;
    }

    public void IncreaseSpeed(float percentage)
    {
        choices[0].SetActive(false);
        player.IncreaseSpeed(percentage);
        decisionsMade++;
    }

    public void IncreaseEnergy(float percentage)
    {
        choices[2].SetActive(false);
        player.IncreaseEnergy(percentage);
        decisionsMade++;
    }

    void Update()
    {
        if (decisionsMade == numberOfSelections)
        {
            choicePanel.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FindObjectOfType<DefenderBase>().ReduceHealth(defenderBaseHPReduction);
            Destroy(this);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}