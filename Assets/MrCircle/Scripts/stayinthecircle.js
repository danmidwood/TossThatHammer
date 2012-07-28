#pragma strict

private var showTime : int = 50;
private var currentVisibleTime : int = 0;
private var blinks = 0;




function Start () {
	Reset();
}

function Update () {
	if (currentVisibleTime < showTime)
	{
		currentVisibleTime = currentVisibleTime + 1;
		if (blinks == 4)
		{
			gameObject.renderer.enabled = true;
		}
		else if (currentVisibleTime > (showTime / 6) && (currentVisibleTime % 10) == 0)
		{
			blinks = blinks + 1;
			gameObject.renderer.enabled = !gameObject.renderer.enabled;
		}

	}
	else
	{
		gameObject.SetActiveRecursively(false);
	}

	
	
}

function Reset()
{
	currentVisibleTime = 0;
	blinks = 0;
}

function Flash()
{
	gameObject.SetActiveRecursively(true);
	Reset();
}