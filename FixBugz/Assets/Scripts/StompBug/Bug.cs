using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugData
{
    public float spawnTime;
    public float speed;
    public bool jumping;
    public bool flying;
    public bool fromLeft;
    public int health;
    public BugColor bugColor;

    public BugData(float spawnTime, float speed, bool jumping = false, bool flying = false, bool fromLeft = false, int health = 1)
    {
        this.spawnTime = spawnTime;
        this.speed = speed;
        this.jumping = jumping;
        this.flying = flying;
        this.fromLeft = fromLeft;
        this.health = health;
    }

    public BugData(BugColor bugColor)
    {
        this.bugColor = bugColor;
    }
}

public class Bug : MonoBehaviour {

    public SpriteRenderer spriteRender;
    public int health;
    public GameObject deathEffect;
    public GameObject attackedEffect;

    private BugData bugData;
    private Color color;

    public void Init(BugData bugData)
    {
        this.bugData = bugData;
        Color modifiedColor;
        Sprite colorSprite = BugColoring.Instance.GetSpriteByType(bugData.bugColor, out modifiedColor);
        if (colorSprite != null)
        {
            spriteRender.sprite = colorSprite;
            color = modifiedColor;
        }
        health = bugData.health;
    }

	public void TakeDamage(int damage) {
        //if (attackedEffect != null)
        //    Instantiate(attackedEffect, transform.position, Quaternion.identity);
        health -= damage;
        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        if (deathEffect != null)
        {
            GameObject effectObj = Instantiate(deathEffect, transform.position, Quaternion.identity);
            UpdateParticleColor(effectObj);
        }

        BugUtils.KillBugOccur();

        Destroy(gameObject);
    }

    private void UpdateParticleColor(GameObject effectObj)
    {
        ParticleSystem.MainModule main = effectObj.GetComponent<ParticleSystem>().main;
        main.startColor = color;

        foreach (Transform child in effectObj.transform)
        {
            ParticleSystem ps = child.GetComponent<ParticleSystem>();
            if (ps == null)
                continue;
            
            main = ps.main;
            main.startColor = color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}
