﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicSource : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!(GetComponent<AudioSource>().isPlaying))
        {
            GetComponent<AudioSource>().Play();
        }
        else
        {
            Debug.Log("Something is wrong with Music.");
        }
    }
}