using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class PageFlipping : MonoBehaviour
{
    [SerializeField] private int pageIndex = 0;

    [SerializeField] private UnityEvent onPageFlipped;

    private Coroutine rotationTween;

    public bool IsTweening => rotationTween != null;

    public void FlipPageForward()
    {
        if (IsTweening)
            return;

        var pages = GetPages();

        if (pageIndex + 2 > pages.Count)
            return;

        ChangeRotation(pages[pageIndex], 90, 1);
        pageIndex++;
        onPageFlipped?.Invoke();

    }
    public void FlipPageBackward()
    {
        if (IsTweening)
            return;

        var pages = GetPages();

        if (pageIndex - 1 < 0)
            return;

        ChangeRotation(pages[pageIndex-1], 0, 1);
        pageIndex--;
        onPageFlipped?.Invoke();


    }

    private void ChangeRotation(RectTransform obj,float value,float duration = 1)
    {
        rotationTween = StartCoroutine(ChangeRotation());
        IEnumerator ChangeRotation()
        {
            Quaternion startRotation = obj.localRotation;
            Quaternion endRotation = Quaternion.Euler(new Vector3(startRotation.x, value,startRotation.z));
            float timer = 0;
            while (timer < duration)
            {
                float t = timer / duration;
                obj.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
                timer += Time.deltaTime;
                yield return null;
            }
            rotationTween = null;
        }
    }

    private List<RectTransform> GetPages()
    {
        List<RectTransform> pages = new List<RectTransform>();

        for (int i = transform.childCount-1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (child is RectTransform page)
                pages.Add(page);
        }

        return pages;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(PageFlipping))]
public class PageFlippingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var self = (PageFlipping)target;

        if (GUILayout.Button("Flip Forward"))
            self.FlipPageForward();

        if (GUILayout.Button("Flip Backward"))
            self.FlipPageBackward();

    }
}
#endif