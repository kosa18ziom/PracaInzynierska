using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Praca_Inżynierska
{
    public partial class Contact : Page
    {
        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ZajeciaId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand("Update AspNetUsers SET ZaplanowaneGodziny=(Select A.ZaplanowaneGodziny FROM AspNetUsers A inner join Zajecia Z on Z.Id_Nauczyciela=A.Id WHERE Z.Id=@ZajeciaId) - (Select ilosc_godzin FROM Zajecia WHERE Id = @ZajeciaId) WHERE Id=(Select Id_Nauczyciela FROM Zajecia WHERE Id=@ZajeciaId)"))
                {
                    cmd1.Parameters.AddWithValue("@ZajeciaId", ZajeciaId);
                    cmd1.Connection = con;
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Zajecia WHERE Id = @ZajeciaId"))
                {
                    cmd.Parameters.AddWithValue("@ZajeciaId", ZajeciaId);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                
            }
            this.BindGrid();
        }
        protected void OnSorting(object sender, GridViewSortEventArgs e)
        {
            this.BindGrid(e.SortExpression);
        }


        private void BindGrid(string sortExpression = null)
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Z.Id,Z.Id_Grupy,G.Nazwa_Grupy,Z.Id_Przedmiotu,P.Nazwa_Przedmiotu,Z.Id_Nauczyciela,A.ImieNazwisko,Z.typ_zajec,Z.semestr,Z.ilosc_godzin FROM Zajecia Z inner join Grupy G on Z.Id_Grupy = G.Id_grupy inner join Przedmioty P on Z.Id_Przedmiotu = P.Id_Przedmiotu inner join AspNetUsers A on Z.Id_Nauczyciela = A.Id"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (sortExpression != null)
                            {
                                DataView dv = dt.AsDataView();
                                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                                dv.Sort = sortExpression + " " + this.SortDirection;
                                GridView1.DataSource = dv;
                            }
                            else
                            {
                                GridView1.DataSource = dt;
                            }
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
}