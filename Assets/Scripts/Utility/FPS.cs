using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FPS : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        StartCoroutine(UpdateFPS());
        IEnumerator UpdateFPS(float tick = 0.5f)
        {
            while (true) {
                float fps = Mathf.RoundToInt(1 / Time.deltaTime);
                textMesh.text = fps.ToString();
                yield return new WaitForSeconds(tick);
            }
        }
    }

}
