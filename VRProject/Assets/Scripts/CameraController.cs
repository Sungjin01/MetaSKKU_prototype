using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Transform playertr;
    float h, v;
    float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        playertr = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(playertr.position.x, playertr.position.y + 1, playertr.position.z + 2);
        h = Input.GetAxis("Mouse X") * speed;
        v = Input.GetAxis("Mouse Y") * speed;
        //transform.Rotate(0, h, 0);
        
    }
}
