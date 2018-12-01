using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bug))]
public class PaintBug : MonoBehaviour {

    public GameObject paintEffect;
    public BugColor bugColor;

    private Bug bugCtrl;
    private Color rbgColor;

    void Start()
    {
        bugCtrl = GetComponent<Bug>();
    }

    public void Paint(BugColor colorToPaint)
    {
        Sprite colorSprite = BugColoring.Instance.GetSpriteByType(colorToPaint, out rbgColor);
        if (colorSprite != null)
        {
            bugColor = colorToPaint;
            bugCtrl.spriteRender.sprite = colorSprite;
            PlayPaintEffect();
        }
    }



    private void PlayPaintEffect()
    {
        if (paintEffect != null)
        {
            GameObject effectObj = Instantiate(paintEffect, transform.position, Quaternion.identity);
            UpdateParticleColor(effectObj);
        }
    }

    private void UpdateParticleColor(GameObject effectObj)
    {
        ParticleSystem.MainModule main = effectObj.GetComponent<ParticleSystem>().main;
        main.startColor = rbgColor;

        foreach (Transform child in effectObj.transform)
        {
            ParticleSystem ps = child.GetComponent<ParticleSystem>();
            if (ps == null)
                continue;

            main = ps.main;
            main.startColor = rbgColor;
        }
    }
}
