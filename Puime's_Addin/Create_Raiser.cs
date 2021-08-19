using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;

namespace Puime_s_Addin
{
    class Create_Raiser
    {
        public static void Create_ABB_Raiser()
        {
            #region try
            Project.UndoContext.BeginUndoStep("RotateBasedOnAxis");
            try
            {
                Station stn = Station.ActiveStation;
                    if (stn == null) return;

                var StationRaisers = new List<Raiser_constructor>(); // List to store all the raisers to create

                var StationElements = stn.GraphicComponents.ToList(); // List of all Station GraphisComponents

                bool RobotInStation = false; // To check if there are some Robots at the station

                if (StationElements.Count > 0) // Check if some object are in the station, if not, send message
                {
                    foreach (GraphicComponent item in stn.GraphicComponents)
                    {
                        bool mec = item.TypeDisplayName == "Mechanism";
                        if (mec)
                        {
                            Mechanism mec2 = item as Mechanism;
                            bool mec3 = mec2.MechanismType.ToString() == "Robot";
                            if (mec3)
                            {
                                RobotInStation = true; // There is some Robot at the station
                                string name = item.DisplayName.ToString();
                                string[] complete_name = name.Split('_');
                                string robot_model = complete_name[0];

                                switch (robot_model)
                                {
                                    case "IRB52":
                                    case "IRB1600":
                                    case "IRB1600ID":
                                    case "IRB1660ID":
                                    case "IRB2600":
                                    case "IRB2600ID":
                                    case "IRB4600":
                                        StationRaisers.Add(new Raiser_constructor(name.ToString(), "TypeA", item.Transform.X * 1000, item.Transform.Y * 1000, item.Transform.Z * 1000, item.Transform.RZ));
                                        break;

                                    case "IRB2400":
                                        StationRaisers.Add(new Raiser_constructor(name.ToString(), "TypeB", item.Transform.X * 1000, item.Transform.Y * 1000, item.Transform.Z * 1000, item.Transform.RZ));
                                        break;

                                    case "IRB6400R":
                                    case "IRB6620":
                                    case "IRB6640":
                                    case "IRB6650S":
                                    case "IRB6660":
                                    case "IRB6700":
                                    case "IRB7600":
                                    case "IRB660":
                                    case "IRB760":
                                    case "IRB460":
                                        StationRaisers.Add(new Raiser_constructor(name.ToString(), "TypeC", item.Transform.X * 1000, item.Transform.Y * 1000, item.Transform.Z * 1000, item.Transform.RZ));
                                        break;

                                    default:
                                        MessageBox.Show(name + "\n\n" + "Not supported Robot model.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }
                            }
                        }
                    }

                    if (!RobotInStation) // if none of the elements in the station is a Robot, sends a message
                    {
                        MessageBox.Show("No valid Robots detected in the station.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else // if no elements in the station sends a message 
                {
                    MessageBox.Show("No valid Robots detected in the station.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              
                foreach (var item in StationRaisers)
                {
                    Raiser(item.Name, item.Type, item.Xpos, item.Ypos, item.Orientation, item.Zpos);
                }
            }
            #endregion try

            catch (Exception execption)
                {
                    Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                    Logger.AddMessage(new LogMessage(execption.Message.ToString()));
                    throw;
                }
            finally
                {
                     Project.UndoContext.EndUndoStep();
                }

        }

        public static void Raiser(string name, string type, double xpos, double ypos, double orientation, double height)
        {
            if (height < 300)
            {
            MessageBox.Show(name + "\n\n" + "Not supported Robot position." + "\n" + "Minium position must be 300mm." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                switch (type)
                {
                    #region Type A
                    case "TypeA":

                        // Checks if the raiser allready exists
                        var allreadyexists = false;
                        Station stn2 = Station.ActiveStation;
                            if (stn2 == null) return;

                        foreach (GraphicComponent item in stn2.GraphicComponents)
                        {
                            bool rai = item.DisplayName == "ABB_Raiser_" + name;
                            if (rai)
                            {
                                MessageBox.Show("Raiser " + name + " allready exist." + "\n\n" + "Delete it or change it's name.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                allreadyexists = true;
                            }
                        }

                        if (allreadyexists)
                        {
                            allreadyexists = false;
                            break;
                        }

                        // Checks if the z position of the Robot is in the maximum allowed
                        if (height>1600) // maximum allowed height
                        {
                            MessageBox.Show(name + "\n\n" + "Not supported Robot position." + "\n" + "Maximum position is 1600mm." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }


                        // checks if the height is in the allowed range
                        var a = height == 300 || height == 400 || height == 500 || height == 600 || height == 700 || height == 800 || height == 900 || height == 1000
                             || height == 1100 || height == 1200 || height == 1300 || height == 1400 || height == 1500 || height == 1600;

                        if (a) // if heght is in the allowed range, creates the raiser 
                        {
                            Station station = Project.ActiveProject as Station;

                            // Import the BasePlateTypeA library                                                                                                             
                            GraphicComponentLibrary BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlate\\BasePlateTypeA.rslib", true, null, false);

                            Part myPart1 = BasePlateTypeALib.RootComponent.CopyInstance() as Part;
                            myPart1.Name = "BasePlateTypeA";
                            myPart1.DisconnectFromLibrary();

                            // Create the raiser middle cylinder.
                            Part myPart3 = new Part();
                            myPart3.Name = "Body";
                            station.GraphicComponents.Add(myPart3);
                            // Create a Cylinder.
                            Vector3 vect_position = new Vector3(0, 0, 0.030); //Creates the cylinder in the 0,0,30 to transfor the position later.
                            Vector3 vect_orientation = new Vector3(0, 0, 0);
                            Matrix4 matrix_origo = new Matrix4(vect_position, vect_orientation);
                            Body b1 = Body.CreateSolidCylinder(matrix_origo, 0.33, height / 1000-0.088);
                            b1.Name = "Raiser_body";
                            myPart3.Bodies.Add(b1);

                            // Import the TopPlateTypeA library
                            GraphicComponentLibrary TopPlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\TopPlate\\TopPlateTypeA.rslib", true, null, false);
                            Part myPart2 = TopPlateTypeALib.RootComponent.CopyInstance() as Part;
                            myPart2.Name = "TopPlateTypeA";
                            myPart2.DisconnectFromLibrary();
                            myPart2.Transform.Z = height/1000 - 0.058;

                            GraphicComponentGroup myGCGroup = new GraphicComponentGroup();
                            myGCGroup.Name = "ABB_Raiser_" + name;
                            station.GraphicComponents.Add(myGCGroup);
                            myGCGroup.GraphicComponents.Add(myPart1);
                            myGCGroup.GraphicComponents.Add(myPart2);
                            myGCGroup.GraphicComponents.Add(myPart3);
                            myGCGroup.Color = Color.FromArgb(255,255,128,0);

                            // Transform the position of the part to the values of the pos_control values. So the part origin is allways in the corner of the box.
                            myGCGroup.Transform.X = xpos / 1000;
                            myGCGroup.Transform.Y = ypos / 1000;
                            myGCGroup.Transform.RZ = orientation;
                        }

                        else // if heght isn't in the allowed range
                        {
                            MessageBox.Show(name + "\n\n" + "Position must be betwen 300mm. and 1600mm." + "\n" + "In 100mm. increment." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        Logger.AddMessage(new LogMessage("ABB_Raiser_" + name + " created.", "Puime's Add-in"));
                        break;
                    #endregion TypeA

                    #region Type B
                    case "TypeB":

                        // Checks if the raiser allready exists
                        var allreadyexistsb = false;
                        Station stn2b = Station.ActiveStation;
                        if (stn2b == null) return;

                        foreach (GraphicComponent item in stn2b.GraphicComponents)
                        {
                            bool rai = item.DisplayName == "ABB_Raiser_" + name;
                            if (rai)
                            {
                                MessageBox.Show("Raiser " + name + " allready exist." + "\n\n" + "Delete it or change it's name.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                allreadyexistsb = true;
                            }
                        }

                        if (allreadyexistsb)
                        {
                            allreadyexistsb = false;
                            break;
                        }

                        // Checks if the z position of the Robot is in the maximum allowed
                        if (height > 1600) // maximum allowed height
                        {
                            MessageBox.Show(name + "\n\n" + "Not supported Robot position." + "\n" + "Maximum position is 1600mm." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        // checks if the height is in the allowed range
                        var b = height == 300 || height == 400 || height == 500 || height == 600 || height == 700 || height == 800 || height == 900 || height == 1000
                             || height == 1100 || height == 1200 || height == 1300 || height == 1400 || height == 1500 || height == 1600;

                        if (b) // if heght is in the allowed range, creates the raiser
                        {
                            Station station = Project.ActiveProject as Station;

                            // Import the BasePlateTypeB library
                            GraphicComponentLibrary BasePlateTypeBLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlate\\BasePlateTypeB.rslib", true, null, false);
                            Part myPart1 = BasePlateTypeBLib.RootComponent.CopyInstance() as Part;
                            myPart1.Name = "BasePlateTypeB";
                            myPart1.DisconnectFromLibrary();

                            // Create the raiser middle cylinder.
                            Part myPart3 = new Part();
                            myPart3.Name = "Body";
                            station.GraphicComponents.Add(myPart3);
                            // Create a Cylinder.
                            Vector3 vect_position = new Vector3(0, 0, 0.030); //Creates the cylinder in the 0,0,30 to transfor the position later.
                            Vector3 vect_orientation = new Vector3(0, 0, 0);
                            Matrix4 matrix_origo = new Matrix4(vect_position, vect_orientation);
                            Body b1 = Body.CreateSolidCylinder(matrix_origo, 0.33, height / 1000 - 0.090);
                            b1.Name = "Raiser_body";
                            myPart3.Bodies.Add(b1);

                            // Import the TopPlateTypeB library
                            GraphicComponentLibrary TopPlateTypeBLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\TopPlate\\TopPlateTypeB.rslib", true, null, false);
                            Part myPart2 = TopPlateTypeBLib.RootComponent.CopyInstance() as Part;
                            myPart2.Name = "TopPlateTypeB";
                            myPart2.DisconnectFromLibrary();
                            myPart2.Transform.Z = height / 1000 - 0.060;

                            GraphicComponentGroup myGCGroup = new GraphicComponentGroup();
                            myGCGroup.Name = "ABB_Raiser_" + name;
                            station.GraphicComponents.Add(myGCGroup);
                            myGCGroup.GraphicComponents.Add(myPart1);
                            myGCGroup.GraphicComponents.Add(myPart2);
                            myGCGroup.GraphicComponents.Add(myPart3);
                            myGCGroup.Color = Color.FromArgb(255, 255, 128, 0);

                            // Transform the position of the part to the values of the pos_control values. So the part origin is allways in the corner of the box.
                            myGCGroup.Transform.X = xpos / 1000;
                            myGCGroup.Transform.Y = ypos / 1000;
                            myGCGroup.Transform.RZ = orientation;
                        }

                        else // if heght isn't in the allowed range
                        {
                            MessageBox.Show(name + "\n\n" + "Position must be betwen 300mm. and 1600mm." + "\n" + " In 100mm. increment." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        Logger.AddMessage(new LogMessage("ABB_Raiser_" + name + " created.", "Puime's Add-in"));
                        break;
                    #endregion Type B

                    #region Type C
                    case "TypeC":

                        // Checks if the raiser allready exists
                        var allreadyexistsc = false;
                        Station stn2c = Station.ActiveStation;
                        if (stn2c == null) return;

                        foreach (GraphicComponent item in stn2c.GraphicComponents)
                        {
                            bool rai = item.DisplayName == "ABB_Raiser_" + name;
                            if (rai)
                            {
                                MessageBox.Show("Raiser " + name + " allready exist." + "\n\n" + "Delete it or change it's name.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                allreadyexistsc = true;
                            }
                        }

                        if (allreadyexistsc)
                        {
                            allreadyexistsc = false;
                            break;
                        }

                        // Checks if the z position of the Robot is in the maximum allowed
                        if (height > 2000)
                        {
                            MessageBox.Show(name + "\n\n" + "Not supported Robot position." + "\n" + "Maximum position is 2000mm." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }


                        // checks if the height is in the allowed range
                        var c = height == 300 || height == 400 || height == 500 || height == 600 || height == 700 || height == 800 || height == 900 || height == 1000
                             || height == 1100 || height == 1200 || height == 1300 || height == 1400 || height == 1500 || height == 1600 || height == 1700 || height == 1800
                             || height == 1900 || height == 2000;

                        if (c) // if heght is in the allowed range, creates the raiser
                        {
                            Station station = Project.ActiveProject as Station;

                            // The TypeC raiser has 3 diferent baseplates depending of it's height.
                            Part PartType = new Part(); // The part to add later depending of the baseplate choosed.
                            switch (height.ToString())
                            {
                                case "300": case "400": case "500": case "600": case "700": case "800": case "900": case "1000":
                                    // Import the BasePlateTypeC library for 300-100 height
                                    GraphicComponentLibrary BasePlateTypeBLiba = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlate\\BasePlateTypeC_300-1000.rslib", true, null, false);
                                    Part myParta = BasePlateTypeBLiba.RootComponent.CopyInstance() as Part;
                                    myParta.Name = "BasePlateTypeC";
                                    myParta.DisconnectFromLibrary();
                                    PartType = myParta;
                                    break;

                                case "1100": case "1200": case "1300": case "1400": case "1500":
                                    // Import the BasePlateTypeC library for 1100-1500 height
                                    GraphicComponentLibrary BasePlateTypeBLibb = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlate\\BasePlateTypeC_1000-1500.rslib", true, null, false);
                                    Part myPartb = BasePlateTypeBLibb.RootComponent.CopyInstance() as Part;
                                    myPartb.Name = "BasePlateTypeC";
                                    myPartb.DisconnectFromLibrary();
                                    PartType = myPartb;
                                    break;

                                case "1600": case "1700": case "1800": case "1900": case "2000":
                                    // Import the BasePlateTypeC library for 1600-2000 height
                                    GraphicComponentLibrary BasePlateTypeBLibc = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlate\\BasePlateTypeC_1600-2000.rslib", true, null, false);
                                    Part myPartc = BasePlateTypeBLibc.RootComponent.CopyInstance() as Part;
                                    myPartc.Name = "BasePlateTypeC";
                                    myPartc.DisconnectFromLibrary();
                                    PartType = myPartc;
                                    break;

                                default:
                                    break;
                            }


                            // Create the raiser middle cylinder.
                            Part myPart3 = new Part();
                            myPart3.Name = "Body";
                            station.GraphicComponents.Add(myPart3);
                            // Create a Cylinder.
                            Vector3 vect_position = new Vector3(0, 0, 0.030); //Creates the cylinder in the 0,0,30 to transfor the position later.
                            Vector3 vect_orientation = new Vector3(0, 0, 0);
                            Matrix4 matrix_origo = new Matrix4(vect_position, vect_orientation);
                            Body b1 = Body.CreateSolidCylinder(matrix_origo, 0.46, height / 1000 - 0.090);
                            b1.Name = "Raiser_body";
                            myPart3.Bodies.Add(b1);

                            // Import the TopPlateTypeC library
                            GraphicComponentLibrary TopPlateTypeBLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\TopPlate\\TopPlateTypeC.rslib", true, null, false);
                            Part myPart2 = TopPlateTypeBLib.RootComponent.CopyInstance() as Part;
                            myPart2.Name = "TopPlateTypeC";
                            myPart2.DisconnectFromLibrary();
                            myPart2.Transform.Z = height / 1000 - 0.060;

                            GraphicComponentGroup myGCGroup = new GraphicComponentGroup();
                            myGCGroup.Name = "ABB_Raiser_" + name;
                            station.GraphicComponents.Add(myGCGroup);
                            myGCGroup.GraphicComponents.Add(PartType);
                            myGCGroup.GraphicComponents.Add(myPart2);
                            myGCGroup.GraphicComponents.Add(myPart3);
                            myGCGroup.Color = Color.FromArgb(255, 255, 128, 0);

                            // Transform the position of the part to the values of the pos_control values. So the part origin is allways in the corner of the box.
                            myGCGroup.Transform.X = xpos / 1000;
                            myGCGroup.Transform.Y = ypos / 1000;
                            myGCGroup.Transform.RZ = orientation;
                        }

                        else // if heght isn't in the allowed range
                        {
                            MessageBox.Show(name + "\n\n" + "Position must be betwen 300mm. and 2000mm." + "\n" + " In 100mm. increment." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Raiser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        Logger.AddMessage(new LogMessage("ABB_Raiser_" + name + " created.", "Puime's Add-in"));
                        break;
                    #endregion Type C

                    default:
                        break;
                }
            }
        }
    } 
}

