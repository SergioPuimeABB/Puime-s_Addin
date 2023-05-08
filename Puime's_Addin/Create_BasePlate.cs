
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
    class Create_BasePlate
    {
        public static void Create_ABB_BasePlate()
        {
            #region try
            Project.UndoContext.BeginUndoStep("RotateBasedOnAxis");
            try
            {
                Station stn = Station.ActiveStation;
                if (stn == null) return;

                var StationBasePlates = new List<BasePlate_constructor>(); // List to store all the baseplates to create

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
                                        StationBasePlates.Add(new BasePlate_constructor(name.ToString(), "TypeA", item.Transform.X * 1000, item.Transform.Y * 1000, item.Transform.Z * 1000, item.Transform.RZ));
                                        break;

                                    case "IRB2400":
                                        StationBasePlates.Add(new BasePlate_constructor(name.ToString(), "TypeB", item.Transform.X * 1000, item.Transform.Y * 1000, item.Transform.Z * 1000, item.Transform.RZ));
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
                                        StationBasePlates.Add(new BasePlate_constructor(name.ToString(), "TypeC", item.Transform.X * 1000, item.Transform.Y * 1000, item.Transform.Z * 1000, item.Transform.RZ));
                                        break;

                                    default:
                                        MessageBox.Show(name + "\n\n" + "Not supported Robot model.", "Puime's Addin - Create ABB BasePlate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }
                            }
                        }
                    }

                    if (!RobotInStation) // if none of the elements in the station is a Robot, sends a message
                    {
                        MessageBox.Show("No valid Robots detected in the station.", "Puime's Addin - Create ABB BasePlate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else // if no elements in the station sends a message 
                {
                    MessageBox.Show("No valid Robots detected in the station.", "Puime's Addin - Create ABB BasePlate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach (var item in StationBasePlates)
                {
                    BasePlate(item.Name, item.Type, item.Xpos, item.Ypos, item.Orientation, item.Zpos);
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

        public static void BasePlate(string name, string type, double xpos, double ypos, double orientation, double height)
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
                            bool rai = item.DisplayName == "ABB_BasePlate_" + name;
                            if (rai)
                            {
                                MessageBox.Show("Base Plate " + name + " allready exist." + "\n\n" + "Delete it or change it's name.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                allreadyexists = true;
                            }
                        }

                        if (allreadyexists)
                        {
                            allreadyexists = false;
                            break;
                        }

                        // Checks if the z position of the Robot is in the maximum allowed
                        if (height > 100) // maximum allowed height
                        {
                            MessageBox.Show(name + "\n\n" + "Not supported Robot position for robot " + name + "." + "\n" + "Maximum position is 100mm." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }


                        // checks if the height is in the allowed range
                        var a = height == 60 || height == 70 || height == 80 || height == 90 || height == 100;

                        if (a) // if heght is in the allowed range, creates the raiser 
                        {
                            Station station = Project.ActiveProject as Station;

                        //Load the base plate based on the height
                        GraphicComponentLibrary BasePlateTypeALib = new GraphicComponentLibrary();

                        switch (height)
                        {
                            case 60:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeA70.rslib", true, null, false);
                                break;
                            case 70:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeA70.rslib", true, null, false);
                                break;
                            case 80:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeA80.rslib", true, null, false);
                                break;
                            case 90:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeA90.rslib", true, null, false);
                                break;
                            case 100:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeA100.rslib", true, null, false);
                                break;
                            default:
                                break;
                        }

                        var myPart1 = BasePlateTypeALib.RootComponent.CopyInstance();
                        myPart1.Name = "BasePlateTypeA";
                        myPart1.DisconnectFromLibrary();

                        GraphicComponentGroup myGCGroup = new GraphicComponentGroup();
                            myGCGroup.Name = "ABB_BasePlate_" + name;
                            station.GraphicComponents.Add(myGCGroup);
                            myGCGroup.GraphicComponents.Add(myPart1);
                            //myGCGroup.Color = Color.FromArgb(255, 255, 128, 0);

                            // Transform the position of the part to the values of the pos_control values. So the part origin is allways in the corner of the box.
                            myGCGroup.Transform.X = xpos / 1000;
                            myGCGroup.Transform.Y = ypos / 1000;
                            myGCGroup.Transform.RZ = orientation;
                        }

                        else // if heght isn't in the allowed range
                        {
                            MessageBox.Show(name + "\n\n" + "Position must be betwen 60 mm and 100 mm" + "\n" + "In 10 mm increment." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        Logger.AddMessage(new LogMessage("ABB_BasePlate_" + name + " created.", "Puime's Add-in"));
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
                        bool rai = item.DisplayName == "ABB_BasePlate_" + name;
                        if (rai)
                        {
                            MessageBox.Show("Base Plate " + name + " allready exist." + "\n\n" + "Delete it or change it's name.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            allreadyexistsb = true;
                        }
                    }

                    if (allreadyexistsb)
                    {
                        allreadyexistsb = false; 
                        break;
                    }

                    // Checks if the z position of the Robot is in the maximum allowed
                    if (height > 50) // maximum allowed height
                    {
                        MessageBox.Show(name + "\n\n" + "Not supported Robot position for robot " + name + "." + "\n" + "Maximum position is 50 mm." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }


                    // checks if the height is in the allowed range
                    var b = height == 50;

                    if (b) // if heght is in the allowed range, creates the raiser 
                    {
                        Station station = Project.ActiveProject as Station;

                        // Import the BasePlateTypeA library                                                                                                             
                        GraphicComponentLibrary BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeB50.rslib", true, null, false);

                        var myPart1 = BasePlateTypeALib.RootComponent.CopyInstance();
                        myPart1.Name = "BasePlateTypeA";
                        myPart1.DisconnectFromLibrary();

                        GraphicComponentGroup myGCGroup = new GraphicComponentGroup();
                        myGCGroup.Name = "ABB_BasePlate_" + name;
                        station.GraphicComponents.Add(myGCGroup);
                        myGCGroup.GraphicComponents.Add(myPart1);
                        //myGCGroup.Color = Color.FromArgb(255, 255, 128, 0);

                        // Transform the position of the part to the values of the pos_control values. So the part origin is allways in the corner of the box.
                        myGCGroup.Transform.X = xpos / 1000;
                        myGCGroup.Transform.Y = ypos / 1000;
                        myGCGroup.Transform.RZ = orientation;
                    }

                    else // if heght isn't in the allowed range
                    {
                        MessageBox.Show(name + "\n\n" + "Position must be 50 mm." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    Logger.AddMessage(new LogMessage("ABB_BasePlate_" + name + " created.", "Puime's Add-in"));
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
                        bool rai = item.DisplayName == "ABB_BasePlate_" + name;
                        if (rai)
                        {
                            MessageBox.Show("Base Plate " + name + " allready exist." + "\n\n" + "Delete it or change it's name.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            allreadyexistsc = true;
                        }
                    }

                    if (allreadyexistsc)
                    {
                        allreadyexistsc = false;
                        break;
                    }

                    // Checks if the z position of the Robot is in the maximum allowed
                    if (height > 100) // maximum allowed height
                    {
                        MessageBox.Show(name + "\n\n" + "Not supported Robot position for robot " + name + "." + "\n" + "Maximum position is 100mm." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }


                    // checks if the height is in the allowed range
                    var c = height == 60 || height == 70 || height == 80 || height == 90 || height == 100;

                    if (c) // if heght is in the allowed range, creates the raiser 
                    {
                        Station station = Project.ActiveProject as Station;

                        //Load the base plate based on the height
                        GraphicComponentLibrary BasePlateTypeALib = new GraphicComponentLibrary();

                        switch (height)
                        {
                            case 60:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeC60.rslib", true, null, false);
                                break;
                            case 70:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeC70.rslib", true, null, false);
                                break;
                            case 80:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeC80.rslib", true, null, false);
                                break;
                            case 90:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeC90.rslib", true, null, false);
                                break;
                            case 100:
                                BasePlateTypeALib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\BasePlates\\BasePlateTypeC100.rslib", true, null, false);
                                break;
                            default:
                                break;
                        }

                        var myPart1 = BasePlateTypeALib.RootComponent.CopyInstance();
                        myPart1.Name = "BasePlateTypeA";
                        myPart1.DisconnectFromLibrary();

                        GraphicComponentGroup myGCGroup = new GraphicComponentGroup();
                        myGCGroup.Name = "ABB_BasePlate_" + name;
                        station.GraphicComponents.Add(myGCGroup);
                        myGCGroup.GraphicComponents.Add(myPart1);
                        //myGCGroup.Color = Color.FromArgb(255, 255, 128, 0);

                        // Transform the position of the part to the values of the pos_control values. So the part origin is allways in the corner of the box.
                        myGCGroup.Transform.X = xpos / 1000;
                        myGCGroup.Transform.Y = ypos / 1000;
                        myGCGroup.Transform.RZ = orientation;
                    }

                    else // if heght isn't in the allowed range
                    {
                        MessageBox.Show(name + "\n\n" + "Position must be betwen 60 mm and 100 mm" + "\n" + "In 10 mm increment." + "\n\n" + "Actual position is " + height.ToString() + "mm.", "Puime's Addin - Create ABB Base plate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    Logger.AddMessage(new LogMessage("ABB_BasePlate_" + name + " created.", "Puime's Add-in"));
                    break;
                #endregion Type C

                default:
                        break;
                }
            
        }
    }
}

