#pragma strict


var points = 80;
var handicap = 0.05;
var hammer : GameObject;

function Start () {

}

function Update () {
	if (gameObject.transform.localScale.y > 5)
	{
		down(gameObject);
	}
}



function down(gameObject :GameObject) {
	gameObject.transform.localScale -= Vector3(0, handicap, 0); 
	gameObject.transform.position -= Vector3(0, 0, handicap);
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
		hammer.SetActiveRecursively(true);
		gameObject.SendMessage("Win");
	}
}