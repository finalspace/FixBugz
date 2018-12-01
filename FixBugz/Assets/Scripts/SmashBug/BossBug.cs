using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bug))]
public class BossBug : MonoBehaviour {

    public int maxHealth;

    private Bug bugCtrl;
    private float initScale = 0.5f;
    private int currentHealth;

    void Start()
    {
        bugCtrl = GetComponent<Bug>();
    }

    void Update()
    {
        currentHealth = bugCtrl.health;
        int healthLost = maxHealth - currentHealth;
        float ratio = (float)healthLost / maxHealth;
        float sizeRadio = Mathf.Lerp(1, 2, ratio);
        transform.localScale = Vector3.one * initScale * sizeRadio;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y * 0.85f + 0.05f, transform.localPosition.z);
    }

    public void Init(int health)
    {
        maxHealth = health;
    }
}
