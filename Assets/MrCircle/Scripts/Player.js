#pragma strict

var touchLocationLabel : GUIText;
var touchLocationLabel2 : GUIText;
var spinner : GameObject;
var hammer : GameObject;

private var spinSpeed : int = 10;
private var spincrease : int = 40;
private var noOfSpinsToWin = 6;
private var touchesToWin = noOfSpinsToWin * 4;
private var noOfTouches = 0;
private var zoneFunctions : Array = new Array();
private var chosenZoneFunctionId : int = 0;

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
	noOfTouches = noOfTouches + 1;
}

function Winned() {
	hammer.SetActiveRecursively(true);
	hammer.SendMessage("Throw");
    gameObject.SendMessage("Win");
    MutableUpdate = function () { 
		spinSpeed = spinSpeed + (Time.deltaTime * 20 * spincrease);
	    Spin();
	};
	GameOver();
}

function GameOver() {
	currentZoneFunction = function () { return dontMatch; };
}

var Spin = function() {	
	spinner.transform.Rotate(0, Time.deltaTime * (0 - spinSpeed), 0);
};



function Update(){
	MutableUpdate();
}

var MutableUpdate : Function = function (){

	Spin();

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
			// Get movement of the finger since last frame
        	var touchDeltaPosition:Vector2 = Input.GetTouch(0).deltaPosition;
        
        	if ( inZone(gameObject, hit.point, currentZoneFunction())) {
        		nextZone();
        		levelUp();
        		if (noOfTouches > touchesToWin) {
        			Winned();
        		}

        	}
         
        }

		touchLocationLabel.text = positionDebugText;

	 }	
        
};