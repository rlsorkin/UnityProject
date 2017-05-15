using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCscript : MonoBehaviour {

    public bool isSatisfied;
    public bool isTalking;

    string[] npcStrings1 = new string[5];
    string[] npcStrings2 = new string[5];

    public GameObject npcCanvas;
    public GameObject Player;
    public GameObject rock;

    bool speechPause;
    bool firstContact;
    public bool ballGiven;
    public int whichLines = 0;
    public bool canGiven;

    void Start () {
        isTalking = false;
        npcCanvas = GameObject.Find("NPCcanvas");
        Player = GameObject.Find("Player");
        speechPause = false;
        firstContact = true;
        ballGiven = false;
        canGiven = false;

        npcStrings2[0] = "Hey, thanks!";
        npcStrings2[1] = "You probably wanna get past the rock, huh?";
        npcStrings2[2] = "I think Im strong enough now";
        npcStrings2[3] = "Let me break that for you";
        npcStrings2[4] = "";

        npcStrings1[0] = "Can you help me?";
        npcStrings1[1] = "Take this.";
        npcStrings1[2] = "Kill the Orb Spiders";
        npcStrings1[3] = "Don't worry they're mostly tame.";
        npcStrings1[4] = "One of em took something of mine";
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (Player.GetComponent<PlayerMove>().canisterGotten)
            {
                Debug.Log("NPC Got Can");
                canGiven = true;
            }

            if (firstContact && !canGiven)
            {
                StartCoroutine(nextLine(npcStrings1, 1));
                
            } else if(canGiven)
            {
                StartCoroutine(nextLine(npcStrings2, 2));
            }

            speechPause = false;
            isTalking = true;
            int count = 0;
            count++;
            Debug.Log("Activated" + " " + count + " " + canGiven);
            
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isTalking = false;
            speechPause = true;
            Debug.Log("Leaving NPC Box");
        }



    }

    IEnumerator nextLine(string[] currentLines, int sequence)
    {
        for (int i = 0; i <= 4; i++)
        {
            firstContact = false;

            while (speechPause)
            {
                yield return null;
            }

            if(i == 1)
            {
                GameObject tempProp = GameObject.Find("BubbleProp");
                Destroy(tempProp);
                ballGiven = true;

            }
            string thisText = currentLines[i];
            Text tempText = npcCanvas.GetComponentInChildren<Text>();
            tempText.text = thisText;
            Debug.Log("Trying...." + "Line" + i);
            if(i > 3 && sequence == 2)
            {
                Destroy(rock);
            }
            yield return new WaitForSeconds(2f);
        }
        
    }



}
