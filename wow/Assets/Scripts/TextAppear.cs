using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextAppear : MonoBehaviour
{

    public GameObject UiObject;
    // Start is called before the first frame update
    void Start()
    {
        UiObject.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            UiObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider player)
    {
        UiObject.SetActive(false);
    }

}
