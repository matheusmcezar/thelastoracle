using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Linq;

public class EyesManager : MonoBehaviour
{
    private Light2D leftEyeLight;
    private Light2D rightEyeLight;

    public float changeColorDuration;

    void Awake()
    {
        Transform leftEye = transform.Find("LeftEye");
        Transform rightEye = transform.Find("RightEye");

        if (leftEye != null)
            leftEyeLight = leftEye.GetComponent<Light2D>();

        if (rightEye != null)
            rightEyeLight = rightEye.GetComponent<Light2D>();
    }

    void Start()
    {
        StartCoroutine(HandleEyesRoutine());
    }

    IEnumerator HandleEyesRoutine()
    {
        while (true)
        {
            int fullColor = Random.Range(1, 3);
            float[] RGB = Enumerable.Repeat(1f, fullColor).Concat(Enumerable.Repeat(0f, 3 - fullColor)).ToArray();
            RGB = RGB.OrderBy(x => Random.value).ToArray();

            Color startEyeColor = leftEyeLight.color;
            Color newEyeColor = new Color(RGB[0], RGB[1], RGB[2]);

            float elapsed = 0f;

            while (elapsed < changeColorDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / changeColorDuration;

                leftEyeLight.color = Color.Lerp(startEyeColor, newEyeColor, t);
                rightEyeLight.color = Color.Lerp(startEyeColor, newEyeColor, t);

                yield return null;
            }

            leftEyeLight.color = newEyeColor;
            rightEyeLight.color = newEyeColor;
        }
    }
}
