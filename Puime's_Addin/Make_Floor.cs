using System;
using System.Drawing;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio;

namespace Puime_s_Addin
{
    public class Make_Floor
    {
        static bool val_x_neg = false; // Used to check if the value is negative
        static bool val_y_neg = false; // Used to check if the value is negative
        public static void ObtenerObjetosEstacion()
        {
          //Begin UndoStep
          Project.UndoContext.BeginUndoStep("RotateBasedOnAxis");
            try
            {
                double min_x = 0;
                double min_x_positivo = 1000000;
                double min_y = 0;
                double min_y_positivo = 1000000;
                double max_x = 0;
                double max_y = 0;
                int x_size = 1;
                int y_size = 1;

                Station station = Station.ActiveStation;

                // Search in all the station components the major/minor X, Y value
                #region foreach item in station.GraphicComponents
                foreach (GraphicComponent item in station.GraphicComponents)
                {
                    BoundingBox bbox = item.GetBoundingBox(true);

                    // Minor X value
                    if (bbox.min.x < 0) // Negative value
                    {
                        if (bbox.min.x < min_x)
                        {
                            min_x = bbox.min.x;
                            val_x_neg = true;
                        }

                    }

                    else if (bbox.min.x == 0) // "0" value
                    {
                            min_x = 0;
                            val_x_neg = true;
                    }

                    else if (val_x_neg == false & bbox.min.x < min_x_positivo) // Positive value
                    {
                            min_x = bbox.min.x;
                            min_x_positivo = min_x;
                    }
                    
                    if (bbox.min.y <= 0) // Negative value
                    {
                        if (bbox.min.y < min_y)
                        {
                            min_y = bbox.min.y;
                            val_y_neg = true;
                        }
                    }
                    else // Positive value
                    {
                        if (val_y_neg == false & bbox.min.y < min_y_positivo)
                        {
                            min_y = bbox.min.y;
                            min_y_positivo = min_y;
                        }
                    }

                    // Maximum X value
                    if (bbox.max.x > max_x)
                    {
                        max_x = bbox.max.x;
                    }

                    // Y maximum value
                    if (bbox.max.y > max_y)
                    {
                        max_y = bbox.max.y;
                    }
                }
                #endregion

                double min_x_red = obtener_redondeo(Math.Floor(min_x * 1000));
                double min_y_red = obtener_redondeo(Math.Floor(min_y * 1000));
                double max_x_red = obtener_redondeo(Math.Floor(max_x * 1000));
                double max_y_red = obtener_redondeo(Math.Floor(max_y * 1000));

                if (val_x_neg)
                {
                    x_size = Math.Abs(Convert.ToInt32(max_x_red)) + Math.Abs(Convert.ToInt32(min_x_red));
                }
                else
                {
                    x_size = Math.Abs(Convert.ToInt32(max_x_red)) - Math.Abs(Convert.ToInt32(min_x_red));
                }


                if (val_y_neg)
                {
                    y_size = Math.Abs(Convert.ToInt32(max_y_red)) + Math.Abs(Convert.ToInt32(min_y_red));
                }
                else
                {
                    y_size = Math.Abs(Convert.ToInt32(max_y_red)) - Math.Abs(Convert.ToInt32(min_y_red));
                }

                crear_box(min_x_red / 1000, min_y_red / 1000, x_size / 1000, y_size / 1000);

             }

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


        static double obtener_redondeo(double valor)
        {
            double valor_red = valor; // value to return
            double valor_ab; // absolute value
            bool b_negativo = false;

            if (valor < 0) // negative number
            {
                valor_ab = Math.Abs(valor); // Absolute value
                b_negativo = true;
            }
            else
                valor_ab = valor;


            if (valor_ab <= 1000)
            {
                valor_red = 1000;

                if (b_negativo) // convert to negative
                {
                    valor_red = valor_red * (-1);
                }

                return valor_red;
            }

            if (valor_ab > 1000)
            {
                double valor_b;
                double valor_c;

                valor_b = valor_ab % 1000; // the three last digits
                valor_c = 1000 - valor_b; // what it rest until the next thousand
                valor_red = valor_ab + valor_c; // all added

                if (b_negativo)  // convert to negative
                {
                    valor_red = valor_red * (-1);
                }

                return valor_red;
            }

            return valor_red;
        }



        static void crear_box(double x_pos, double y_pos, int val_x, int val_y)
        {
            Station station = Station.ActiveStation;
            if (station == null) return;

            // Create a part to contain the bodies.
            Part p = new Part();
            p.Name = "Floor";
            station.GraphicComponents.Add(p);
            
            Vector3 position = new Vector3();
            Vector3 size = new Vector3();

            if (val_x_neg & val_y_neg)
            {
                position = new Vector3(x_pos - 1, y_pos - 1, -0.01);
                size = new Vector3(val_x + 2, val_y + 2, 0.01);
            }

            else if (!val_x_neg & val_y_neg)
            {
                position = new Vector3(x_pos - 2, y_pos - 1, -0.01);
                size = new Vector3(val_x + 3, val_y + 2, 0.01);
            }

            else if (val_x_neg & !val_y_neg)
            {
                position = new Vector3(x_pos - 1, y_pos - 2, -0.01);
                size = new Vector3(val_x + 2, val_y + 3, 0.01);
            }

            else if (!val_x_neg & !val_y_neg)
            {
                position = new Vector3(x_pos - 2, y_pos - 2, -0.01);
                size = new Vector3(val_x + 3, val_y + 3, 0.01);
            }


            Matrix4 matrix_origo = new Matrix4(position);
            Body b1 = Body.CreateSolidBox(matrix_origo, size);
            b1.Name = "Box";
            b1.Color = Color.FromArgb(215, 215, 215);
            p.Bodies.Add(b1);

            // Define the floor size
            RectangleF floor_rect = new RectangleF(0F, 0F, 3000F, 3000F);

            val_x_neg = false;
            val_y_neg = false;
        }
    }
}
