#pragma strict

private var counter : int = 0;
private var blinkSpeed : int = 20;




function Start () {
}

function Update () {
	if (counter > 150 && !gameObject.renderer.enabled)
	{
		Destroy(gameObject);
	}
	if (counter % blinkSpeed == 0){
		gameObject.renderer.enabled = !gameObject.renderer.enabled;
	}
	counter = counter + 1;

}