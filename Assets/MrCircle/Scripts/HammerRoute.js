#pragma strict

var speed : float = 1;
var camera11 : GameObject;
var thrown : boolean = false;


function Start () {

}

function Update () {
	if (thrown) {
		gameObject.transform.Rotate(0.2, -5, 0); 
	 	gameObject.transform.position += Vector3(speed * 2.5, speed * 1, 0);
	 	speed = speed + 0.1;
	 	camera11.transform.position += Vector3(speed * 1.5, 2.25, 0);	 	
	 	camera11.transform.Rotate (0,0.05,0);
 	}
 }

 function Throw() {
 	thrown = true;
 	gameObject.transform.parent = null;
 	gameObject.transform.position = Vector3(-10, 0, 0);
 }