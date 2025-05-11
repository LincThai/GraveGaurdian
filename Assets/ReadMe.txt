By Linxdarkness

Summary
This is a 3rd-Person Character Controller with all the movement functionality (e.g. Jump, Crouch, Sprint and walk) without animations or keybindings for Unity. The purpose of this tool is to give developer a quick and easy 3rd-Person character controller, with basically all functionality built-in for them to use. This is a tool that fulfills the primary function of making a game playable when you need something to move around a space.

How To Use
You may need to install Cinemachine for the tool and Probuilder for the demo.

Basic Setup
1. Bring in the two prefabs Player and 3rdPersonCam into you Unity scene and remove the main camera that is already in the scene.
2. On the Player Prefab ensure that the Controller, Cam, Camera Rig, Cam Rotation and Ground Check references are set.
	a. For the controller just drag and drop the Character Controller on the Player prefab here.
	b. Go to the 3rdPersonCam in the hierarchy open it's drop down where you will see a main camera drag this to the cam reference.
	c. Drag in the 3rdPersonCam for the Camera Rig.
	d. Drag in the 3rdPersonCam for the Cam Rotation.
	e. In the Player prefab their is an object called GroundCheck drag that into the field with the same name.
3. Go to the Ground Mask click the dropdown and set it to Ground. If you don't have a ground layer in the dropdown there is an add layer button click it and you will see a list of layers. In an empty space make write Ground and there you have it you have a Ground layer.
4. Set the layer of all the floors, platforms and other objects the player can walk on to Ground. This will allow the player to jump.
5. In the Cinemachine Camera under Target Tracking drag in the Player Prefab.

The variable seen in in the rest of the inspector do basically what they say. An example is The three top most numerical variables they each represent a movement speed in each of the three modes; walk, sprint or crouch. While ther variables for height are slightly different the jump height is as stated is the hieght of your jump while the other two are the heights of your collider i.e. when you are standing or crouching you are shorter ot taller. Most variables do what they say so experiment.

For the cinemachine camera I am using the target tracking function which automatically gives you the Orbital Follow, Rotation Composer and Input Axis Controller Components. I also Added the Deoccluder and Decollider for objects that the camera collides into and is blocked by. Keep your environment/obstacles under the ground layer and everything should work fine.
For what is in the editor for the cinemachine the variables are for the Three Ring Rig Cinemachine System I am using you will need to make sure you have all the camera references connected. And the variables for each ring are their Height and Radius which are labeled for the Top, Center and Bottom. height is their position on the y axis and Radius is the distance from the player to the camera which orbits the player. The spline curvature is the orbital movement between rings. Screen Position is where the player is positioned in the screen.

References
3rd person controller, movement
https://www.youtube.com/watch?v=4HpC--2iowE&t=1022s
jump
https://www.youtube.com/watch?v=_QajrabyTJc&t=711s
3rd person camera using Cinememachine
https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/index.html