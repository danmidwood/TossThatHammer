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
	if ((counter < 40 || counter > 100) && counter % blinkSpeed == 0){
		gameObject.renderer.enabled = !gameObject.renderer.enabled;
	}
	counter = counter + 1;

}