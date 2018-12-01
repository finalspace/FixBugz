using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : MonoBehaviour {

    public bool movingRight;
    public float startSpeed;

    private float speed;


    void Start()
    {
        speed = startSpeed * (movingRight ? 1 : -1);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        //Debug.Log(transform.localScale.x);
    }

    public void Init(bool movingRight, float movingSpeed)
	{
        this.movingRight = movingRight;
        this.startSpeed = movingSpeed;
        speed = startSpeed * (movingRight ? 1 : -1);

        Flip();
	}

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= (movingRight ? 1 : -1);
        transform.localScale = theScale;
    }


}
