using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMove : MonoBehaviour {

    public bool movingRight;
    public float startSpeed;
    public float height;
    public float waveSpeed;
    public float waveStrongness;

    private float speed;


	void Start()
    {
        speed = startSpeed * (movingRight ? 1 : -1);
        Flip();
    }


    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, height + Mathf.Sin(Time.time * waveSpeed) * waveStrongness, transform.position.z);
    }

    public void Init(bool movingRight, float movingSpeed, float height, float waveSpeed, float waveStrongness)
    {
        this.movingRight = movingRight;
        this.startSpeed = movingSpeed;
        this.height = height;
        this.waveSpeed = waveSpeed > 0 ? waveSpeed : this.waveSpeed;
        this.waveStrongness = waveStrongness > 0 ? waveStrongness : this.waveStrongness;
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
