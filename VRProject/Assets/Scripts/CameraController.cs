using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Transform playertr;
    // Start is called before the first frame update
    void Start()
    {
        playertr = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
