using System;
using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private CanvasGroup group;

    public IEnumerator FadeIn(float duration, float delay, Action finished = null) 
    {
        group.alpha = 1;

        float t = 0;

        yield return new WaitForSecondsRealtime(delay);

        while (t <= duration) {
            t += Time.deltaTime;

            float alpha = t / duration;

            if (group) {
                group.alpha = Mathf.Clamp(Mathf.Lerp(1, 0, alpha), 0, 1);

            }
            
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }

        finished?.Invoke();
    }

    public IEnumerator FadeOut(float duration, float delay, Action finished = null) 
    {
        group.alpha = 0;

        float t = 0;

        yield return new WaitForSecondsRealtime(delay);

        while (t <= duration) {
            t += Time.deltaTime;

            float alpha = t / duration;

            if (group) {
                group.alpha =  Mathf.Clamp(Mathf.Lerp(0, 1, alpha), 0, 1);
            }
           

            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }

        finished?.Invoke();
    }

    private void OnEnable()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 1;
    }
}
