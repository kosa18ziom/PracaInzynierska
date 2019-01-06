using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Praca_Inżynierska
{
    public partial class Planowanie : System.Web.UI.Page
    {
        //int count = 0;
        //static string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-Praca_Inżynierska-20181230023417.mdf; Initial Catalog = aspnet - Praca_Inżynierska - 20181230023417; Integrated Security = True";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.IsPostBack)
            {
                string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Przedmioty ORDER BY nazwa_Przedmiotu ASC"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        ddlCustomers.DataSource = cmd.ExecuteReader();
                        ddlCustomers.DataTextField = "nazwa_Przedmiotu";
                        ddlCustomers.DataValueField = "Id_Przedmiotu";
                        ddlCustomers.DataBind();
                        con.Close();
                    }
                }

                //Add blank item at index 0.
                ddlCustomers.Items.Insert(0, new ListItem("Wybierz Przedmiot"));
            }

        }

        protected void ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {

                setSubjectTableWyklad();
                setSubjectTableCwiczenia();
                setSubjectTableLab();
                setSubjectTableProjekt();
                setSubjectTableSem();
                setddlgrupy();
                ddlCustomers.Items.Remove(ddlCustomers.Items.FindByValue("Wybierz Przedmiot"));
            Planuj.Visible = true;
            CheckIfWykladPlanned();
            CheckIfCwPlanned();
            CheckIfLabPlanned();
            CheckIfProjPlanned();
            CheckIfSemPlanned();
            addwykladh();
            addcwh();
            addlabh();
            addprojh();
            addsemh();
        }

        protected void ddlgrupy_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckIfWykladPlanned();
            CheckIfCwPlanned();
            CheckIfLabPlanned();
            CheckIfProjPlanned();
            CheckIfSemPlanned();
        }

        protected void ddlNauczyciel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void addwykladh()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT wyklad FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    con.Open();
                    wykladh.Text = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
            }
        }
        protected void addcwh()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT cwiczenia FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    con.Open();
                    cwh.Text = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
            }
        }

        protected void addlabh()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT laboratorium FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    con.Open();
                    labh.Text = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
            }
        }

        protected void addprojh()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT projekt FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    con.Open();
                    projh.Text = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
            }
        }

        protected void addsemh()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT seminarium FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId "))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    con.Open();
                    semh.Text = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
            }
        }
        protected void setddlgrupy()
        {
            
                string przedmiotId = ddlCustomers.SelectedValue;
                string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Distinct G.Id_Grupy,G.Nazwa_Grupy  from Grupy G inner join Plan_Studiow P on G.Id_Planu = P.Id_Planu inner join Przedmioty Pr on P.Id_Planu = Pr.Id_Planu WHERE G.Id_Planu = Pr.Id_Planu"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                        con.Open();
                        ddlgrupy.DataSource = cmd.ExecuteReader();
                        ddlgrupy.DataTextField = "Nazwa_Grupy";
                        ddlgrupy.DataValueField = "Id_Grupy";
                        ddlgrupy.DataBind();
                        con.Close();
                    }
                }
            
        }

        protected void CheckIfWykladPlanned()
        {
            int przedmiotId = Convert.ToInt32(ddlCustomers.SelectedValue);
            int GrupaId = Convert.ToInt32(ddlgrupy.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Zajecia where typ_zajec='wyklad'"))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.Parameters.AddWithValue("@GrupaId", GrupaId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read()) 
                        {
                            if (reader.GetInt32(reader.GetOrdinal("Id_Przedmiotu")) == przedmiotId)
                            {
                                if (reader.GetInt32(reader.GetOrdinal("Id_Grupy")) == GrupaId)
                                {
                                    Zaplanowano.Visible = true;
                                    Niezaplanowane.Visible = false;
                                    Button1.Enabled = false;
                                    ddlNauczyciel.Enabled = false;
                                }
                                else
                                {
                                    Zaplanowano.Visible = false;
                                    Niezaplanowane.Visible = true;
                                    Button1.Enabled = true;
                                    ddlNauczyciel.Enabled = true;
                                }
                            }
                            else if (reader.GetInt32(reader.GetOrdinal("Id_Przedmiotu")) != przedmiotId)
                            {
                                Zaplanowano.Visible = false;
                                Niezaplanowane.Visible = true;
                                Button1.Enabled = true;
                                ddlNauczyciel.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        protected void CheckIfCwPlanned()
        {
            int przedmiotId = Convert.ToInt32(ddlCustomers.SelectedValue);
            int GrupaId = Convert.ToInt32(ddlgrupy.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Zajecia where typ_zajec='cwiczenia' "))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(reader.GetOrdinal("Id_Przedmiotu")) == przedmiotId)
                            {
                                if (reader.GetInt32(reader.GetOrdinal("Id_Grupy")) == GrupaId)
                                {
                                    Zaplanowano1.Visible = true;
                                    Niezaplanowane1.Visible = false;
                                    Button2.Enabled = false;
                                    ddlNauczyciel1.Enabled = false;
                                }
                                else
                                {
                                    Zaplanowano1.Visible = false;
                                    Niezaplanowane1.Visible = true;
                                    Button2.Enabled = true;
                                    ddlNauczyciel1.Enabled = true;
                                }
                            }
                            else if (reader.GetInt32(reader.GetOrdinal("Id_Przedmiotu")) != przedmiotId)
                            {
                                Zaplanowano1.Visible = false;
                                Niezaplanowane1.Visible = true;
                                Button2.Enabled = true;
                                ddlNauczyciel1.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        protected void CheckIfLabPlanned()
        {
            int przedmiotId = Convert.ToInt32(ddlCustomers.SelectedValue);
            int GrupaId = Convert.ToInt32(ddlgrupy.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id,Id_Grupy,Id_Przedmiotu,Id_Nauczyciela,typ_zajec,semestr,ilosc_godzin FROM Zajecia where typ_zajec='laboratorium' AND Id_Przedmiotu=@przedmiotId "))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                                if (reader.GetInt32(reader.GetOrdinal("Id_Grupy")) == GrupaId)
                                {
                                    Zaplanowano2.Visible = true;
                                    Niezaplanowane2.Visible = false;
                                    Button3.Enabled = false;
                                    ddlNauczyciel2.Enabled = false;
                                }
                                else
                                {
                                    Zaplanowano2.Visible = false;
                                    Niezaplanowane2.Visible = true;
                                    Button3.Enabled = true;
                                    ddlNauczyciel2.Enabled = true;
                                }
                        }
                    }
                }
            }
        }

        protected void CheckIfProjPlanned()
        {
            int przedmiotId = Convert.ToInt32(ddlCustomers.SelectedValue);
            int GrupaId = Convert.ToInt32(ddlgrupy.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Zajecia where typ_zajec='projekt' "))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(reader.GetOrdinal("Id_Przedmiotu")) == przedmiotId)
                            {
                                if (reader.GetInt32(reader.GetOrdinal("Id_Grupy")) == GrupaId)
                                {
                                    Zaplanowano3.Visible = true;
                                    Niezaplanowane3.Visible = false;
                                    Button4.Enabled = false;
                                    ddlNauczyciel3.Enabled = false;
                                }
                                else
                                {
                                    Zaplanowano3.Visible = false;
                                    Niezaplanowane3.Visible = true;
                                    Button4.Enabled = true;
                                    ddlNauczyciel3.Enabled = true;
                                }
                            }
                            else if (reader.GetInt32(reader.GetOrdinal("Id_Przedmiotu")) != przedmiotId)
                            {
                                Zaplanowano3.Visible = false;
                                Niezaplanowane3.Visible = true;
                                Button4.Enabled = true;
                                ddlNauczyciel3.Enabled = true;

                            }
                        }
                    }
                }
            }
        }

        protected void CheckIfSemPlanned()
        {
            int przedmiotId = Convert.ToInt32(ddlCustomers.SelectedValue);
            int GrupaId = Convert.ToInt32(ddlgrupy.SelectedValue);
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Zajecia where typ_zajec='seminarium' "))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(reader.GetOrdinal("Id_Przedmiotu")) == przedmiotId)
                            {
                                if (reader.GetInt32(reader.GetOrdinal("Id_Grupy")) == GrupaId)
                                {
                                    Zaplanowano4.Visible = true;
                                    Niezaplanowane4.Visible = false;
                                    Button5.Enabled = false;
                                    ddlNauczyciel4.Enabled = false;
                                }
                                else
                                {
                                    Zaplanowano4.Visible = false;
                                    Niezaplanowane4.Visible = true;
                                    Button5.Enabled = true;
                                    ddlNauczyciel4.Enabled = true;
                                }
                            }
                            else if (reader.GetInt32(reader.GetOrdinal("Id_Przedmiotu")) != przedmiotId)
                            {
                                Zaplanowano4.Visible = false;
                                Niezaplanowane4.Visible = true;
                                Button5.Enabled = true;
                                ddlNauczyciel4.Enabled = true;

                            }
                        }
                    }
                }
            }
        }
        protected void setSubjectTableWyklad()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT wyklad FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId"))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.GetInt32(reader.GetOrdinal("wyklad")) == 0)
                        {
                            Button1.Enabled = false;
                            ddlNauczyciel.Enabled = false;
                        }
                        else
                        {
                            Button1.Enabled = true;
                            ddlNauczyciel.Enabled = true;
                        }
                    }

                    con.Close();
                }
            }
        }

        protected void setSubjectTableCwiczenia()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT cwiczenia FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId"))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.GetInt32(reader.GetOrdinal("cwiczenia")) == 0)
                        {
                            Button2.Enabled = false;
                            ddlNauczyciel1.Enabled = false;
                        }
                        else
                        {
                            Button2.Enabled = true;
                            ddlNauczyciel1.Enabled = true;
                        }
                    }
                    con.Close();
                }
            }
        }

        protected void setSubjectTableLab()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT laboratorium FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId"))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.GetInt32(reader.GetOrdinal("laboratorium")) == 0)
                        {
                            Button3.Enabled = false;
                            ddlNauczyciel2.Enabled = false;
                        }
                        else
                        {
                            Button3.Enabled = true;
                            ddlNauczyciel2.Enabled = true;
                        }
                    }
                
                    con.Close();
                }
            }
        }

        protected void setSubjectTableProjekt()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT projekt FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId"))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.GetInt32(reader.GetOrdinal("projekt")) == 0)
                        {
                            Button4.Enabled = false;
                            ddlNauczyciel3.Enabled = false;
                        }
                        else
                        {
                            Button4.Enabled = true;
                            ddlNauczyciel3.Enabled = true;
                        }
                    }

                    con.Close();
                }
            }
        }
        protected void setSubjectTableSem()
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT seminarium FROM Przedmioty WHERE Id_Przedmiotu = @przedmiotId"))
                {
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.GetInt32(reader.GetOrdinal("seminarium")) == 0)
                        {
                            Button5.Enabled = false;
                            ddlNauczyciel4.Enabled = false;
                        }
                        else
                        {
                            Button5.Enabled = true;
                            ddlNauczyciel4.Enabled = false;
                        }
                    }

                    con.Close();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string grupaId = ddlgrupy.SelectedValue;
            string nauczycielId = ddlNauczyciel.SelectedValue;
            string wyklad = "Wyklad";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "INSERT INTO Zajecia(Id_Grupy,Id_Przedmiotu,Id_Nauczyciela,typ_zajec,semestr,ilosc_godzin) VALUES(@grupaId,@przedmiotId,@nauczycielId,@wyklad,(Select semestr from Przedmioty WHERE Id_Przedmiotu=@przedmiotId),(Select wyklad from Przedmioty where Id_Przedmiotu = @przedmiotId))";
            string Updatequery1 = "Update AspNetUsers SET ZaplanowaneGodziny = (ZaplanowaneGodziny + (Select wyklad from Przedmioty where Id_Przedmiotu = @przedmiotId)) where Id=@nauczycielId ";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@grupaId", grupaId);
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd.Parameters.AddWithValue("@wyklad", wyklad);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                using (SqlCommand cmd2 = new SqlCommand(Updatequery1))
                {
                    cmd2.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd2.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd2.Connection = con;
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
            CheckIfWykladPlanned();
            CheckIfCwPlanned();
            CheckIfLabPlanned();
            CheckIfProjPlanned();
            CheckIfSemPlanned();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string grupaId = ddlgrupy.SelectedValue;
            string nauczycielId = ddlNauczyciel1.SelectedValue;
            string cwiczenia = "Cwiczenia";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "INSERT INTO Zajecia(Id_Grupy,Id_Przedmiotu,Id_Nauczyciela,typ_zajec,semestr,ilosc_godzin) VALUES(@grupaId,@przedmiotId,@nauczycielId,@cwiczenia,(Select semestr from Przedmioty WHERE Id_Przedmiotu=@przedmiotId),(Select cwiczenia from Przedmioty where Id_Przedmiotu = @przedmiotId))";
            string Updatequery1 = "Update AspNetUsers SET ZaplanowaneGodziny = (ZaplanowaneGodziny + (Select cwiczenia from Przedmioty where Id_Przedmiotu = @przedmiotId)) where Id=@nauczycielId ";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@grupaId", grupaId);
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd.Parameters.AddWithValue("@cwiczenia", cwiczenia);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                
                using (SqlCommand cmd2 = new SqlCommand(Updatequery1))
                {
                    cmd2.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd2.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd2.Connection = con;
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
            CheckIfWykladPlanned();
            CheckIfCwPlanned();
            CheckIfLabPlanned();
            CheckIfProjPlanned();
            CheckIfSemPlanned();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string grupaId = ddlgrupy.SelectedValue;
            string nauczycielId = ddlNauczyciel2.SelectedValue;
            string lab = "Laboratorium";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "INSERT INTO Zajecia(Id_Grupy,Id_Przedmiotu,Id_Nauczyciela,typ_zajec,semestr,ilosc_godzin) VALUES(@grupaId,@przedmiotId,@nauczycielId,@lab,(Select semestr from Przedmioty WHERE Id_Przedmiotu=@przedmiotId),(Select laboratorium from Przedmioty where Id_Przedmiotu = @przedmiotId))";
            string Updatequery1 = "Update AspNetUsers SET ZaplanowaneGodziny = (ZaplanowaneGodziny + (Select laboratorium from Przedmioty where Id_Przedmiotu = @przedmiotId)) where Id=@nauczycielId ";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@grupaId", grupaId);
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd.Parameters.AddWithValue("@lab", lab);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
               
                using (SqlCommand cmd2 = new SqlCommand(Updatequery1))
                {
                    cmd2.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd2.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd2.Connection = con;
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
            CheckIfWykladPlanned();
            CheckIfCwPlanned();
            CheckIfLabPlanned();
            CheckIfProjPlanned();
            CheckIfSemPlanned();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string grupaId = ddlgrupy.SelectedValue;
            string nauczycielId = ddlNauczyciel3.SelectedValue;
            string projekt = "Projekt";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "INSERT INTO Zajecia(Id_Grupy,Id_Przedmiotu,Id_Nauczyciela,typ_zajec,semestr,ilosc_godzin) VALUES(@grupaId,@przedmiotId,@nauczycielId,@projekt,(Select semestr from Przedmioty WHERE Id_Przedmiotu=@przedmiotId),(Select projekt from Przedmioty where Id_Przedmiotu = @przedmiotId))";
            string Updatequery1 = "Update AspNetUsers SET ZaplanowaneGodziny = (ZaplanowaneGodziny + (Select projekt from Przedmioty where Id_Przedmiotu = @przedmiotId)) where Id=@nauczycielId ";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@grupaId", grupaId);
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd.Parameters.AddWithValue("@projekt", projekt);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
               
                using (SqlCommand cmd2 = new SqlCommand(Updatequery1))
                {
                    cmd2.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd2.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd2.Connection = con;
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
            CheckIfWykladPlanned();
            CheckIfCwPlanned();
            CheckIfLabPlanned();
            CheckIfProjPlanned();
            CheckIfSemPlanned();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string przedmiotId = ddlCustomers.SelectedValue;
            string grupaId = ddlgrupy.SelectedValue;
            string nauczycielId = ddlNauczyciel4.SelectedValue;
            string seminarium = "Seminarium";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "INSERT INTO Zajecia(Id_Grupy,Id_Przedmiotu,Id_Nauczyciela,typ_zajec,semestr,ilosc_godzin) VALUES(@grupaId,@przedmiotId,@nauczycielId,@seminarium,(Select semestr from Przedmioty WHERE Id_Przedmiotu=@przedmiotId),(Select seminarium from Przedmioty where Id_Przedmiotu = @przedmiotId))";
            string Updatequery1 = "Update AspNetUsers SET ZaplanowaneGodziny = (ZaplanowaneGodziny + (Select seminarium from Przedmioty where Id_Przedmiotu = @przedmiotId)) where Id=@nauczycielId ";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@grupaId", grupaId);
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd.Parameters.AddWithValue("@seminarium", seminarium);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
           
                using (SqlCommand cmd2 = new SqlCommand(Updatequery1))
                {
                    cmd2.Parameters.AddWithValue("@przedmiotId", przedmiotId);
                    cmd2.Parameters.AddWithValue("@nauczycielId", nauczycielId);
                    cmd2.Connection = con;
                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
            CheckIfWykladPlanned();
            CheckIfCwPlanned();
            CheckIfLabPlanned();
            CheckIfProjPlanned();
            CheckIfSemPlanned();
        }

        protected void ddlNauczyciel1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlNauczyciel2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlNauczyciel3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlNauczyciel4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
