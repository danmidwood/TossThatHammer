#pragma strict

var speed = 1;

function Start () {

}

function Update () {
	if (Input.touchCount > 0 && 
      Input.GetTouch(0).phase == TouchPhase.Moved) {
    
  

        var touch = Input.GetTouch(0);

        var pos = touch.position;
		Debug.Log(pos);
        

		var ray = Camera.main.ScreenPointToRay(pos); 
		
		var hit : RaycastHit;
		if (Physics.Raycast (ray, hit)) {
			Debug.Log("hit: " + hit.collider.gameObject.name);
		      // Get movement of the finger since last frame
        	var touchDeltaPosition:Vector2 = Input.GetTouch(0).deltaPosition;
        
        	// Move object across XY plane
        	transform.Translate (touchDeltaPosition.x * speed, 0, touchDeltaPosition.y * speed);	
         
        }

	 }	
        
}