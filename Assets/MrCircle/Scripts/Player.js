#pragma strict

var touchLocationLabel : GUIText;
var touchLocationLabel2 : GUIText;
var speed = 1;
var touchCount = 0;

var scoreBoard: GameObject;

function Start () {

	var circle = GameObject.Find("base");

	Debug.Log(circle.renderer.bounds);
}

function inNorthWest(gameObject : GameObject, touchPosition : Vector3)
{
	var worldPosition = touchPositionToWorldLocation(touchPosition);	
	var objectCenter = gameObject.renderer.bounds.center;
	
	var pointConvertor = gameObject.renderer.worldToLocalMatrix;
	
    // var localCenter = pointConvertor.MultiplyPoint(worldCenter);
	// var localPosition = pointConvertor.MultiplyPoint(worldPosition);

	var circleX = worldPosition.x - objectCenter.x;
	var circleY = worldPosition.y - objectCenter.y;
	var circleZ = worldPosition.z - objectCenter.z;
	// var localPosition = Camera.main.ScreenToWorldPoint(touchPosition);
	// Debug.Log("Local Center is " + localCenter);
	// Debug.Log("Local position is " + localPosition);

	var positionDebugText = "World Touch: " + worldPosition.x + ", " + worldPosition.y + ", " + worldPosition.z;
	positionDebugText = positionDebugText + "\nCircle Touch: " + circleX + ", " + circleY+ ", " + circleZ;
	touchLocationLabel2.text = positionDebugText;
	return circleX < 0 && circleZ > 0;
	//  && worldPosition.z > worldCenter.z
}


function touchPositionToWorldLocation(position: Vector3) {
	return Vector3(position.x, position.y, position.z);
}



function up(gameObject :GameObject) {
	var points = 1;
	if (gameObject.transform.localScale.y < 100)
	{
		gameObject.transform.localScale += Vector3(0, points, 0); 
	 	gameObject.transform.position += Vector3(0, 0, points);
	}
}


function Update () {
	if (Input.touchCount > 0 && 
      (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)) {
    
        

        var touch = Input.GetTouch(0);

        var pos = touch.position;        

		var positionDebugText = "Touch:     " + pos.x + ", " + pos.y;
		positionDebugText = positionDebugText + "\nMouse:     " + Input.mousePosition.x + ", " + Input.mousePosition.y;

		// Input.mousePosition.x
		var ray = Camera.main.ScreenPointToRay(pos); 
		
		var hit : RaycastHit;
		if (Physics.Raycast (ray, hit)) {
			touchCount = touchCount + 1;

			positionDebugText = positionDebugText + "\nTouch Count: " + touchCount;
			positionDebugText = positionDebugText + "\nHit: " + hit.collider.gameObject.name;

		      // Get movement of the finger since last frame
        	var touchDeltaPosition:Vector2 = Input.GetTouch(0).deltaPosition;
        
        	// Move object across XY plane
        	// transform.Translate (touchDeltaPosition.x * speed, 0, touchDeltaPosition.y * speed);	

        	if ( inNorthWest(hit.collider.gameObject, hit.point)) {
        		up(scoreBoard);
        	}
         
        }

		touchLocationLabel.text = positionDebugText;

	 }	
        
}