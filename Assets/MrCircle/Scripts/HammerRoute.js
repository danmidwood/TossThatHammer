#pragma strict

var speed : float = 1;
var camera11 : GameObject;
var thrown : boolean = false;

private var hammerRotateX : float = 0.2 * 40;
private var hammerRotateY : float = -5 * 40;
private var hammerRotateZ : float = 0 * 40;

private var hammerMoveX : float = 4.5 * 40;
private var hammerMoveY : float = 1 * 40;
private var hammerMoveZ : float = 0 * 40;

private var cameraRotateX : float = 0 * 40;
private var cameraRotateY : float = 0.05 * 40;
private var cameraRotateZ : float = 0 * 40;

private var cameraMoveX : float = 3.5 * 40;
private var cameraMoveY : float = 2.25 * 40;
private var cameraMoveZ : float = 0 * 40;

function Start () {

}

function Update () {
	if (thrown) {
		gameObject.transform.Rotate(hammerRotateX * Time.deltaTime, hammerRotateY  * Time.deltaTime, hammerRotateZ * Time.deltaTime); 
	 	gameObject.transform.position += Vector3(speed * hammerMoveX * Time.deltaTime, speed * hammerMoveY * Time.deltaTime, hammerMoveZ  * Time.deltaTime);
	 	
	 	speed = speed + 0.1;
	 	
	 	camera11.transform.position += Vector3(speed * cameraMoveX * Time.deltaTime, cameraMoveY * Time.deltaTime, cameraMoveZ * Time.deltaTime);	 	
	 	camera11.transform.Rotate (cameraRotateX * Time.deltaTime, cameraRotateY * Time.deltaTime, cameraRotateZ * Time.deltaTime);
 	}
 }

 function Throw() {
 	thrown = true;
 	gameObject.transform.parent = null;
 	gameObject.transform.position = Vector3(-30, 0, 0);
 }