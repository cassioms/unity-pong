﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{

    private float playerVelocity = 240.0f;
    private bool canGoUp = true;
    private bool canGoDown = true;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameScript.isPaused)
        {
            if (Input.GetKey(KeyCode.UpArrow) && canGoUp)
            {
                player.transform.Translate(Vector3.back * playerVelocity * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow) && canGoDown)
            {
                player.transform.Translate(Vector3.forward * playerVelocity * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "RailUp")
        {
            canGoUp = false;
        }

        if (col.gameObject.name == "RailDown")
        {
            canGoDown = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "RailUp")
        {
            canGoUp = true;
        }

        if (col.gameObject.name == "RailDown")
        {
            canGoDown = true;
        }
    }
}
