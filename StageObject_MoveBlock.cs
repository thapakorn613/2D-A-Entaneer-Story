using UnityEngine;
using System.Collections;

public class StageObject_MoveBlock : MonoBehaviour {

	public Vector3 	velocityA 		= new Vector3( 1.0f,0.0f,0.0f);
	public Vector3 	velocityB 		= new Vector3(-1.0f,0.0f,0.0f);
	public float 	switchingTime 	= 5.0f; 
	public float	vt 				= 0;

	bool	turn		= false;
	float 	changeTime	= 0.0f;

	void FixedUpdate () {// เน้นเรื่องของ Physics 
		if (changeTime <= 0.0f) {
			changeTime 			 = Time.fixedTime;
			GetComponent<Rigidbody2D>().velocity = velocityA;
		}

		if (Time.fixedTime + vt > changeTime + switchingTime) {
			GetComponent<Rigidbody2D>().velocity = turn ? velocityA : velocityB;
			turn 				 = turn ? false : true;
			changeTime  = Time.fixedTime;
		}
	}

    //private void LateUpdate() // เป็นตัวอัพเดทครั้งสุดท้าย
    //{
        
    //}
}
