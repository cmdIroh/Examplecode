using UnityEngine;
using System.Collections;

public class LeverScript : BinaryRotator, IInteractable{


	public GameObject[] _lights;
	public ParticleSystem _sparks;
	
	void Update () {	
	
		//Add event for state change

		base.Update();
		ControlLights ();
	}

	public void Interact(){
		//Display particle effect
		if(_sparks!=null)
			_sparks.Play ();
		//rotate the lever
		base.Interact ();
	}
	
	//Turn the lights controlled by this lever on or off
	void ControlLights(){
		if (!_enabled) {
			foreach (GameObject g in _lights) {
				g.GetComponent<Light> ().enabled = false;
			}
		} else {
			foreach (GameObject g in _lights) {
				g.GetComponent<Light> ().enabled = true;
			}
		}
	}
}
