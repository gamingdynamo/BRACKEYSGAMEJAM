using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class NotificationComponent : MonoBehaviour
{
    public TextMeshProUGUI notificationText;
    private CanvasGroup canvasGroup;


    public void StartNotification(string message,float time, float blend)
    {
        canvasGroup = GetComponent<CanvasGroup>();


        if (!notificationText || !canvasGroup)
            Destroy(gameObject);

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.0f;

        notificationText.text = message;

        StartCoroutine(ShowNotification());
        IEnumerator ShowNotification()
        {
            float timer = 0.0f;

            while(timer < blend)
            {
                canvasGroup.alpha = Mathf.Lerp(0,1,timer / blend);
                timer += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(time);

            timer = 0.0f;

            while (timer < blend)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, timer / blend);
                timer += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
