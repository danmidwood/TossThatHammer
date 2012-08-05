#pragma strict

private var counter : int = 0;
private var blinkSpeed : int = 10;




function Start () {
}

function Update () {
	gameObject.transform.Rotate(0, -1.5, 0); 
	if (counter > 200 && !gameObject.renderer.enabled)
	{
		Destroy(gameObject);
	}
	counter = counter + 1;

}