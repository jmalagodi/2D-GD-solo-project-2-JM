using UnityEngine;

public class PlatformFader : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public BoxCollider2D col;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
}