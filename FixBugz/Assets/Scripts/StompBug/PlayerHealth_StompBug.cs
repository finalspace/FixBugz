using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_StompBug : SingletonBehaviour<PlayerHealth_StompBug> {

    public int health;
    public ScreenShake shake;

    public void UpdateHealth(int amount)
    {
        if (Player_StompBug.Instance.IsDead())
            return;
        
        health += amount;
        if (health <= 0)
            Dead();
    }

    private void Dead()
    {
        Player_StompBug.Instance.Dead();
        if (shake != null)
            shake.Shake(0.05f, 0.2f);
    }
}
