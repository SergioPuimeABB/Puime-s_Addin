 ![Add-in](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/000.png)
# Puime's Addin
###### Version 1.0


**Overview.**
The Puime’s Addin is a RobotStudio add-in that extends RobotStudio with some useful tools. Use the add-in to copy objects position, to create a station floor, ABB boxes and ABB raisers.

**Installation.**
From the Add-Ins tab in RobotStudio, click on “Install Package” and select the PuimesAddin-1.0.rspak file. This will add an item in the “Installed Packages” section. You’ll need to restart RobotStudio in order to finish the installation.

**How it works.**
After the installation, a new toolwindow will be added at the right side in RobotStudio. It’ll start hidden.



*Add-in screen position*

![Add-in screen position](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/001.png)



*Add-in main screen*

![Add-in main screen](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/002.png)

* **Copy Position.** Copy the position and orientation of the selected object. It only woks with Parts, Targets or WorkObjects.

* **Set Position.** Sets the previous copied position and orientation to the selected object. It only woks with Parts, Targets or WorkObjects.

* **Create floor.** Creates a floor in the station. Scans all the station objects position to store its bounding box and creates a box with the dimension of the bounding box plus a meter in all directions. The top face of the floor is at z0 of the station. 
It’ll reset the RS floor size to match the floor and hides all the targets/frames and paths.

* **Create ABB box.** Similar to the standard Create box, but it’ll add cardboard textures to all of the created box faces. The origin of the created box will be always in the box corner though you specified another position and orientation when you create it. 

* **Create ABB raiser.** Creates ABB standard raisers (type A, B and C). It’ll scan all the station robots and its position to create the corresponding raiser at the robot position with the robot height.

	The supported robot models are:
	- Type A
	  - IRB52, IRB1600, IRB1600ID, IRB1660ID, IRB2600, IRB2600ID, IRB4600

	- Type B
	  - IRB2400

	- Type C
	  - IRB6400R, IRB6620, IRB6640, IRB6650S, IRB6660, IRB6700, IRB7600, IRB660, IRB760, IRB460

	The position must be between 300 mm and 1600 mm in 100 mm increments.




* In the Data folder you'll find the rspak file and the libraries and textures used.
