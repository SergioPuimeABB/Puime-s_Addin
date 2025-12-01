 ![Add-in](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/000.png)
# Puime's Addin
###### Version 4.0


**Overview.** 
The Puime’s Addin is a RobotStudio add-in coded by Sergio Puime (sergio.puime@es.abb.com) that extends RobotStudio with some useful tools. Use the add-in to copy objects position, to create a station floor, ABB boxes, ABB raisers, auto markups and aluminum profiles. It has some useful helpers like Auto rename targets, Join parts, Auto move parameters and Zoom view.

**Installation.**
From the Add-Ins tab in RobotStudio, click on “Install Package” and select the PuimesAddin-4.0.rspak file. This will add an item in the “Installed Packages” section. You’ll need to restart RobotStudio in order to finish the installation.

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

* **ABB box creator.** ![ABB box creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/06.png) Like the standard Create box, but it’ll add cardboard textures to all of the created box faces. The origin of the created box will be always in the box corner though you specified another position and orientation when you create it. In the v2 it’ll create a preview box and accepts only one dimension value copying the preview one.

* **ABB raiser creator.** ![ABB raiser creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/07.png) Creates ABB standard raisers (type A, B and C). It’ll scan all the station robots and its position to create the corresponding raiser at the robot position with the robot height.

	The supported robot models are:
	- Type A
	  - IRB52, IRB1600, IRB1600ID, IRB1660ID, IRB2600, IRB2600ID, IRB4600

	- Type B
	  - IRB2400

	- Type C
	  - IRB6400R, IRB6620, IRB6640, IRB6650S, IRB6660, IRB6700, IRB7600, IRB660, IRB760, IRB460

	The position must be between 300 mm and 1600 mm in 100 mm increments.
    
* **ABB base plate creator.** ![ABB base plate creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/09.png) Creates ABB standard base plates (type A, B and C). It’ll scan all the station robots and its position to create the corresponding base plate at the robot position.

	The supported robot models are:
	- Type A
	  - IRB52, IRB1600, IRB1600ID, IRB1660ID, IRB2600, IRB2600ID, IRB4600

	- Type B
	  - IRB2400

	- Type C
	  - IRB6400R, IRB6620, IRB6640, IRB6650S, IRB6660, IRB6700, IRB7600, IRB660, IRB760, IRB460

	The position of the robot must be 60 mm, 70 mm, 80 mm, 90 mm, or 100 mm in height.

 
* **Auto markup creator.** ![Auto markup creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/08.png) Designed to help in the clips naming of the clipping applications. It creates a markup each time you click in the graphic window. The markup can be customized in name, color, start number, increments and you can select the standard clipping color.
![Auto markup creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/10.png) 

* **Aluminum profile creator.** ![Aluminum profile creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/11.png) Creates aluminum square profiles with custom length and selected shape. 
![Aluminum profile creator](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/12.png) 

* **Auto rename targets.** ![Auto rename targets](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/13.png)
Whit a path selected; it’ll rename the “Target_xxx” target names to “pxxx” in that path.
You can use right click mouse button to display this tool if one Path is selected.
![Auto rename targets](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/14.png) 
 
* **Join parts.** ![Join parts](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/15.png)
Whit some parts selected, will join all the parts bodies into a single “JoinedPart” part.
You can use right click mouse button to display this tool if one or more Parts are selected.

* **Auto move parameters.** ![Auto move parameters](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/16.png) 
Assign preset move parameters to the selected move instruction. When you open the tool, it opens a window with different move parameters. If a move instruction is selected in the tree, you can assign the desired parameters to the move instruction.
![Auto move parameters](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/17.png) 

* **Zoom view.** ![Zoom view](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/18.png) 
Displays a window to zoom more or less with a custom zoom factor.
![Zoom view](https://github.com/SergioPuimeABB/Puime-s_Addin/blob/master/Puime's_Addin/Screenshots/10.png) 


 
# 

**Version History.**

**# 4.0 (2025 Dec)**
•	Adapted to be able to works in RS2024 and RS2025. The RS2025 DistributionPackages can be at different folder.
•	Aluminum profile creator tool added.
•	Auto move parameters tool added.
•	Zoom view tool added.
•	ABB Box creator tool. Now you can change the opacity in the “ Graphic Appearance “.
•	Auto rename targets tool added to the right click mouse button.
•	Auto rename targets tool. This tool only renames the first time that it finds a target. If the same target is in another place of the path, this time the target will not be renamed, and it will be at an useless state. If you need to use the target more than once, please, rename it before.
•	Join parts tool added to the right click mouse button.
•	Join parts tool. This tool will only works if the selected parts have bodies.

**# 3.0 (2023 Nov)**
•	Floor Creator changes the floor color that creates.
•	ABB Raiser creator creates a different color raiser.
•	ABB Base plate creator tool added.
•	Auto rename targets tool added.
•	Join parts tool added.

**# 2.0 (2021 Aug)**

- Access to the tool changed to the Ribbon tab.
- ABB Box creator tool had some improvements.
- Auto markup creator tool added.

**# 1.0**

- First available Package
 
 
 
 
*In the Data folder you'll find the rspak file, the libraries and the textures used.*
