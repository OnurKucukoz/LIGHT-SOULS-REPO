using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset = new Vector3(0,5,-12);
    public GameObject player;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
