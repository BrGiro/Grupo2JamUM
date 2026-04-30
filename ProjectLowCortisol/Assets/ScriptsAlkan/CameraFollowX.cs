using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform player; // Drag player object here in Inspector

    void LateUpdate()
    {
        if (player != null)
        {
            // Create a new position using player's X, but camera's current Y and Z
            transform.position = new Vector3(player.position.x + 7, transform.position.y, transform.position.z);
        }
    }
}
