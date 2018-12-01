using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PulseAnimation : MonoBehaviour
{
    public Transform pulseRoot;

    private string id;
    private float phaseTime = 0.5f;
    private bool playing = false;
    private float scale;

    private void Start()
    {
        id = "goal";

        if (pulseRoot == null)
            pulseRoot = transform;
    }

    private void Update()
    {
        if (!playing)
            scale = 1;

        pulseRoot.transform.localScale = Vector3.one * scale;
    }

    public void Play()
    {
        DOTween.Kill(id);
        scale = 1.5f;
        playing = true;
        DOTween.To(() => scale, x => scale = x, 1, phaseTime).SetEase(Ease.OutCubic).SetId(id).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        playing = false;
    }
}