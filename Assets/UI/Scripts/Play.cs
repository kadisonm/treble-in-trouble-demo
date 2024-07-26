using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Sprite loading;
    [SerializeField] private float duration;

    private bool pressed = false;

    IEnumerator Fade() 
    {
        float t = 0;

        while (group.alpha < 1) {
            t += Time.deltaTime;

            float alpha = t / duration;
            group.alpha = Mathf.Lerp(0, 1, alpha);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        SceneManager.LoadScene("Demo");
    }

    public void OnPlay()
    {
        if (pressed == false) {
            pressed = true;
            StartCoroutine(Fade());
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<Image>().sprite = loading;
        }
    }
}
