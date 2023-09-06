using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeButton : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;
    public float fadeDuration = 2.0f; 

   //private void Start()
   //{
   //    rend = GetComponent<Renderer>();
   //    startColor = rend.material.color;
   //    startColor.a = 0.0f;
   //    rend.material.color = startColor;
   //}

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color currentColor = startColor;

        while (elapsedTime < fadeDuration)
        {
            currentColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            rend.material.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentColor.a = 1.0f;
        rend.material.color = currentColor;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color currentColor = rend.material.color;

        while (elapsedTime < fadeDuration)
        {
            currentColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            rend.material.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentColor.a = 0.0f;
        rend.material.color = currentColor;
    }
}
