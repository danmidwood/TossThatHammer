#pragma strict

function Start () {

}

function Update () {
		gameObject.transform.Rotate(0.2, 5, 0); 
	 	gameObject.transform.position += Vector3(3, 5, 0);
}