#pragma strict

var counter : int = 0;
var blinkSpeed : int = 20;




function Start () {
}

function Update () {
	if (counter > 200 && !gameObject.renderer.enabled)
	{
		Destroy(gameObject);
	}
	if (counter % blinkSpeed == 0){
		gameObject.renderer.enabled = !gameObject.renderer.enabled;
	}
	counter = counter + 1;

}