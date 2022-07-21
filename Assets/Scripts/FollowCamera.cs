using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position - new Vector3(0,0,10);
    }
}
