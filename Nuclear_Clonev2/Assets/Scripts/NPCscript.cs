using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCscript : MonoBehaviour {

    public bool isSatisfied;
    public bool isTalking;
    string[] npcStrings = new string[5];
    public GameObject npcCanvas;
    bool speechPause;
    bool firstContact;
    public bool ballGiven;

    // Use this for initialization
    void Start () {
        isTalking = false;
        npcCanvas = GameObject.Find("NPCcanvas");
        speechPause = false;
        firstContact = true;
        ballGiven = false;

        npcStrings[0] = "Can you help me?";
        npcStrings[1] = "Take this.";
        npcStrings[2] = "Kill the Orb Spiders";
        npcStrings[3] = "Don't worry they're mostly tame.";
        npcStrings[4] = "One of em took something of mine";
    }
	

  
   void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (firstContact)
            {
                StartCoroutine(nextLine());
            }

            speechPause = false;
            isTalking = true;
            int count = 0;
            count++;
            Debug.Log("Activated" + " " + count + " " + isTalking);
            
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

    IEnumerator nextLine()
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
            string thisText = npcStrings[i];
            Text tempText = npcCanvas.GetComponentInChildren<Text>();
            tempText.text = thisText;
            Debug.Log("Trying...." + "Line" + i);
            yield return new WaitForSeconds(2f);
        }
    }



}
