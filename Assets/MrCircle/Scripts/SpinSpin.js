#pragma strict

private var counter : float = 0.0;




function Start () {
}

function Update () {
	gameObject.transform.Rotate(0, -45 * Time.deltaTime, 0); 
	if (counter > 1 && !gameObject.renderer.enabled)
	{
		Destroy(gameObject);
	}
	counter = counter + Time.deltaTime;

}