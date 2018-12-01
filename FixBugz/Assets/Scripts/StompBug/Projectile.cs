using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Bug")) {
                hitInfo.collider.GetComponent<Bug>().TakeDamage(1);
                DestroyProjectile(true);
            }

            if (hitInfo.collider.CompareTag("DeadZone"))
            {
                DestroyProjectile();
            }
        }


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile(bool playEffect = false) {
        if (playEffect && destroyEffect != null)
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
