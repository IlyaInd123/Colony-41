using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    [SerializeField] KeyCode skipButton = KeyCode.Space;
    [SerializeField] int nextSceneIndex = 2;

    private void Update()
    {
        if (Input.GetKeyDown(skipButton))
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}