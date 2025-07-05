using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandleBurn : MonoBehaviour
{
    private Light2D light;

    private float currentValue;
    public float minValue = 0.8f;
    public float maxValue = 1.2f;
    public float speed = 0.3f;

    void Awake()
    {
        light = this.GetComponent<Light2D>();
    }

    void Start()
    {
        currentValue = 0.8f;
        StartCoroutine(HandleCandleBurn());
    }

    private IEnumerator HandleCandleBurn()
    {
        bool ascending = true;

        while (true)
        {
            float target = ascending ? maxValue : minValue;

            while (Mathf.Abs(currentValue - target) > 0.01f)
            {
                currentValue = Mathf.MoveTowards(currentValue, target, speed * Time.deltaTime);
                light.pointLightOuterRadius = currentValue + 1;
                light.intensity = currentValue;
                yield return null;
            }

            currentValue = target;
            ascending = !ascending;
            yield return null;
        }
    }
}
