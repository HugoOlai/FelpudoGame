using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour {

	// Use this for initialization
	void bateEsquerda () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(-10, 0);
		GetComponent<Rigidbody2D> ().isKinematic = false;
		GetComponent<Rigidbody2D> ().AddTorque (-100f);
		Invoke ("destroi", 2f);

	}
	
	// Update is called once per frame
	void bateDireita () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (10, 0);
		GetComponent<Rigidbody2D> ().isKinematic = false;
		GetComponent<Rigidbody2D> ().AddTorque (100f);
		Invoke ("destroi", 2f);
	}

	void destroi()
	{
		Destroy (this.gameObject);
	}
}
