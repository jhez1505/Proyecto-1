using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_1
{
    public partial class Form1 : Form
    {
        double num1 = 0, num2 = 0, resultado = 0;
        string operacion = "";
        bool nuevaOperacion = false;
        string conexionString = @"Server=(local);Database=HistorialOperaciones;Trusted_Connection=True;";


        void GuardarHistorial(string operacion, double resultado)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    string sql = "INSERT INTO HistorialOperaciones (Operacion, Resultado) VALUES (@op, @res)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@op", operacion);
                    cmd.Parameters.AddWithValue("@res", resultado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (nuevaOperacion)
            {
                Pantalla.Clear();
                nuevaOperacion = false;
            }
            Pantalla.Text += btn.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pantalla.Clear();
            num1 = num2 = resultado = 0;
            operacion = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Pantalla.Clear();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (Pantalla.Text != "")
            {
                if (operacion != "")
                    button18_Click(null, null);

                num1 = double.Parse(Pantalla.Text);
                operacion = btn.Text;
                Pantalla.Text += " " + operacion + " ";
                nuevaOperacion = false;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (Pantalla.Text != "")
            {
                if (operacion != "")
                    button18_Click(null, null);

                num1 = double.Parse(Pantalla.Text);
                operacion = btn.Text;
                Pantalla.Text += " " + operacion + " ";
                nuevaOperacion = false;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (Pantalla.Text != "")
            {
                if (operacion != "")
                    button18_Click(null, null);

                num1 = double.Parse(Pantalla.Text);
                operacion = btn.Text;
                Pantalla.Text += " " + operacion + " ";
                nuevaOperacion = false;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (Pantalla.Text != "")
            {
                if (operacion != "")
                    button18_Click(null, null);

                num1 = double.Parse(Pantalla.Text);
                operacion = btn.Text;
                Pantalla.Text += " " + operacion + " ";
                nuevaOperacion = false;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (Pantalla.Text != "")
            {
                num1 = double.Parse(Pantalla.Text);
                resultado = Math.Pow(num1, 2);
                Pantalla.Text = resultado.ToString();
                GuardarHistorial($"{num1}²", resultado);
                nuevaOperacion = true;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (Pantalla.Text != "")
            {
                num1 = double.Parse(Pantalla.Text);
                resultado = Math.Sqrt(num1);
                Pantalla.Text = resultado.ToString();
                GuardarHistorial($"√{num1}", resultado);
                nuevaOperacion = true;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = Pantalla.Text;
                string[] partes = texto.Split(new char[] { '+', '-', '*', '/' });

                if (partes.Length >= 2)
                {
                    string num1Txt = partes[0].Trim();
                    string num2Txt = partes[1].Trim();

                    if (double.TryParse(num1Txt, out num1) && double.TryParse(num2Txt, out num2))
                    {
                        switch (operacion)
                        {
                            case "+": resultado = num1 + num2; break;
                            case "-": resultado = num1 - num2; break;
                            case "*": resultado = num1 * num2; break;
                            case "/":
                                if (num2 != 0)
                                    resultado = num1 / num2;
                                else
                                {
                                    MessageBox.Show("No se puede dividir entre 0");
                                    return;
                                }
                                break;
                        }

                        Pantalla.Text = resultado.ToString();
                        GuardarHistorial($"{num1} {operacion} {num2}", resultado);
                        nuevaOperacion = true;
                        operacion = "";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error en la operación");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!Pantalla.Text.EndsWith("."))
                Pantalla.Text += ".";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conexionString);
                string sql = "SELECT * FROM HistorialOperaciones ORDER BY Fecha DESC";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                string texto = "";
                foreach (DataRow fila in dt.Rows)
                {
                    texto += fila["Fecha"] + ": " + fila["Operacion"] + " = " + fila["Resultado"] + "\n";
                }

                if (texto == "")
                    texto = "No hay operaciones guardadas.";

                MessageBox.Show(texto, "Historial de Operaciones");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar historial: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
