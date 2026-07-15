using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string targetScene = "YourSceneName";
    private bool isTransitioning = false;
  

    public void TriggerTransition()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            StartCoroutine(TransitionDelay());
        }
    }

    private IEnumerator TransitionDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(targetScene);
    }
}
