using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCam : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public GameObject child;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        child = player.transform.Find("CameraConstraint").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void follow()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, child.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(player.gameObject.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        follow();
    }
}
