using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private Vector3 offScreenPosition = new Vector3(12, -0.2f, 1);
    [SerializeField] private Vector3 onScreenPosition = new Vector3(7, -0.2f, 1);
    [SerializeField] private float slideDuration = 0.2f;
    private Transform transform;

    private void Awake()
    {
        if (this.gameObject != null)
        {
            transform = this.gameObject.GetComponent<Transform>();
        }
    }

    public void SpawnNPC()
    {
        if (this.gameObject != null)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(Slide(offScreenPosition, onScreenPosition));
        }
    }

    public void UnpawnNPC()
    {
        if (this.gameObject != null)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(Slide(onScreenPosition, offScreenPosition, true));
        }
    }

    private IEnumerator Slide(Vector3 start, Vector3 end, bool slidingIn = true)
    {
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / slideDuration);
            transform.localPosition = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.localPosition = end;

        if (slidingIn)
        {
            StartCoroutine(Talk());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator Talk()
    {
        yield return null;
    }
}
