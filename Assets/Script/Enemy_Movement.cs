using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public Transform[] patrolPoints;  // A járőr pontokat tartalmazó tömb
    public float moveSpeed;  // Az ellenség mozgási sebessége
    public int patrolDestination;  // Az aktuális járőr pont indexe

    // Update is called once per frame
    void Update()
    {
        if (patrolDestination == 0)
        {
            // Az ellenség pozíciójának mozgatása a következő járőr pont felé
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);

            // Ha az ellenség elérte az első járőr pontot
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f)
            {
                transform.localScale = new Vector3(1, 1, 1);  // Az ellenség irányának beállítása
                patrolDestination = 1;  // Az aktuális járőr pont indexének frissítése a következő pontra
            }
        }

        if (patrolDestination == 1)
        {
            // Az ellenség pozíciójának mozgatása a következő járőr pont felé
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);

            // Ha az ellenség elérte a második járőr pontot
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f)
            {
                transform.localScale = new Vector3(-1, 1, 1);  // Az ellenség irányának beállítása
                patrolDestination = 0;  // Az aktuális járőr pont indexének frissítése a következő pontra
            }
        }
    }
}







