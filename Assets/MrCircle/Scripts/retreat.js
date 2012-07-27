#pragma strict

var counter : int = 0;




function Start () {
}

function Update () {
	if (counter > 50 && !gameObject.renderer.enabled)
	{
		Destroy(gameObject);
	}
	else if (counter > 30)
	{
		gameObject.transform.position += Vector3(0, 10, 0);
	}
	counter = counter + 1;

}