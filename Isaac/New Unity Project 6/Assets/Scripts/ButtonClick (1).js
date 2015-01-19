#pragma strict
var wasClicked : boolean;
var Door : GameObject;

function Start () 
{

}

function Update () 
{

}

 
function OnMouseDown() {
    wasClicked = true;
    Activate();
}
 
function OnMouseUp() {
   wasClicked = false;
   Deactivate();
}
 
function OnMouseEnter() {
    if (wasClicked) {
        Activate();
    }
}
 
function OnMouseExit() {
    Deactivate();
}
 
function Activate() {

    renderer.material.color = Color.red;
    //door.animation.play("animation name here")
    Door.animation.Play("DoorOpening");

}
 
function Deactivate() {
    renderer.material.color = Color.white;
}