using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class FadeInOut : MonoBehaviour
{
    [Header("List Of Objects")]
    public Image BG;
    public Text UIText;
    public SpriteRenderer sprite;
    public TMPro.TextMeshPro textMeshText;

    [Header("Config Data")]
    public float valueFrom;
    public float valueTo;
    public float time = 1.5f;
    public float delay;
    public Ease easeType = Ease.Linear;
    public bool pingPong = false;

    public UnityEvent callback;

    private bool playing = false;
    private float progress;

    private void Awake()
    {
        if (callback == null)
            callback = new UnityEvent();
    }

    private void Update()
    {
        if (!playing)
            return;

        if (BG != null)
            BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, progress);
        if (UIText != null)
            UIText.color = new Color(UIText.color.r, UIText.color.g, UIText.color.b, progress);
        if (sprite != null)
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, progress);
        if (textMeshText != null)
            textMeshText.color = new Color(textMeshText.color.r, textMeshText.color.g, textMeshText.color.b, progress);
    }

    public void Play()
    {
        progress = valueFrom;
        if (pingPong)
            DOTween.To(() => progress, x => progress = x, valueTo, time).SetDelay(delay).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        else DOTween.To(() => progress, x => progress = x, valueTo, time).SetDelay(delay).SetEase(easeType).OnComplete(OnFinish);

        playing = true;
    }

    public void OnFinish()
    {
        playing = false;
        callback.Invoke();
    }
}