using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float FollowSpeed = 2f; // A követés sebessége
    public float yOffset = 1f; // A kamera magassága a céltól
    public Transform target; // A cél, amit a kamera követ

    // Update is called once per frame
    void Update()
    {
        // Új pozíció kiszámítása a célpont és az eltolás alapján
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);

        // Követési animáció a jelenlegi pozíciótól az új pozícióig
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}