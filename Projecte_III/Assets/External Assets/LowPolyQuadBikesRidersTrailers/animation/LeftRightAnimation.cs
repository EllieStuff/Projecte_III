using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightAnimation : MonoBehaviour {

	Animator animPlayer;
	// Use this for initialization
	void Start () {
		animPlayer = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			disableAllAnimation();
			animPlayer.SetBool("leftturn",true);
		}

		else if(Input.GetKeyDown(KeyCode.RightArrow)){
			disableAllAnimation();
			animPlayer.SetBool("rightturn",true);
		}

		if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
		{
			disableAllAnimation();
			animPlayer.SetBool("ideal",true);
		}
	}

	void OnGUI(){
	
//		GUI.Box
		GUI.Box(new Rect(0,0,250,50),"Press Left Arrow Key - Left Turn\nPress Right Arrow Key - Right Turn");
	}

	void disableAllAnimation(){
		animPlayer.SetBool("ideal",false);
		animPlayer.SetBool("rightturn",false);
		animPlayer.SetBool("leftturn",false);
	}


}
