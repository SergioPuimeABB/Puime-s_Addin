using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

                // Create a part to contain the bodies.
                Part p = new Part();
                p.Name = "My_Curves";
                station.GraphicComponents.Add(p);

                ////Letter A
                //Body A1 = Body.CreateLine(new Vector3(0.0, 0.0, 0.0), new Vector3(0.11, 0.0, 0.0));
                //Body A2 = Body.CreateLine(new Vector3(0.11, 0.0, 0.0), new Vector3(0.20, 0.30, 0.0));
                //Body A3 = Body.CreateLine(new Vector3(0.20, 0.30, 0.0), new Vector3(0.54, 0.30, 0.0));
                //Body A4 = Body.CreateLine(new Vector3(0.54, 0.30, 0.0), new Vector3(0.63, 0.0, 0.0));
                //Body A5 = Body.CreateLine(new Vector3(0.63, 0.0, 0.0), new Vector3(0.74, 0.0, 0.0));
                //Body A6 = Body.CreateLine(new Vector3(0.74, 0.0, 0.0), new Vector3(0.42, 1.0, 0.0));
                //Body A7 = Body.CreateLine(new Vector3(0.42, 1.0, 0.0), new Vector3(0.32, 1.0, 0.0));
                //Body A8 = Body.CreateLine(new Vector3(0.32, 1.0, 0.0), new Vector3(0.0, 0.0, 0.0));
                //Body A9 = Body.CreateLine(new Vector3(0.23, 0.42, 0.0), new Vector3(0.51, 0.42, 0.0));
                //Body A10 = Body.CreateLine(new Vector3(0.51, 0.42, 0.0), new Vector3(0.37, 0.90, 0.0));
                //Body A11 = Body.CreateLine(new Vector3(0.37, 0.90, 0.0), new Vector3(0.23, 0.42, 0.0));
                //A1.Name = "A1";
                //A2.Name = "A2";
                //A3.Name = "A3";
                //A4.Name = "A4";
                //A5.Name = "A5";
                //A6.Name = "A6";
                //A7.Name = "A7";
                //A8.Name = "A8";
                //A9.Name = "A9";
                //A10.Name = "A10";
                //A11.Name = "A11";
                //p.Bodies.Add(A1);
                //p.Bodies.Add(A2);
                //p.Bodies.Add(A3);
                //p.Bodies.Add(A4);
                //p.Bodies.Add(A5);
                //p.Bodies.Add(A6);
                //p.Bodies.Add(A7);
                //p.Bodies.Add(A8);
                //p.Bodies.Add(A9);
                //p.Bodies.Add(A10);
                //p.Bodies.Add(A11);



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


                // Create a wire.
                Body wirebody = Body.CreateLine
                                (new Vector3(0.0, 0.0, 0.0), new Vector3(1.0, 1.0, 0));
                wirebody.Name = "Wirebody";
                p.Bodies.Add(wirebody);
                Wire w = wirebody.Shells[0].Wires[0];

                // Set the sweep option to solid.
                SweepOptions so = new SweepOptions();
                so.MakeSolid = true;
                Vector3 vec = new Vector3(0, 0, 1.0);

                //// Create a second wire.
                //Body wirebody2 = Body.CreateLine
                //                 (new Vector3(0.0, 0.0, 0.0), new Vector3(1.0, 1.0, -1.0));
                //wirebody2.Name = "Wirebody 2";
                //p.Bodies.Add(wirebody2);
                //Wire w2 = wirebody2.Shells[0].Wires[0];

                // Extrude the first wire along the second.
                Body[] bds2 = Body.Extrude(w, vec, null, so);
                foreach (Body bd in bds2)
                {
                    bd.Name = "Wire extruded along wire";
                    p.Bodies.Add(bd);
                }

                //Project.UndoContext.BeginUndoStep("BodyCreateCurves");
                //try
                //{
                //    Station station = Station.ActiveStation;

                //    // Create a part to contain the bodies.
                //    Part p = new Part();
                //    p.Name = "My_Curves";
                //    station.GraphicComponents.Add(p);

                //    // Create an arc.
                //    Vector3 start = new Vector3(0.0, 0.0, 0.0);
                //    Vector3 end = new Vector3(0.5, 0.5, 0.5);
                //    Vector3 via = new Vector3(0.25, 0.25, 0.0);
                //    Body b1 = Body.CreateArc(start, end, via);
                //    b1.Name = "Arc";
                //    p.Bodies.Add(b1);

                //    // Create a circle.
                //    Matrix4 matrix_origo = new Matrix4(new Vector3(Axis.X), 0.0);
                //    Body b2 = Body.CreateCircle(matrix_origo, 0.5);
                //    b2.Name = "Circle";
                //    p.Bodies.Add(b2);

                //    // Create an ellipse.
                //    Vector3 vector_origo = new Vector3(0.0, 0.0, 0.0);
                //    Body b3 = Body.CreateEllipse(vector_origo, end, 0.25);
                //    b3.Name = "Ellipse";
                //    p.Bodies.Add(b3);

                //    // Create an elliptic arc.
                //    Body b4 = Body.CreateEllipticArc(vector_origo, end, via, 0.3, 1.1);
                //    b4.Name = "Elliptic arc";
                //    p.Bodies.Add(b4);

                //    // Create a line.
                //    Body b5 = Body.CreateLine(start, end);
                //    b5.Name = "Line";
                //    p.Bodies.Add(b5);

                //    // Create a polygon.
                //    Body b6 = Body.CreatePolygon(vector_origo, via, 8);
                //    b6.Name = "Polygon";
                //    p.Bodies.Add(b6);

                //    // Create a polyline.
                //    Vector3[] vertices = new Vector3[3] { start, via, end };
                //    Body b7 = Body.CreatePolyLine(vertices);
                //    b7.Name = "Polyline";
                //    p.Bodies.Add(b7);

                //    // Create a reactangle.
                //    Body b8 = Body.CreateRectangle(matrix_origo, 0.7, 0.3);
                //    b8.Name = "Reactangle";
                //    p.Bodies.Add(b8);

                //    // Create a spline.
                //    Body b9 = Body.CreateSpline(vertices, 0.2);
                //    b9.Name = "Spline";
                //    p.Bodies.Add(b9);
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
