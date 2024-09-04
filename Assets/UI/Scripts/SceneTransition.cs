using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private CanvasGroup group;

    public IEnumerator FadeIn(float duration, float delay, Action? finished = null) 
    {
        group.alpha = 1;

        float t = 0;

        yield return new WaitForSecondsRealtime(delay);

        while (group.alpha > 0 && t <= duration) {
            t += Time.deltaTime;

            float alpha = t / duration;
            group.alpha = Mathf.Lerp(1, 0, alpha);

            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }

        if (finished != null) {
            finished();
        }
    }

    public IEnumerator FadeOut(float duration, float delay, Action? finished = null) 
    {
        group.alpha = 0;

        float t = 0;

        yield return new WaitForSecondsRealtime(delay);

        while (group.alpha < 1 && t <= duration) {
            t += Time.deltaTime;

            float alpha = t / duration;
            group.alpha = Mathf.Lerp(0, 1, alpha);

            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }

        if (finished != null) {
            finished();
        }
    }

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 1;
    }
}
