#pragma strict


var points = 1;

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

function up()
{
	if (gameObject.transform.localScale.y < 50)	{
		gameObject.transform.localScale += Vector3(0, points, 0); 
	 	gameObject.transform.position += Vector3(0, 0, points);
	 	gameObject.SendMessage("Point");
	}
	else
	{
		gameObject.SendMessage("Win");
	}
}