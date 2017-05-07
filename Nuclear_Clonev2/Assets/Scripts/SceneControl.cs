using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour {

    public GameObject npcCanvas;
    public GameObject player;
    public GameObject npc;
    public Text textObj;
    public int currentPos = 0;
    public string currentText;
    string[] npcStrings = new string[5];
    bool npcTalking;

    // Use this for initialization
    void Start () {

        player = GameObject.Find("Player");
        npcCanvas = GameObject.Find("NPCcanvas");
        npc = GameObject.Find("purpleMysteryGuy");
        textObj = npcCanvas.GetComponentInChildren<Text>();
        npcCanvas.gameObject.SetActive(false);
        
        npcStrings[0] = "Can you help me?";
        npcStrings[1] = "Take this.";
        npcStrings[2] = "Kill the Orb Spiders";
        npcStrings[3] = "Hey, thanks.";
        npcStrings[4] = "I can break that rock now";

    }

    // Update is called once per frame
    void Update () {
        npcTalking = npc.GetComponent<NPCscript>().isTalking;
        bool npcBall = npc.GetComponent<NPCscript>().ballGiven;
        if (npcBall)
        {
            player.GetComponent<PlayerMove>().ballGotten = true;
            Debug.Log("Ball Handoff");
        }

        if (npcTalking)
        {
            npcCanvas.SetActive(true);
            //textObj.text = "This Sucks!";
            //StartCoroutine(nextLine());
        } else if (!npcTalking)
        {
            npcCanvas.SetActive(false);
        }

    }

    IEnumerator nextLine()
    {
        for (int i = 0; i <= 4; i++) {
            string thisText = npcStrings[i];
            Text tempText = npcCanvas.GetComponentInChildren<Text>();
            tempText.text = thisText;
            Debug.Log("Trying............");
            yield return new WaitForSeconds(2f);
        }
    }
}
