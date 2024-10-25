using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


//
//
//Sample from the help.
//file:///C:/Program%20Files%20(x86)/ABB/SDK/RobotStudio%202022%20SDK/Help/api/ABB.Robotics.RobotStudio.Stations.Body.html
//file:///C:/Program%20Files%20(x86)/ABB/SDK/RobotStudio%202022%20SDK/Help/api/ABB.Robotics.RobotStudio.Stations.Body.html#ABB_Robotics_RobotStudio_Stations_Body_CreateBorderAroundFace_ABB_Robotics_RobotStudio_Stations_Face_
//
//
//Project.UndoContext.BeginUndoStep("BodyAdvancedCurves");
//try
//{
//    Station station = Station.ActiveStation;

//    // Create a part to contain the bodies.
//    #region BodyAdvancedCurvesStep1
//    Part p = new Part();
//    p.Name = "My_Advanced_Curves";
//    station.GraphicComponents.Add(p);
//    #endregion

//    // Create border around face.
//    // First create a box.
//    #region BodyAdvancedCurvesStep2
//    Matrix4 origin = new Matrix4(new Vector3(Axis.X), 0.0);
//    Vector3 size = new Vector3(0.5, 0.5, 0.5);
//    Body box = Body.CreateSolidBox(origin, size);
//    box.Name = "Box";
//    p.Bodies.Add(box);
//    #endregion

//    // Then create the border curve.
//    #region BodyAdvancedCurvesStep3
//    Body b1 = Body.CreateBorderAroundFace(box.Shells[0].Faces[0]);
//    b1.Name = "Face Border";
//    p.Bodies.Add(b1);
//    #endregion

//    // Create border from points.
//    // Create an array of points of the box.
//    #region BodyAdvancedCurvesStep4
//    Vector3 p1 = new Vector3(0.0, 0.0, 0.0);
//    Vector3 p2 = new Vector3(0.5, 0.0, 0.0);
//    Vector3 p3 = new Vector3(0.5, 0.5, 0.0);
//    Vector3 p4 = new Vector3(0.5, 0.5, 0.5);
//    Vector3[] points = new Vector3[4] { p1, p2, p3, p4 };
//    #endregion
//    #region BodyAdvancedCurvesStep5
//    Body b2 = Body.CreateBorderFromPoints(box, points);
//    b2.Name = "Border from Points";
//    p.Bodies.Add(b2);
//    #endregion

//    // Create Intersection Curve.
//    // Create a SolidSphere.
//    #region BodyAdvancedCurvesStep6
//    Body sphere = Body.CreateSolidSphere(new Vector3(0.25, 0.25, 0.25), 0.3);
//    p.Bodies.Add(sphere);
//    #endregion
//    #region BodyAdvancedCurvesStep7
//    Body intersectionCurve = Body.CreateIntersectionCurve(box, sphere);
//    p.Bodies.Add(intersectionCurve);
//    #endregion

//    // Create the same curve using the non static intersection curve method.
//    Body intersectionCurve2 = box.CreateIntersectionCurve(sphere);
//    p.Bodies.Add(intersectionCurve2);
//}
//catch
//{
//    Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
//    throw;
//}
//finally
//{
//    Project.UndoContext.EndUndoStep();
//}



namespace PuimesAddin
{
    internal class TextCreator
    {

        public static void CreateText()
        {

            Project.UndoContext.BeginUndoStep("BodyCreateCurves");
            try
            {
                Station station = Station.ActiveStation;


                Part p = new Part();
                p.Name = "Letter";
                station.GraphicComponents.Add(p);

                string text = "A";

                //Bitmap bmp = new Bitmap(400, 400);
                //GraphicsPath gp = new GraphicsPath();
                //using (Graphics g = Graphics.FromImage(bmp))
                using (Font f = new Font("Tahoma", 40f))

                {
                    foreach (char c in text)
                    {
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddString(c.ToString(), f.FontFamily, (int)f.Style, f.Size, new PointF(0, 0), StringFormat.GenericDefault);
                            PointF[] points = path.PathPoints;
                            byte[] types = path.PathTypes;

                            bool bFirstJump = true;

                            Vector3 vTempStart = new Vector3();

                            Logger.AddMessage(new LogMessage("Character:" + c.ToString(), "Puime's Add-in"));
                            //glyphPathListBox.Items.Add($"Character: {c}");
                            for (int i = 0; i < points.Length; i++)
                            {
                                Logger.AddMessage(new LogMessage("Point: " + i.ToString() + "Type: " + types[i].ToString() + " " + "X: " + points[i].X.ToString() + "Y: " + points[i].Y.ToString(), "Puime's Add-in"));

                                if (types[i] == 0) // StartPoint
                                {
                                    if (bFirstJump==false)
                                    {
                                        vTempStart = (new Vector3 (points[i].X / 1000, points[i].Y / 1000, 0.0));
                                    }
                                }
                                
                                if (types[i] == 1) // Line
                                {
                                    Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                    Vector3 vEnd = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);

                                    Body b1 = Body.CreateLine(vStart, vEnd);
                                    b1.Name = ("Line" + i.ToString());
                                    b1.Color = Color.Red;
                                    p.Bodies.Add(b1);

                                    //i += 1; // Skip the next point as it is part of the current Line segment
                                }

                                if (types[i] == 3) // Bezier curve
                                {
                                    //listBox2.Items.Add($"Bezier Curve Control Points:");

                                    //listBox2.Items.Add($"  Start Point: , X: {points[i - 1].X}, Y: {points[i - 1].Y}");
                                    Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                    //listBox2.Items.Add($"  Control Point 1: , X: {points[i].X}, Y: {points[i].Y}");
                                    //listBox2.Items.Add($"  Control Point 2: , X: {points[i + 1].X}, Y: {points[i + 1].Y}");
                                    Vector3 vCP1 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                    Vector3 vCP2 = new Vector3(points[i+1].X / 1000, points[i+1].Y / 1000, 0.0);
                                    //listBox2.Items.Add($"  End Point: , X: {points[i + 2].X}, Y: {points[i + 2].Y}");
                                    Vector3 vEnd = new Vector3(points[i+2].X / 1000, points[i+2].Y / 1000, 0.0);
                                    i += 2; // Skip the next two points as they are part of the current Bezier segment

                                    Body b1 = Body.CreateLine(vStart, vEnd);
                                    b1.Name = "Line";
                                    b1.Color = Color.Blue;
                                    p.Bodies.Add(b1);

                                    Matrix4 mCP1 = new Matrix4(vCP1);
                                    Matrix4 mCP2 = new Matrix4(vCP2);

                                    Body b2 = Body.CreateCircle(mCP1, 1);
                                    b1.Name = "Circle";
                                    b1.Color = Color.Green;
                                    p.Bodies.Add(b2);

                                    Body b3 = Body.CreateCircle(mCP2, 1);
                                    b1.Name = "Circle";
                                    b1.Color = Color.Green;
                                    p.Bodies.Add(b3);
                                }

                                if (types[i] == 129) // JumpPoint
                                {
                                    Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                    Vector3 vEnd = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);

                                    Body b1 = Body.CreateLine(vStart, vEnd);
                                    b1.Name = ("Line" + i.ToString());
                                    b1.Color = Color.Red;
                                    p.Bodies.Add(b1);

                                    if (bFirstJump)
                                    { 
                                    Vector3 vStart2 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                    Vector3 vEnd2 = new Vector3(points[0].X / 1000, points[0].Y / 1000, 0.0);

                                    Body b1b = Body.CreateLine(vStart2, vEnd2);
                                    b1b.Name = ("Line" + i.ToString());
                                    b1b.Color = Color.Red;
                                    p.Bodies.Add(b1b);
                                    
                                    bFirstJump = false;
                                    }
                                    else
                                    {
                                        Vector3 vStart2 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                        Body b1c = Body.CreateLine(vStart2, vTempStart);
                                        b1c.Name = ("Line" + i.ToString());
                                        b1c.Color = Color.Red;
                                        p.Bodies.Add(b1c);
                                    }
                                }

                                if (types[i] == 161) // EndPoint
                                {

                                    if (bFirstJump==true)
                                    {
                                        Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                        Vector3 vEnd = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);

                                        Body b1 = Body.CreateLine(vStart, vEnd);
                                        b1.Name = ("Line" + i.ToString());
                                        b1.Color = Color.Red;
                                        p.Bodies.Add(b1);

                                        Vector3 vStart2 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                        Vector3 vEnd2 = new Vector3(points[0].X / 1000, points[0].Y / 1000, 0.0);

                                        Body b1b = Body.CreateLine(vStart2, vEnd2);
                                        b1b.Name = ("Line Close");
                                        b1b.Color = Color.Red;
                                        p.Bodies.Add(b1b);
                                    }
                                    else
                                    {
                                        Vector3 vStart = new Vector3(points[i - 1].X / 1000, points[i - 1].Y / 1000, 0.0);
                                        Vector3 vEnd = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);

                                        Body b1 = Body.CreateLine(vStart, vTempStart);
                                        b1.Name = ("Line" + i.ToString());
                                        b1.Color = Color.Red;
                                        p.Bodies.Add(b1);

                                        Vector3 vStart2 = new Vector3(points[i].X / 1000, points[i].Y / 1000, 0.0);
                                        //Vector3 vEnd2 = new Vector3(points[0].X / 1000, points[0].Y / 1000, 0.0);

                                        Body b1b = Body.CreateLine(vStart2, vTempStart);
                                        b1b.Name = ("Line Close with Jump");
                                        b1b.Color = Color.Red;
                                        p.Bodies.Add(b1b);
                                    }


                                    
                                }

                                //string typeDescription = types[i] switch
                                //{
                                //    0 => "Start",
                                //    1 => "Line",
                                //    3 => "Bezier",
                                //    _ => "Unknown"
                                //};
                                //glyphPathListBox.Items.Add($"Type: {typeDescription}, X: {points[i].X}, Y: {points[i].Y}");
                            }
                        }
                    }
                }

                    //{
                    //    g.ScaleTransform(4, 4);
                    //    gp.AddString("a", f.FontFamily, 0, 40f, new Point(0, 0), StringFormat.GenericDefault);
                    //    g.DrawPath(Pens.Gray, gp);
                    //    gp.Flatten(new System.Drawing.Drawing2D.Matrix(), 0.2f);  // <<== *
                    //    g.DrawPath(Pens.DarkSlateBlue, gp);
                    //    for (int i = 0; i < gp.PathPoints.Length -1; i++)
                    //    {
                    //        PointF ptStart = gp.PathPoints[i];
                    //        PointF ptEnd = gp.PathPoints[i+1];
                    //        //g.FillEllipse(Brushes.DarkOrange, p.X - 1, p.Y - 1, 2, 2);

                    //        Vector3 start = new Vector3(ptStart.X / 1000, ptStart.Y / 1000, 0.0);
                    //        Vector3 end = new Vector3(ptEnd.X / 1000, ptEnd.Y / 1000, 0.0);

                    //        Body b5 = Body.CreateLine(start, end);
                    //        b5.Name = "Line";
                    //        p.Bodies.Add(b5);

                    //        Logger.AddMessage(new LogMessage(ptStart.X.ToString() + " - " + ptStart.Y.ToString(), "Puime's Add-in"));


                    //    }
                    //    //pictureBox1.Image = bmp;
                    //}





                    #region help
                    //// Create a part to contain the bodies.
                    //Part p = new Part();
                    //p.Name = "My_Curves";
                    //station.GraphicComponents.Add(p);

                    //////Letter A
                    ////Body A1 = Body.CreateLine(new Vector3(0.0, 0.0, 0.0), new Vector3(0.11, 0.0, 0.0));
                    ////Body A2 = Body.CreateLine(new Vector3(0.11, 0.0, 0.0), new Vector3(0.20, 0.30, 0.0));
                    ////Body A3 = Body.CreateLine(new Vector3(0.20, 0.30, 0.0), new Vector3(0.54, 0.30, 0.0));
                    ////Body A4 = Body.CreateLine(new Vector3(0.54, 0.30, 0.0), new Vector3(0.63, 0.0, 0.0));
                    ////Body A5 = Body.CreateLine(new Vector3(0.63, 0.0, 0.0), new Vector3(0.74, 0.0, 0.0));
                    ////Body A6 = Body.CreateLine(new Vector3(0.74, 0.0, 0.0), new Vector3(0.42, 1.0, 0.0));
                    ////Body A7 = Body.CreateLine(new Vector3(0.42, 1.0, 0.0), new Vector3(0.32, 1.0, 0.0));
                    ////Body A8 = Body.CreateLine(new Vector3(0.32, 1.0, 0.0), new Vector3(0.0, 0.0, 0.0));
                    ////Body A9 = Body.CreateLine(new Vector3(0.23, 0.42, 0.0), new Vector3(0.51, 0.42, 0.0));
                    ////Body A10 = Body.CreateLine(new Vector3(0.51, 0.42, 0.0), new Vector3(0.37, 0.90, 0.0));
                    ////Body A11 = Body.CreateLine(new Vector3(0.37, 0.90, 0.0), new Vector3(0.23, 0.42, 0.0));
                    ////A1.Name = "A1";
                    ////A2.Name = "A2";
                    ////A3.Name = "A3";
                    ////A4.Name = "A4";
                    ////A5.Name = "A5";
                    ////A6.Name = "A6";
                    ////A7.Name = "A7";
                    ////A8.Name = "A8";
                    ////A9.Name = "A9";
                    ////A10.Name = "A10";
                    ////A11.Name = "A11";
                    ////p.Bodies.Add(A1);
                    ////p.Bodies.Add(A2);
                    ////p.Bodies.Add(A3);
                    ////p.Bodies.Add(A4);
                    ////p.Bodies.Add(A5);
                    ////p.Bodies.Add(A6);
                    ////p.Bodies.Add(A7);
                    ////p.Bodies.Add(A8);
                    ////p.Bodies.Add(A9);
                    ////p.Bodies.Add(A10);
                    ////p.Bodies.Add(A11);



                    ////}
                    ////catch
                    ////{
                    ////    Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                    ////    throw;
                    ////}
                    ////finally
                    ////{
                    ////    Project.UndoContext.EndUndoStep();
                    ////}


                    //// Create a wire.
                    //Body wirebody = Body.CreateLine
                    //                (new Vector3(0.0, 0.0, 0.0), new Vector3(1.0, 1.0, 0));
                    //wirebody.Name = "Wirebody";
                    //p.Bodies.Add(wirebody);
                    //Wire w = wirebody.Shells[0].Wires[0];

                    //// Set the sweep option to solid.
                    //SweepOptions so = new SweepOptions();
                    //so.MakeSolid = true;
                    //Vector3 vec = new Vector3(0, 0, 1.0);

                    ////// Create a second wire.
                    ////Body wirebody2 = Body.CreateLine
                    ////                 (new Vector3(0.0, 0.0, 0.0), new Vector3(1.0, 1.0, -1.0));
                    ////wirebody2.Name = "Wirebody 2";
                    ////p.Bodies.Add(wirebody2);
                    ////Wire w2 = wirebody2.Shells[0].Wires[0];

                    //// Extrude the first wire along the second.
                    //Body[] bds2 = Body.Extrude(w, vec, null, so);
                    //foreach (Body bd in bds2)
                    //{
                    //    bd.Name = "Wire extruded along wire";
                    //    p.Bodies.Add(bd);
                    //}

                    ////Project.UndoContext.BeginUndoStep("BodyCreateCurves");
                    ////try
                    ////{
                    ////    Station station = Station.ActiveStation;

                    ////    // Create a part to contain the bodies.
                    ////    Part p = new Part();
                    ////    p.Name = "My_Curves";
                    ////    station.GraphicComponents.Add(p);

                    ////    // Create an arc.
                    ////    Vector3 start = new Vector3(0.0, 0.0, 0.0);
                    ////    Vector3 end = new Vector3(0.5, 0.5, 0.5);
                    ////    Vector3 via = new Vector3(0.25, 0.25, 0.0);
                    ////    Body b1 = Body.CreateArc(start, end, via);
                    ////    b1.Name = "Arc";
                    ////    p.Bodies.Add(b1);

                    ////    // Create a circle.
                    ////    Matrix4 matrix_origo = new Matrix4(new Vector3(Axis.X), 0.0);
                    ////    Body b2 = Body.CreateCircle(matrix_origo, 0.5);
                    ////    b2.Name = "Circle";
                    ////    p.Bodies.Add(b2);

                    ////    // Create an ellipse.
                    ////    Vector3 vector_origo = new Vector3(0.0, 0.0, 0.0);
                    ////    Body b3 = Body.CreateEllipse(vector_origo, end, 0.25);
                    ////    b3.Name = "Ellipse";
                    ////    p.Bodies.Add(b3);

                    ////    // Create an elliptic arc.
                    ////    Body b4 = Body.CreateEllipticArc(vector_origo, end, via, 0.3, 1.1);
                    ////    b4.Name = "Elliptic arc";
                    ////    p.Bodies.Add(b4);

                    ////    // Create a line.
                    ////    Body b5 = Body.CreateLine(start, end);
                    ////    b5.Name = "Line";
                    ////    p.Bodies.Add(b5);

                    ////    // Create a polygon.
                    ////    Body b6 = Body.CreatePolygon(vector_origo, via, 8);
                    ////    b6.Name = "Polygon";
                    ////    p.Bodies.Add(b6);

                    ////    // Create a polyline.
                    ////    Vector3[] vertices = new Vector3[3] { start, via, end };
                    ////    Body b7 = Body.CreatePolyLine(vertices);
                    ////    b7.Name = "Polyline";
                    ////    p.Bodies.Add(b7);

                    ////    // Create a reactangle.
                    ////    Body b8 = Body.CreateRectangle(matrix_origo, 0.7, 0.3);
                    ////    b8.Name = "Reactangle";
                    ////    p.Bodies.Add(b8);

                    ////    // Create a spline.
                    ////    Body b9 = Body.CreateSpline(vertices, 0.2);
                    ////    b9.Name = "Spline";
                    ////    p.Bodies.Add(b9);
                    ///
                    #endregion help

                }
            catch
            {
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                throw;
            }
            finally
            {
                Project.UndoContext.EndUndoStep();
            }


        }

    }
}
