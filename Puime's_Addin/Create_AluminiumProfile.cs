using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

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
                //myPart.Name = "20Profile";
                myPart.DisconnectFromLibrary();
                //stn.GraphicComponents.Add(myPart);
                ProfileLib.Close();



                Wire MyWire2=null;

                foreach (var item in myPart.Bodies)
                {
                    Body MyBody = item as Body;
                    if (MyBody != null)
                    {
                        MyWire2 = MyBody.Shells[0].Wires[0];
                    }
                }

                Vector3 projection = new Vector3(0.0, 0.0, 0.1);
                Wire alongWire = null;

                SweepOptions sweepOptions = new SweepOptions();
                sweepOptions.MakeSolid = true;
                //Body[] array = null;
                Part part = new Part(); //First step
                Part part2 = new Part(); //Scond step
                Part part3 = new Part(); //Final step
                part3.Name = "20Profile";

                Body[] bodyarray = Body.Extrude(MyWire2, projection, alongWire, sweepOptions);
                if (bodyarray.Length != 0)
                {
                    //Fist step
                    foreach (Body bbody in bodyarray)
                    {
                        bbody.Name = "Body1";
                        part.Bodies.Add(bbody);
                        
                        Body bbodycopy2= (Body)bbody.Copy();
                        bbodycopy2.Name = "Body2";
                        bbodycopy2.Transform.RZ = Globals.DegToRad(90);
                        part.Bodies.Add(bbodycopy2);

                        Body bbodycopy3 = (Body)bbody.Copy();
                        bbodycopy3.Name = "Body3";
                        bbodycopy3.Transform.RZ = Globals.DegToRad(180);
                        part.Bodies.Add(bbodycopy3);

                        Body bbodycopy4 = (Body)bbody.Copy();
                        bbodycopy4.Name = "Body4";
                        bbodycopy4.Transform.RZ = Globals.DegToRad(270);
                        part.Bodies.Add(bbodycopy4);

                        //Scond step
                        Body[] b1 = bbody.Join(bbodycopy2, false);
                        foreach (Body b11 in b1)
                        {
                            b11.Name = "Body1";
                            part2.Bodies.Add(b11);
                        }
                        
                        Body[] b2 = bbodycopy3.Join(bbodycopy4, false);
                        foreach (Body b12 in b2)
                        {
                            b12.Name = "Body2";
                            part2.Bodies.Add(b12);
                        }

                        //Final step
                        Body[] b7 = b1[0].Join(b2[0], false);
                        foreach (Body b in b7)
                        {
                            b.Name = "Body2";
                            b.Color = Color.FromArgb(224, 224, 224);
                            part3.Bodies.Add(b);
                        }

                    }

                    stn.GraphicComponents.Remove(part);
                    stn.GraphicComponents.Remove(part2);
                    stn.GraphicComponents.Add(part3);
                    return;
                }


















                //ABB.Robotics.RobotStudio.Stations.Body b = myPart2.Bodies.FirstOrDefault();// myPart.Bodies.First();

                //myPart2.Bodies.Add(b);

                //Face f = b.Shells[0].Faces[0];

                //// Create a wire.
                //ABB.Robotics.RobotStudio.Stations.Body wirebody = ABB.Robotics.RobotStudio.Stations.Body.CreateLine
                //                (new Vector3(0.0, 0.0, 0.0), new Vector3(0, 0, 1));
                //wirebody.Name = "Wirebody";
                //myPart2.Bodies.Add(wirebody);

                //Wire w = wirebody.Shells[0].Wires[0];

                //// Set the sweep option to solid.
                //SweepOptions so = new SweepOptions();
                //so.MakeSolid = true;

                //Vector3 vec = new Vector3(0, 0, 1);



                //ABB.Robotics.RobotStudio.Stations.Body[] bds = ABB.Robotics.RobotStudio.Stations.Body.Extrude(f, vec, w, so);
                //foreach (ABB.Robotics.RobotStudio.Stations.Body bd in bds)
                //{
                //    bd.Name = "Wire extruded";
                //    myPart2.Bodies.Add(bd);
                //}

                //ABB.Robotics.RobotStudio.Stations.Body [] wireunion = ABB.Robotics.RobotStudio.Stations.Body.JoinCurves(w);

                //foreach (ABB.Robotics.RobotStudio.Stations.Body b1 in wireunion)
                //{
                //    b1.Name = "Semicircle from cirle and line";
                //    myPart2.Bodies.Add(b1);
                //}

                //myPart2.Bodies.Add(wireunion);





                ////foreach (ABB.Robotics.RobotStudio.Stations.Body bd in bds)
                ////{
                ////    //bd.Name = "Face extruded along wire";
                ////    myPart.Bodies.Add(bd);
                ////}



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
