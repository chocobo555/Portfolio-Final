var renderer, camera;
var scene, element;
var ambient, point;
var aspectRatio, windowHalf;
var mouse, time;

var controls;

var useRift = false;

var riftCam;

var ground, groundGeometry, groundMaterial;
var box1;
var box2;
var box3;
var box4;
var cyl1;
var cyl2;
var cyl3;
var cyl4;
var cyl5;
var skyBox;
var floor;
var boxColor;
var spheres = [];
var spheresY = [];
var lastPositionY;

var freq = [];
var lowFreqAverage;
var midFreqAverage;
var highFreqAverage;


var particleCount = 1800, 
particles = new THREE.Geometry(),
pMaterial = new THREE.ParticleBasicMaterial({ color: 0xFFFFFF, size: 20 });

var particleSystem = new THREE.ParticleSystem(particles, pMaterial);


var bodyAngle;
var bodyAxis;
var bodyPosition;
var viewAngle;

var velocity;
var oculusBridge;


// Map for key states
var keys = [];
for(var i = 0; i < 130; i++)
{
  keys.push(false);
}


//****************************************************************************
if (! window.AudioContext) 
{
        if (! window.webkitAudioContext) 
		{
            alert('no audiocontext found');
        }
        window.AudioContext = window.webkitAudioContext;
}
    var context = new AudioContext();
    var audioBuffer;
    var sourceNode;
    var splitter;
    var analyser, analyser2;
    var javascriptNode;


    // load the sound
    setupAudioNodes();
    //loadSound("Heyeayea.wav");
	//loadSound("StuckInADaze.wav");
	loadSound("Flicker.wav");


    function setupAudioNodes() 
	{

        // setup a javascript node
        javascriptNode = context.createScriptProcessor(2048, 1, 1);
        // connect to destination, else it isn't called
        javascriptNode.connect(context.destination);


        // setup a analyzer
        analyser = context.createAnalyser();
        analyser.smoothingTimeConstant = 0.3;
        analyser.fftSize = 1024;

        analyser2 = context.createAnalyser();
        analyser2.smoothingTimeConstant = 0.0;
        analyser2.fftSize = 1024;

        // create a buffer source node
        sourceNode = context.createBufferSource();
        splitter = context.createChannelSplitter();

        // connect the source to the analyser and the splitter
        sourceNode.connect(splitter);

        // connect one of the outputs from the splitter to
        // the analyser
        splitter.connect(analyser,0,0);
        splitter.connect(analyser2,1,0);

        // connect the splitter to the javascriptnode
        // we use the javascript node to draw at a
        // specific interval.
        analyser.connect(javascriptNode);

//        splitter.connect(context.destination,0,0);
//        splitter.connect(context.destination,0,1);

        // and connect to destination
        sourceNode.connect(context.destination);
    }

    // load the specified sound
    function loadSound(url) 
	{
        var request = new XMLHttpRequest();
        request.open('GET', url, true);
        request.responseType = 'arraybuffer';

        // When loaded decode the data
        request.onload = function() 
		{

            // decode the data
            context.decodeAudioData(request.response, function(buffer) 
			{
                // when the audio is decoded play the sound
                playSound(buffer);
            }, onError);
        }
        request.send();
    }


    function playSound(buffer) 
	{
        sourceNode.buffer = buffer;
        sourceNode.start(0);
    }

    // log if an error occurs
    function onError(e) 
	{
        //console.log(e);
    }


    var average;
    // when the javascript node is called
    // we use information from the analyzer node
    // to draw the volume
	
	
	var array =  new Uint8Array(analyser.frequencyBinCount);//******************CHANGE
    javascriptNode.onaudioprocess = function() 
	{

        // get the average for the first channel
        //var array =  new Uint8Array(analyser.frequencyBinCount);
        analyser.getByteFrequencyData(array);
        average = getAverageVolume(array);

        // get the average for the second channel
        var array2 =  new Uint8Array(analyser2.frequencyBinCount);
        analyser2.getByteFrequencyData(array2);
        var average2 = getAverageVolume(array2);
        
    }

    function getAverageVolume(array) 
	{
        var values = 0;
        var average;

        var length = array.length;

        // get all the frequency amplitudes
        for (var i = 0; i < length; i++) 
		{
            values += array[i];
        }
		
		freq = array;
		//console.log(freq);

        average = values / length;
        
        document.getElementById("blah").innerHTML = average;
        
        
        return average;
    
    }
	
//****************************************************************************


function initScene() 
{
  mouse = new THREE.Vector2(0, 0);

  windowHalf = new THREE.Vector2(window.innerWidth / 2, window.innerHeight / 2);
  aspectRatio = window.innerWidth / window.innerHeight;
  
  scene = new THREE.Scene();

  camera = new THREE.PerspectiveCamera(45, aspectRatio, 1, 10000);
  camera.useQuaternion = true;

  camera.position.set(100, 150, 300);
  camera.lookAt(scene.position);
  
  
  var materialArray = [];
    materialArray.push(new THREE.MeshBasicMaterial({
        map: THREE.ImageUtils.loadTexture('cliffsofinsanity_right.jpg')
    }));
    materialArray.push(new THREE.MeshBasicMaterial({
        map: THREE.ImageUtils.loadTexture('cliffsofinsanity_left.jpg')
    }));
    materialArray.push(new THREE.MeshBasicMaterial({
        map: THREE.ImageUtils.loadTexture('cliffsofinsanity_top.jpg')
    }));
    materialArray.push(new THREE.MeshBasicMaterial({
        map: THREE.ImageUtils.loadTexture('cliffsofinsanity_top.jpg')
    }));
    materialArray.push(new THREE.MeshBasicMaterial({
        map: THREE.ImageUtils.loadTexture('cliffsofinsanity_front.jpg')
    }));
    materialArray.push(new THREE.MeshBasicMaterial({
        map: THREE.ImageUtils.loadTexture('cliffsofinsanity_back.jpg')
    }));
    for (var i = 0; i < 6; i++) materialArray[i].side = THREE.DoubleSide;
    var skyboxMaterial = new THREE.MeshFaceMaterial(materialArray);
    var skyboxGeom = new THREE.CubeGeometry(8000, 8000, 8000, 1, 1, 1);
    var skybox = new THREE.Mesh(skyboxGeom, skyboxMaterial);
    scene.add(skybox);
	

  // Initialize the renderer
  renderer = new THREE.WebGLRenderer({antialias:true});
  renderer.setClearColor(0xdbf7ff);
  renderer.setSize(window.innerWidth, window.innerHeight);

  element = document.getElementById('viewport');
  element.appendChild(renderer.domElement);

  controls = new THREE.OrbitControls(camera);
  
  
  //******************************************************PARTICLES***********************

for(var p = 0; p < particleCount; p++)
{
	var pX = Math.random() * 3000 - 1250,
		pY = Math.random() * 3000 - 1250,
		pZ = Math.random() * 3000 - 1250,
		particle = new THREE.Vector3(pX, pY, pZ);
		
	particle.velocity = new THREE.Vector3(0, -Math.random(), 0);
		
	particles.vertices.push(particle);
}

scene.add(particleSystem);



//*******************************************************PARTICLES******************************
  
}


function initLights()
{

  ambient = new THREE.AmbientLight(0x404040);
  scene.add(ambient);

  point1 = new THREE.DirectionalLight( 0xffffff, 0.01, 0.5, Math.PI, 1 );
		//point.position.set( -250, 250, 150 );
  point1.position.set( 100, 100, 0 );
  
  point2 = new THREE.DirectionalLight( 0xffffff, 0.01, 0.5, Math.PI, 1 );
  point2.position.set( -100, 100, 0 );
  
  scene.add(point1);
  scene.add(point2);
}


var floorColor;

function initGeometry()
{
  var floorColor = new THREE.Color("rgb(50, 50, 50)"); // 255 is max
  var floorMaterial = new THREE.MeshBasicMaterial( { color: floorColor } );
  var floorGeometry = new THREE.PlaneGeometry(30000, 30000, 10, 10);
  floor = new THREE.Mesh(floorGeometry, floorMaterial);
  floor.rotation.x = -Math.PI / 2;

  scene.add(floor);

	boxColor = new THREE.Color(80, 80, 80); // 255 is max
	
    var material1 = new THREE.MeshLambertMaterial( {color: boxColor} );
	var material2 = new THREE.MeshLambertMaterial( {color: boxColor} );
	var material3 = new THREE.MeshLambertMaterial( {color: boxColor} );
	var material4 = new THREE.MeshLambertMaterial( {color: boxColor} );
	var material5 = new THREE.MeshLambertMaterial( {color: boxColor} );
	var material6 = new THREE.MeshLambertMaterial( {color: boxColor} );
	var material7 = new THREE.MeshLambertMaterial( {color: boxColor} );
	var material8 = new THREE.MeshLambertMaterial( {color: boxColor} );
	var material9 = new THREE.MeshLambertMaterial( {color: boxColor} );
	

	var material10 = new THREE.MeshLambertMaterial( {color: boxColor} );
	/*
	skyBoxColor = new THREE.Color(255, 255, 255); // 255 is max
	var skyBoxMaterial = new THREE.MeshLambertMaterial( {color: skyBoxColor} );
	skyBox = new THREE.Mesh(new THREE.CubeGeometry(1000000, 100, 1000000), skyBoxMaterial);
	skyBox.position.set(0, 1000, 0);
	*/
	
    var height1 = 60;
    var width1 = 60;
	
    box1 = new THREE.Mesh( new THREE.CubeGeometry(width1, height1, width1), material1);
    box1.position.set(-450, 100, 450);
    box1.rotation.set(0, 2 * Math.PI * 2, 0);
	box1.geometry.dynamic = true;
	
	box2 = new THREE.Mesh( new THREE.CubeGeometry(width1, height1, width1), material2);
    box2.position.set(450, 100, -450);
    box2.rotation.set(0, 2 * Math.PI * 2, 0);
	
	box3 = new THREE.Mesh( new THREE.CubeGeometry(width1, height1, width1), material3);
    box3.position.set(-450, 100, -450);
    box3.rotation.set(0, 2 * Math.PI * 2, 0);
	
	box4 = new THREE.Mesh( new THREE.CubeGeometry(width1, height1, width1), material4);
    box4.position.set(450, 100, 450);
    box4.rotation.set(0, 2 * Math.PI * 2, 0);
	
	cyl1 = new THREE.Mesh( new THREE.CylinderGeometry(20, 20, height1, 7, 1, false), material5);//*******player spawn location
    cyl1.position.set(0, 100, 0);
    cyl1.rotation.set(0, 2 * Math.PI * 2, 0);
	
	cyl2 = new THREE.Mesh( new THREE.CylinderGeometry(20, 20, height1, 3, 1, false), material6);
    cyl2.position.set(0, 100, -430);
    cyl2.rotation.set(0, 2 * Math.PI * 2, 0);
	
	cyl3 = new THREE.Mesh( new THREE.CylinderGeometry(20, 20, height1, 3, 1, false), material7);
    cyl3.position.set(0, 100, 430);
    cyl3.rotation.set(0, 2 * Math.PI * 2, 0);
	
	cyl4 = new THREE.Mesh( new THREE.CylinderGeometry(20, 20, height1, 3, 1, false), material8);
    cyl4.position.set(430, 100, 0);
    cyl4.rotation.set(0, 2 * Math.PI * 2, 0);
	
	cyl5 = new THREE.Mesh( new THREE.CylinderGeometry(20, 20, height1, 3, 1, false), material9);
    cyl5.position.set(-430, 100, 0);
    cyl5.rotation.set(0, 2 * Math.PI * 2, 0);
	
	
	for(var i = 0; i < 500; i++)
	{
		var sphere = new THREE.Mesh( new THREE.SphereGeometry(20, 40, 40), material10);
		sphere.position.set(((Math.random() * 3300) - 1500), ((Math.random() * 600) - 150), ((Math.random() * 3300) - 1500));
		sphere.rotation.set(0, Math.random() * Math.PI * 2, 0);
		spheres.push(sphere);
		spheresY.push(sphere.position.y);
		scene.add(sphere);
	}
	
    scene.add(box1);
	scene.add(box2);
	scene.add(box3);
	scene.add(box4);
	scene.add(cyl1);
	scene.add(cyl2);
	scene.add(cyl3);
	scene.add(cyl4);
	scene.add(cyl5);
	
	//scene.add(skyBox);
}


function init()
{
	  document.addEventListener('keydown', onKeyDown, false);
	  document.addEventListener('keyup', onKeyUp, false);
	  document.addEventListener('mousemove', onMouseMove, false);

	  document.getElementById("toggle-render").addEventListener("click", function()
	  {
		useRift = !useRift;
		onResize();
	  });

	  window.addEventListener('resize', onResize, false);

	  time          = Date.now();
	  bodyAngle     = 0;
	  bodyAxis      = new THREE.Vector3(0, 1, 0);
	  bodyPosition  = new THREE.Vector3(0, 15, 0);
	  velocity      = new THREE.Vector3();

	  initScene();
	  initGeometry();
	  initLights();
	  
	  oculusBridge = new OculusBridge
	  ({
		"debug" : true,
		"onOrientationUpdate" : bridgeOrientationUpdated,
		"onConfigUpdate"      : bridgeConfigUpdated,
	  });
	  oculusBridge.connect();

	  riftCam = new THREE.OculusRiftEffect(renderer);

}


function onResize() 
{
  if(!useRift)
  {
    windowHalf = new THREE.Vector2(window.innerWidth / 2, window.innerHeight / 2);
    aspectRatio = window.innerWidth / window.innerHeight;
   
    camera.aspect = aspectRatio;
    camera.updateProjectionMatrix();
   
    renderer.setSize(window.innerWidth, window.innerHeight);
  } 
  else 
  {
    riftCam.setSize(window.innerWidth, window.innerHeight);
  }
}


function bridgeConfigUpdated(config)
{
  console.log("Oculus config updated.");
  riftCam.setHMD(config);      
}

function bridgeOrientationUpdated(quatValues) 
{
  var quat = new THREE.Quaternion();
  quat.setFromAxisAngle(bodyAxis, bodyAngle);

  // make a quaternion for the current orientation of the Rift
  var quatCam = new THREE.Quaternion(quatValues.x, quatValues.y, quatValues.z, quatValues.w);

  // multiply the body rotation by the Rift rotation.
  quat.multiply(quatCam);


  // Make a vector pointing along the Z axis and rotate it accoring to the combined look/body angle.
  var xzVector = new THREE.Vector3(0, 0, 1);
  xzVector.applyQuaternion(quat);

  // Compute the X/Z angle based on the combined look/body angle.  This will be used for FPS style movement controls
  // so you can steer with a combination of the keyboard and by moving your head.
  viewAngle = Math.atan2(xzVector.z, xzVector.x) + Math.PI;

  // Apply the combined look/body angle to the camera.
  camera.quaternion.copy(quat);
}


function onMouseMove(event) 
{
  mouse.set( (event.clientX / window.innerWidth - 0.5) * 2, (event.clientY / window.innerHeight - 0.5) * 2);
}


function onKeyDown(event) 
{

  if(event.keyCode == 48)
  { // zero key.
    useRift = !useRift;
    onResize();
  }

  // prevent repeat keystrokes.
  if(!keys[32] && (event.keyCode == 32))
  { 
    velocity.y += 1.9;
  }

  keys[event.keyCode] = true;
}


function onKeyUp(event) 
{
  keys[event.keyCode] = false;
}


function updateInput(delta) 
{
  
  var step        = 25 * delta;
  var turn_speed  = (55 * delta) * Math.PI / 180;


  // Forward/backward

  if(keys[87] || keys[38])
  { // W or UP
      bodyPosition.x += Math.cos(viewAngle) * step;
      bodyPosition.z += Math.sin(viewAngle) * step;
  }

  if(keys[83] || keys[40])
  { // S or DOWN
      bodyPosition.x -= Math.cos(viewAngle) * step;
      bodyPosition.z -= Math.sin(viewAngle) * step;
  }

  // Turn

  if(keys[81])
  { // E
      bodyAngle += turn_speed;
  }   
  
  if(keys[69])
  { // Q
       bodyAngle -= turn_speed;
  }

  // Straif

  if(keys[65] || keys[37])
  { // A or LEFT
      bodyPosition.x -= Math.cos(viewAngle + Math.PI/2) * step;
      bodyPosition.z -= Math.sin(viewAngle + Math.PI/2) * step;
  }   
  
  if(keys[68] || keys[39])
  { // D or RIGHT
      bodyPosition.x += Math.cos(viewAngle+Math.PI/2) * step;
      bodyPosition.z += Math.sin(viewAngle+Math.PI/2) * step;
  }
  

  // VERY simple gravity/ground plane physics for jumping.
  
  velocity.y -= 0.15;
  
  bodyPosition.y += velocity.y;
  
  if(bodyPosition.y < 15)
  {
    velocity.y *= -0.12;
    bodyPosition.y = 15;
  }

  // update the camera position when rendering to the oculus rift.
  if(useRift) 
  {
    camera.position.set(bodyPosition.x, bodyPosition.y, bodyPosition.z);
  }
}


var rotObjectMatrix;
function rotateAroundObjectAxis(object, axis, radians)
{
	rotObjectMatrix = new THREE.Matrix4();
	rotObjectMatrix.makeRotationAxis(axis.normalize(), radians);
	
	object.matrix.multiply(rotObjectMatrix);
	object.rotation.setFromRotationMatrix(object.matrix);
}

var xAxis = new THREE.Vector3(1,0,0);


function animate() 
{
	//rotateAroundObjectAxis(box1, xAxis, Math.PI / 180); DON'T DO THIS AGAIN!!!!
	lowFreqAverage = 0.0;
	midFreqAverage = 0.0;
	highFreqAverage = 0.0;
	
	
	for(var i = 0; i < freq.length; i++)
	{
		if(i >= 50 && i < 100)
		{
			lowFreqAverage += freq[i];
		}
		else if(i >= 200 && i < 250)
		{
			midFreqAverage += freq[i];
		}
		else if(i >= 400 && i < 450)
		{
			highFreqAverage += freq[i];
		}
	}
	
	
	lowFreqAverage = lowFreqAverage / 50;
	midFreqAverage  = midFreqAverage / 50;
	highFreqAverage = highFreqAverage / 50;
	
	box1.rotation.y += .03;
	//box1.position.y = average * 2;
	//box1.position.y = array[0] * 2;
	box1.material.color.setRGB(30, (average * 2),  40);
	box1.scale.z = 1;
	box1.scale.y = (lowFreqAverage/10);
	box1.scale.x = 1;
	
	box2.rotation.y += .03;
	//box2.position.y = average * 2;
	//box2.position.y = array[0] * 2;
	box2.material.color.setRGB(30, (average * 2),  40);
	box2.scale.z = 1;
	box2.scale.y = (midFreqAverage/10);
	box2.scale.x = 1;
	
	box3.rotation.y += .03;
	//box3.position.y = average * 2;
	//box3.position.y = array[0] * 2;
	box3.material.color.setRGB(30, (average * 2),  40);
	box3.scale.z = 1;
	box3.scale.y = (highFreqAverage/10);
	box3.scale.x = 1;
	
	box4.rotation.y += .03;
	box4.position.y = average * 2;
	box4.material.color.setRGB(1, 1 , (average * 2));
	box4.scale.z = 1;
	box4.scale.y = (lowFreqAverage/10);
	box4.scale.x = 1;
	
	cyl1.rotation.y += .03;
	cyl1.position.y = average * 2;
	cyl1.material.color.setRGB(20, (average * 2), 10);
	cyl1.scale.z = 1;
	cyl1.scale.y = (midFreqAverage/10);
	cyl1.scale.x = 1;
	
	cyl2.rotation.y += .03;
	cyl2.position.y = average * 2;
	cyl2.material.color.setRGB((average * 2), 255, 1);
	cyl2.scale.z = 1;
	cyl2.scale.y = (highFreqAverage/10);
	cyl2.scale.x = 1;
	
	cyl3.rotation.y += .03;
	cyl3.position.y = average * 2;
	cyl3.material.color.setRGB(234, (average * 2), 255);
	cyl3.scale.z = 1;
	cyl3.scale.y = (lowFreqAverage/10);
	cyl3.scale.x = 1;
	
	cyl4.rotation.y += .03;
	cyl4.position.y = average * 2;
	cyl4.material.color.setRGB(64, 255, (average * 2));
	cyl4.scale.z = 1;
	cyl4.scale.y = (midFreqAverage/10);
	cyl4.scale.x = 1;
	
	cyl5.rotation.y += .03;
	cyl5.position.y = average * 2;
	cyl5.material.color.setRGB(5, (average * 2), 233);
	cyl5.scale.z = 1;
	cyl5.scale.y = (highFreqAverage/10);
	cyl5.scale.x = 1;
	
	
	//**********************************************particles***********************************
	particleSystem.rotation.y += 0.01;
	
	var pCount = particleCount;
	while(pCount--)
	{
	/*
		//&*#$%&*$%&*#*%&J*&#&ISSUE  ISSUE  ISSUE)#*$^#)$*)#*&@**@^&)*@
		var particle = new THREE.Vector3(particles.vertices[pCount]);
		
		if(particle.position.y < -200)
		{
			particle.position.y = 200;
			particle.velocity.y = 0;
		}
		
		particle.velocity.y -= Math.random() * .1;
		
		particle.position.addSelf(particle.velocity);
	*/
	}	
	
	particleSystem.geometry._dirtyVertices = true;
	
	renderer.render(scene, camera);
	

	SphereChange();

	if(render())
	{
		requestAnimationFrame(animate);  
	}
	
}


function render() 
{ 
  try
  {
    if(useRift)
	{
      riftCam.render(scene, camera);
    }
	else
	{
      controls.update();
      renderer.render(scene, camera);
    }  
  } 
  catch(e)
  {
    //console.log(e);
    if(e.name == "SecurityError")
	{
      crashSecurity(e);
    } 
	else 
	{
      crashOther(e);
    }
    return false;
  }
  return true;
}


function SphereChange()
{

	for(var i = 0; i < spheres.length; i++)
	{
		spheres[i].position.y = spheresY[i] + ((freq[i]) /*(Math.random() * 25)*/);
		
		spheres[i].material.color.setRGB((freq[5]/2), (freq[150]/2), (freq[300]/2));//low frequencies
		
		spheres[i].scale.z = (freq[i] / 80);
		spheres[i].scale.y = (freq[i] / 80);
		spheres[i].scale.x = (freq[i] / 80);
	}
}


window.onload = function() 
{
  init();
  animate();
}