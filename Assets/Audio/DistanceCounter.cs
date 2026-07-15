using UnityEngine;
using TMPro;
using System.Collections;

public class DistanceCounter : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float duration = 3f;

    private void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        float current = 150f;
        float elapsed = 0f;

        while (current > 0f)
        {
            elapsed += Time.deltaTime;
            current = Mathf.Lerp(100f, 0f, elapsed / duration);
            countdownText.text = Mathf.CeilToInt(current).ToString();
            yield return null;
        }

        countdownText.text = "0";
    }
}
