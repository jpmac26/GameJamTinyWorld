﻿using UnityEngine;
using System.Collections;

public class Gatherer : MonoBehaviour {

	public float speed = 2f;
	public float turnSpeed = 100f;
	private Rigidbody rigidbody;
	private GameObject owner;
	private GameObject target;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent <Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate () {
		if (target) {
			Vector3 offset = transform.InverseTransformPoint(target.transform.position);
			if(offset.x < 0.0) {
				//target is to the left
				//this.transform.Rotate(Vector3.up * -turnSpeed);
				this.rigidbody.AddRelativeTorque(0f, -turnSpeed, 0f);
			} else {
				//target is to the right
				//this.transform.Rotate(Vector3.up * turnSpeed);
				this.rigidbody.AddRelativeTorque(0f, turnSpeed, 0f);
			}
			if(Mathf.Abs(offset.x) < 10.0 && offset.z > 0.0) {
				//If we're facing mostly towards it and it's in front of us.
				this.rigidbody.AddRelativeForce(0f, 0f, speed);
			}
		} else {
			GameObject[] possibles = GameObject.FindGameObjectsWithTag("Resource");
			print (possibles);
			GameObject currentBest = null;
			foreach(GameObject test in possibles) {
				if (currentBest) {
					if(Vector3.Distance(test.transform.position, rigidbody.position) <
					   Vector3.Distance(currentBest.transform.position, rigidbody.position)) {
						currentBest = test;
					}
				} else {
					currentBest = test;
				}
			}
			target = currentBest;
		}
	}
}
