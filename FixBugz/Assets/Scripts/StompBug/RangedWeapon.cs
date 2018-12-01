using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour {

    public float offset;

    public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;
    public Animator camAnim;

    public float startTimeBtwShots;
    private float timeBtwShots;
    private Quaternion direction;

	private void Start()
	{
        direction = Quaternion.Euler(0f, 0f, offset);
	}

	private void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (shotEffect != null)
                    Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                //camAnim.SetTrigger("shake");
                float angle = offset + ((transform.lossyScale.x < 0) ? 180.0f : 0);
                direction = Quaternion.Euler(0f, 0f, angle);
                Instantiate(projectile, shotPoint.position, direction);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }

       
    }
}
