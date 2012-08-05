#pragma strict

var touchLocationLabel : GUIText;
var touchLocationLabel2 : GUIText;
private var speed :int = 1;
private var touchCount :int = 0;
private var spinSpeed : int = 1;
private var winCriteria : int = 50;
private var spincrease : int = 2;
var spinner : GameObject;
var hammer : GameObject;

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

function dontMatch(x: float, z: float) {
	return false;
}



var zoneFunctions : Array = new Array();

var chosenZoneFunctionId : int = 0;

function nextZone() {
	chosenZoneFunctionId = (chosenZoneFunctionId + 1) % 4;
}

var currentZoneFunction = function() {
	return zoneFunctions[chosenZoneFunctionId];
};

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



function levelUp() {
	spinSpeed = spinSpeed + spincrease;
}

function Winned() {
	hammer.SetActiveRecursively(true);
	hammer.SendMessage("Throw");
    gameObject.SendMessage("Win");
	currentZoneFunction = function () { return dontMatch; };
	spincrease = 0.05;
	MutableUpdate = Spin;
}

var Spin = function() {	
	// gameObject.SendMessage("Point");
	spinner.transform.Rotate(0, 0 - spinSpeed, 0);
};



function Update(){
	MutableUpdate();
}

var MutableUpdate : Function = function (){

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
        	if ( inZone(gameObject, hit.point, currentZoneFunction())) {
        		nextZone();
        		Spin();
        		levelUp();
        		positionDebugText = positionDebugText + "\nSpin Speed: " + spinSpeed;
        		if (spinSpeed > winCriteria) {
        			touchLocationLabel.text = "\nSpin Speed: " + spinSpeed + "\nWin Criteria: " + winCriteria;
        			Winned();
        		}

        	}
         
        }

		touchLocationLabel.text = positionDebugText;

	 }	
        
};