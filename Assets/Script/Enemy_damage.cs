using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_damage : MonoBehaviour
{
    public int damage;  // Az ellenség sebzésének értéke
    public PlayerHealth playerHealth;  // A játékos egészségét kezelő komponens

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);  // Ha az ellenség érintkezik a "Player" objektummal, akkor a játékos egészsége csökken a sebzés értékével
        }   
    }
}
