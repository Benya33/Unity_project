using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;  // A maximális egészség értéke
    public int health;  // Az aktuális egészség értéke

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;  // Az egészség értékének beállítása a maximális értékre kezdetben
    }

    public void TakeDamage(int damage)
    {
        health -= damage;  // Az egészség csökkentése a kapott sebzés értékével

        if (health <= 0)
        {
            Destroy(gameObject);  // Ha az egészség értéke 0 vagy annál kisebb, a játékos objektum megsemmisül
        }
    }
}
