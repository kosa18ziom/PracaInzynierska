using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace Praca_Inżynierska
{
    public partial class Grupy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }
        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "Select G.Id_Grupy,G.Nazwa_Grupy,G.Ilosc_Studentow,G.Stopien_Studiow,G.Semestr,G.Wydzial,G.Kierunek,G.Specjalizacja,G.Rodzaj_Studiow,G.Id_Planu,P.nazwa from Grupy G left join Plan_Studiow P on G.Id_Planu = P.Id_Planu";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        
                    }
                }
            }
        }
        protected void Insert(object sender, EventArgs e)
        {
            string NazwaGrupy = txtNazwaGrupy.Text;
            txtNazwaGrupy.Text = "";
            string query = "INSERT INTO Grupy(Nazwa_Grupy) VALUES(@NazwaGrupy)";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@NazwaGrupy", NazwaGrupy);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            this.BindGrid();
        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int GrupaId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string NazwaGrupy = (row.FindControl("txtName") as TextBox).Text;
            string Ilosc = (row.FindControl("txtIlosc") as TextBox).Text;
            string Stopien = (row.FindControl("txtStopien") as TextBox).Text;
            string Semestr = (row.FindControl("txtSemestr") as TextBox).Text;
            string Wydzial = (row.FindControl("txtWydzial") as TextBox).Text;
            string Kierunek = (row.FindControl("txtKierunek") as TextBox).Text;
            string Specjalizacja = (row.FindControl("txtSpecjalizacja") as TextBox).Text;
            string Rodzaj = (row.FindControl("txtRodzaj") as TextBox).Text;
            string ddl = (row.FindControl("ddlPlan") as DropDownList).SelectedValue;
            string query = "UPDATE Grupy SET Nazwa_Grupy=@NazwaGrupy, Ilosc_Studentow=@Ilosc,Stopien_Studiow=@Stopien,Semestr=@Semestr,Wydzial=@Wydzial,Kierunek=@Kierunek,Specjalizacja=@Specjalizacja,Rodzaj_Studiow=@Rodzaj,Id_Planu=@ddl WHERE Id_grupy=@GrupaId";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@GrupaId", GrupaId);
                    cmd.Parameters.AddWithValue("@NazwaGrupy", NazwaGrupy);
                    cmd.Parameters.AddWithValue("@Ilosc", Ilosc);
                    cmd.Parameters.AddWithValue("@Stopien", Stopien);
                    cmd.Parameters.AddWithValue("@Semestr", Semestr);
                    cmd.Parameters.AddWithValue("@Wydzial", Wydzial);
                    cmd.Parameters.AddWithValue("@Kierunek", Kierunek);
                    cmd.Parameters.AddWithValue("@Specjalizacja", Specjalizacja);
                    cmd.Parameters.AddWithValue("@Rodzaj", Rodzaj);
                    cmd.Parameters.AddWithValue("@ddl", ddl);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int GrupaId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string query = "DELETE FROM Grupy WHERE Id_Grupy=@GrupaId";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@GrupaId", GrupaId);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            this.BindGrid();
        }

        protected void ddlPlan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}