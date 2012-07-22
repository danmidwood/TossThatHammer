#pragma strict

function Start () {

}

function Update () {


		// gameObject.transform.localScale += Vector3(1.5, 1);
		gameObject.transform.Rotate(0, 0.2, 0); 
	 	gameObject.transform.position += Vector3(2.5, 0.35, -1.25);
}