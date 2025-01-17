﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour
{

	public float rotationSpeed;
	public float walkSpeed;
	public float runSpeed;
    public float startWater;
	public static float initialWater;
	//current water:
	public static float water;
	//private float water;



	public GameObject winImage;
	public Animator	animatorController;
	public Scrollbar waterBar;
	public GameObject waterBarImage;

    //private Rigidbody rb;
	private CharacterController characterController;
	private Quaternion targetRotation;




    void Start()
    {
		initialWater = startWater;
		water = initialWater;

		//waterBarImage.position = new Vector3(0,0,0);

		//rb = GetComponent<Rigidbody>();
		characterController = GetComponent<CharacterController>();
		winImage.GetComponent<RawImage>().enabled = false;
		//waterBarImage.GetComponent<Image>().transform.position = new Vector3(332,31,0);
		animatorController = GetComponent<Animator> ();
		//waterBar = GetComponent<Scrollbar> ();
		waterBar.size = 1;
    }

    void FixedUpdate()
    {
		Vector3 input = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));


		if (input != Vector3.zero) {
			targetRotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

			animatorController.SetBool ("isWalking", true);


			if (Input.GetButton ("Run") && water > 0) {

				water -= 1;
				waterBar.size = (float)water / initialWater;
				print (water.ToString ());

				animatorController.SetBool ("isRunning", true);


			}
			else 
			{
				animatorController.SetBool ("isRunning", false);
			}

			if (Input.GetButton ("Crawl")) {
				animatorController.SetBool ("isCrawling", true);
			} else 
			{
				animatorController.SetBool ("isCrawling", false);
			}
		

		
		} else 
		{
			animatorController.SetBool ("isWalking", false);
			animatorController.SetBool ("isRunning", false);
			animatorController.SetBool ("isCrawling", false);
		}



		float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = input;

		movement *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.z) == 1) ? .7f : 1;
		if (Input.GetButton ("Run") && water > 0) {
			movement *= runSpeed;
		} else 
		{
			movement *= walkSpeed;
		}
		//movement *= () ? runSpeed : walkSpeed;


		movement += Vector3.up * -8;

		characterController.Move (movement*Time.deltaTime);

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //rb.AddForce(movement * walkSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
		{	
			print ("YOU ESCAPED!!"); 
			winImage.GetComponent<RawImage>().enabled = true;
            //SceneManager.LoadScene("levelname");
        }
    }

    void OnTriggerStay (Collider other)
    {

    }
}