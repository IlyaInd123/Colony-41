using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneTrigger : MonoBehaviour
{
    [SerializeField] int nextSceneIndex = 2;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
