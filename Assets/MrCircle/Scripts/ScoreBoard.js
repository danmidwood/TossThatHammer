#pragma strict



function Start () {

}

function Update () {
	if (gameObject.transform.localScale.y > 5)
	{
		down(gameObject);
	}
}



function down(gameObject :GameObject) {
	gameObject.transform.localScale -= Vector3(0,0.2,0); 
	gameObject.transform.position -= Vector3(0, 0,0.2);
}