
using System;
using System.Drawing;

using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;
using ABB.Robotics.RobotStudio;

namespace Puime_s_Addin
{
    public class Make_Floor
    {
        static bool val_x_neg = false; // Para saber si el valor es negativo
        static bool val_y_neg = false; // Para saber si el valor es negativo
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
                //bool val_neg = false;
                int x_size = 1;
                int y_size = 1;

                Station station = Station.ActiveStation;



                // Busca entre todos los componentes el mayor/menor valor de X e Y
                #region foreach item in station.GraphicComponents
                foreach (GraphicComponent item in station.GraphicComponents)
                {
                    BoundingBox bbox = item.GetBoundingBox(true);

                    //Logger.AddMessage(new LogMessage(item.ToString() + " bbox.min.x = " + bbox.min.x));

                    // Valor minimo de X
                    if (bbox.min.x < 0) // Valor negativo
                    {
                        if (bbox.min.x < min_x)
                        {
                            min_x = bbox.min.x;
                            val_x_neg = true;

                        //    Logger.AddMessage(new LogMessage(item.ToString() + " min_x negativo - Valor: " + min_x + "val nega :" + val_x_neg));
                        }

                    }

                    else if (bbox.min.x == 0) // Valor "cero"
                    {
                            min_x = 0;  // bbox.min.x;
                            val_x_neg = true;

                    //    Logger.AddMessage(new LogMessage(item.ToString() + " min_x cero - Valor: " + min_x + "val nega :" + val_x_neg));
                    }

                    else if (val_x_neg == false & bbox.min.x < min_x_positivo) // Valor positico
                     //{
                    {
                            min_x = bbox.min.x;
                            min_x_positivo = min_x;

                    //        Logger.AddMessage(new LogMessage(item.ToString() + " min_x positivo - Valor: " + min_x + "val nega :" + val_x_neg));
                    }
                    //}

                    // Valor minimo de Y
                    //if (bbox.min.y < min_y)
                    //{
                    //    min_y = bbox.min.y;
                    //}

                    if (bbox.min.y <= 0) // Valor negativo
                    {
                        if (bbox.min.y < min_y)
                        {
                            min_y = bbox.min.y;
                            val_y_neg = true;
                        }
                    }
                    else // Valor positico
                    {
                        if (val_y_neg == false & bbox.min.y < min_y_positivo)
                        {
                            min_y = bbox.min.y;
                            min_y_positivo = min_y;
                        }
                    }

                    // Valor maximo de X
                    if (bbox.max.x > max_x)
                    {
                        max_x = bbox.max.x;
                    }

                    // Valor maximo de Y
                    if (bbox.max.y > max_y)
                    {
                        max_y = bbox.max.y;
                    }

                    //Logger.AddMessage(new LogMessage(item.ToString() + " - min.x: " + min_x + " - min.y: " +min_y+ " - max.x: " +max_x+ " - max.y: " + max_y ));
                    //Logger.AddMessage(new LogMessage(min_x + ", " + min_y + ", " + max_x + ", " + max_y ));

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
                //Cancel UndoStep
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                Logger.AddMessage(new LogMessage(execption.Message.ToString()));
                throw;
            }
            finally
             {
                //End UndoStep
                Project.UndoContext.EndUndoStep();
             }
        }


        static double obtener_redondeo(double valor)
        {
            double valor_red = valor; //valor que devolvemos
            double valor_ab; //valor absoluto
            bool b_negativo = false;

            if (valor < 0) //numero negativo
            {
                valor_ab = Math.Abs(valor); //valor absoluto de valor
                b_negativo = true;
            }
            else
                valor_ab = valor;


            if (valor_ab <= 1000)
            {
                valor_red = 1000;

                if (b_negativo) //convertirlo a negativo
                {
                    valor_red = valor_red * (-1);
                }

                return valor_red;
            }

            if (valor_ab > 1000)
            {
                double valor_b;
                double valor_c;

                valor_b = valor_ab % 1000; // tres ultimos digitos
                valor_c = 1000 - valor_b; // lo que falta hasta el próxino mil
                valor_red = valor_ab + valor_c; // sumamos todo

                if (b_negativo)  //convertirlo a negativo
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
            

            // Create a solid box.
            //Matrix4 matrix_origo = new Matrix4(new Vector3(Axis.X), 0.0);
            //Matrix4 matrix_origo = new Matrix4(new Vector3 (x_pos, y_pos, -10));

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
            //Color box_color = Color.FromArgb(128, 128, 255);
            //b1.Color = box_color;
            b1.Color = Color.FromArgb(128, 128, 255);
            p.Bodies.Add(b1);

            // Define the floor size
            RectangleF floor_rect = new RectangleF(0F, 0F, 3000F, 3000F);

            val_x_neg = false;
            val_y_neg = false;

            //Logger.AddMessage(new LogMessage(position + ", " + matrix_origo + ", " + size ));
        }

    }
}
