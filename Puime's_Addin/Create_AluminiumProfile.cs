﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace PuimesAddin
{
    class Create_AluminiumProfile
    {
        public static void CreateAluminiumProfile()
        {

            #region try
            Project.UndoContext.BeginUndoStep("CreateAluminiumProfile");
            try
            {
                Station stn = Station.ActiveStation;
                if (stn == null) return;


                // Import the Profile librery
                //TODO: Realizar cases según medida seleccioanda en el menú
                GraphicComponentLibrary ProfileLib = GraphicComponentLibrary.Load("C:\\ProgramData\\ABB\\DistributionPackages\\PuimesAddin-2.0\\RobotStudio\\Add-In\\Library\\AlumProfiles\\BaseProfile_20x20.rslib", true, null, true);

                //Part myPart = new Part();
                Part myPart = ProfileLib.RootComponent.CopyInstance() as Part;
                myPart.Name = "20Profile";
                myPart.DisconnectFromLibrary();
                stn.GraphicComponents.Add(myPart);
                ProfileLib.Close();
                
                Part myPart2 = new Part();
                myPart2.Name = "20ProfileCopy";
                stn.GraphicComponents.Add(myPart2);

                ABB.Robotics.RobotStudio.Stations.Body b = myPart.Bodies.First();

                myPart2.Bodies.Add(b);

                Face f = b.Shells[0].Faces[0];

                // Create a wire.
                ABB.Robotics.RobotStudio.Stations.Body wirebody = ABB.Robotics.RobotStudio.Stations.Body.CreateLine
                                (new Vector3(0.0, 0.0, 0.0), new Vector3(0, 0, 1));
                wirebody.Name = "Wirebody";
                myPart2.Bodies.Add(wirebody);
                
                Wire w = wirebody.Shells[0].Wires[0];

                // Set the sweep option to solid.
                SweepOptions so = new SweepOptions();
                so.MakeSolid = true;
                
                Vector3 vec = new Vector3(0, 0, 1);

               
                
                ABB.Robotics.RobotStudio.Stations.Body[] bds = ABB.Robotics.RobotStudio.Stations.Body.Extrude(f, vec, w, so);
                foreach (ABB.Robotics.RobotStudio.Stations.Body bd in bds)
                {
                    bd.Name = "Wire extruded";
                    myPart2.Bodies.Add(bd);
                }

                //ABB.Robotics.RobotStudio.Stations.Body [] wireunion = ABB.Robotics.RobotStudio.Stations.Body.JoinCurves(w);

                //foreach (ABB.Robotics.RobotStudio.Stations.Body b1 in wireunion)
                //{
                //    b1.Name = "Semicircle from cirle and line";
                //    myPart2.Bodies.Add(b1);
                //}

                //myPart2.Bodies.Add(wireunion);





                //foreach (ABB.Robotics.RobotStudio.Stations.Body bd in bds)
                //{
                //    //bd.Name = "Face extruded along wire";
                //    myPart.Bodies.Add(bd);
                //}



            }// End try
            
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
            #endregion try
        }

    }
}
