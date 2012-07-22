#pragma strict

var touchLocationLabel : GUIText;
var touchLocationLabel2 : GUIText;
var speed = 1;
var touchCount = 0;

var scoreBoard: GameObject;

function Start () {

	zoneFunctions.push(inNorthEast);
	zoneFunctions.push(inNorthWest);
	zoneFunctions.push(inSouthWest);
	zoneFunctions.push(inSouthEast);

	
}

function inNorthWest(x: float, z:float) {
	return x < 0 && z > 0;
}

function inNorthEast(x: float, z:float) {
	return x > 0 && z > 0;
}

function inSouthWest(x: float, z:float) {
	return x < 0 && z < 0;
}

function inSouthEast(x: float, z:float) {
	return x > 0 && z < 0;
}



var zoneFunctions : Array = new Array();

var chosenZoneFunctionId : int = 0;

function nextZone() {
	chosenZoneFunctionId = (chosenZoneFunctionId + 1) % 4;
}

function currentZoneFunction() {
	return zoneFunctions[chosenZoneFunctionId];
}

function inZone(gameObject : GameObject, touchPosition : Vector3, criteria: Function )
{
	var worldPosition = touchPositionToWorldLocation(touchPosition);	
	var objectCenter = gameObject.renderer.bounds.center;
	
	var pointConvertor = gameObject.renderer.worldToLocalMatrix;

	var circleX = worldPosition.x - objectCenter.x;
	var circleY = worldPosition.y - objectCenter.y;
	var circleZ = worldPosition.z - objectCenter.z;

	var positionDebugText = "World Touch: " + worldPosition.x + ", " + worldPosition.y + ", " + worldPosition.z;
	positionDebugText = positionDebugText + "\nCircle Touch: " + circleX + ", " + circleY+ ", " + circleZ;
	touchLocationLabel2.text = positionDebugText;
	return criteria(circleX, circleZ);
}


function touchPositionToWorldLocation(position: Vector3) {
	return Vector3(position.x, position.y, position.z);
}



function up(gameObject :GameObject) {
		gameObject.SendMessage("up");
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

        	if ( inZone(hit.collider.gameObject, hit.point, currentZoneFunction())) {
        		nextZone();
        		up(scoreBoard);
        	}
         
        }

		touchLocationLabel.text = positionDebugText;

	 }	
        
}