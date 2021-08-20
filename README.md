 ![Add-in](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/000.png)
# Puime's Addin
###### Version 2.0



**Overview.**
The Puime’s Addin is a RobotStudio add-in that extends RobotStudio with some useful tools. Use the add-in to copy objects position, to create a station floor, ABB boxes, ABB raisers and auto markups.

**Installation.**
From the Add-Ins tab in RobotStudio, click on “Install Package” and select the PuimesAddin-1.0.rspak file. This will add an item in the “Installed Packages” section. You’ll need to restart RobotStudio in order to finish the installation.

**How it works.**
After the installation, a new ribbon group will be added at the Create group in the Modeling ribbon tab.





![Add-in screen position](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/01.png)

*Add-in screen position*




![Add-in main screen](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/02.png)

*Add-in main screen*



* **Copy Position.** ![copy position](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/03.png) Copy the position and orientation of the selected object. It only woks with Parts, Targets or WorkObjects.

* **Set Position.** ![set position](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/04.png) Sets the previous copied position and orientation to the selected object. It only woks with Parts, Targets or WorkObjects.

* **Floor creator.** ![floor creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/05.png) Creates a floor in the station. Scans all the station objects position to store its bounding box and creates a box with the dimension of the bounding box plus a meter in all directions. The top face of the floor is at z0 of the station. 
It’ll reset the RS floor size to match the floor and hides all the targets/frames and paths.

* **ABB box creator.** ![ABB box creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/06.png) Similar to the standard Create box, but it’ll add cardboard textures to all of the created box faces. The origin of the created box will be always in the box corner though you specified another position and orientation when you create it. In the v2 it’ll create a preview box and accepts only one dimension value copying the preview one. 

* **ABB raiser creator.** ![ABB raiser creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/07.png) Creates ABB standard raisers (type A, B and C). It’ll scan all the station robots and its position to create the corresponding raiser at the robot position with the robot height.

	The supported robot models are:
	- Type A
	  - IRB52, IRB1600, IRB1600ID, IRB1660ID, IRB2600, IRB2600ID, IRB4600

	- Type B
	  - IRB2400

	- Type C
	  - IRB6400R, IRB6620, IRB6640, IRB6650S, IRB6660, IRB6700, IRB7600, IRB660, IRB760, IRB460

	The position must be between 300 mm and 1600 mm in 100 mm increments.
 
* **Auto markup creator.** ![Auto markup creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/08.png) Designed to help in the clips naming of the clipping applications. It creates a markup each time you click in the graphic window. The markup can be customized in name, color, start number, increments and you can select the standard clipping color.

 
# 

**Version History.**

**# 2.0 (2021 Aug)**
	* Access to the tool changed to the Ribbon tab.
	* ABB Box creator tool had some improvements.
	* Auto markup creator tool added.

**# 1.0**
	* First available Package
 
 
 
*In the Data folder you'll find the rspak file, the libraries and the textures used.*
