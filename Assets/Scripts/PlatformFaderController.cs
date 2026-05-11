using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformFaderController : MonoBehaviour
{
    public PlatformFader[] platforms;

    public float fadeDuration = 1f;
    public float hiddenTime = 2f;
    public float activeTime = 2f;

    void Start()
    {
        StartCoroutine(RunLoop());
    }

    IEnumerator RunLoop()
    {
        while (true)
        {
            // pick 2 random platforms
            List<int> indices = new List<int>();

            while (indices.Count < 2 && indices.Count < platforms.Length)
            {
                int rand = Random.Range(0, platforms.Length);

                if (!indices.Contains(rand))
                    indices.Add(rand);
            }

            PlatformFader p1 = platforms[indices[0]];
            PlatformFader p2 = platforms[indices[1]];

            // FADE OUT
            yield return StartCoroutine(FadePlatform(p1, 1f, 0f));
            yield return StartCoroutine(FadePlatform(p2, 1f, 0f));

            // disable colliders AFTER fade
            p1.col.enabled = false;
            p2.col.enabled = false;

            yield return new WaitForSeconds(hiddenTime);

            // enable colliders BEFORE fade in
            p1.col.enabled = true;
            p2.col.enabled = true;

            // FADE IN
            yield return StartCoroutine(FadePlatform(p1, 0f, 1f));
            yield return StartCoroutine(FadePlatform(p2, 0f, 1f));

            yield return new WaitForSeconds(activeTime);
        }
    }

    IEnumerator FadePlatform(PlatformFader p, float start, float end)
    {
        float t = 0f;
        Color c = p.sr.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(start, end, t / fadeDuration);
            p.sr.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }

        p.sr.color = new Color(c.r, c.g, c.b, end);
    }
}