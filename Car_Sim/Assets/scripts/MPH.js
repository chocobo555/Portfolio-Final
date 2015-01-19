#pragma strict

var mphText : GUIText;


function Start () {

}

function Update () {

var mph = rigidbody.velocity.magnitude*2.237;

mphText.text = mph + " MPH";



}