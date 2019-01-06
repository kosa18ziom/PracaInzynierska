using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Praca_Inżynierska
{
    public partial class Import : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void insertSubject(string[] subject, int planid)
        {
            string consString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Przedmioty (nazwa_przedmiotu,wyklad,cwiczenia,laboratorium,projekt,seminarium,Rygor,Semestr,Id_Planu) values (@nazwa,@wyklad,@cwiczenia,@lab,@projekt,@seminarium,@rygor,@semestr,@id_planu)",con))
        
        {
            cmd.Parameters.AddWithValue("@nazwa", subject[0]);
            cmd.Parameters.AddWithValue("@wyklad", subject[1]);
                cmd.Parameters.AddWithValue("@cwiczenia", subject[2]);
                cmd.Parameters.AddWithValue("@lab", subject[3]);
                cmd.Parameters.AddWithValue("@projekt", subject[4]);
                cmd.Parameters.AddWithValue("@seminarium", subject[5]);
                cmd.Parameters.AddWithValue("@semestr", subject[7]);
                cmd.Parameters.AddWithValue("@rygor", subject[6]);
                cmd.Parameters.AddWithValue("@id_planu", planid);

                con.Open();

            cmd.ExecuteNonQuery(); // executeNonQuery

            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }
        }

    protected void Upload(object sender, EventArgs e)
        {
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);
            string fileName = FileUpload1.PostedFile.FileName;
            fileName = fileName.Substring(0, fileName.Length - 4);
            int planid = insertPlan(fileName);
            string csvData = File.ReadAllText(csvPath);
            Boolean headerRowHasBeenSkipped = false;
            foreach (string row in csvData.Split('\n'))
            {
                if (headerRowHasBeenSkipped)
                {
                    if (!string.IsNullOrEmpty(row))
                {
                    string[] subject = row.Split(';');
                    
                    insertSubject(subject, planid);
                }
                }
                headerRowHasBeenSkipped = true;
            }
        }
           
            
            protected int insertPlan(string fileName)
            {

            string consString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Plan_Studiow(nazwa) output INSERTED.id_planu VALUES(@na)", con))
            {
                cmd.Parameters.AddWithValue("@na", fileName);
                con.Open();

                int planid = (int)cmd.ExecuteScalar();

                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();

                return planid;
            }
        }

        protected void insertGrupy(string[] Grupy)
        {
            string consString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Grupy (Nazwa_Grupy,Semestr) values (@nazwa,@semestr)", con))

            {
                cmd.Parameters.AddWithValue("@nazwa", Grupy[0]);
                cmd.Parameters.AddWithValue("@semestr", Grupy[1]);


                con.Open();

                cmd.ExecuteNonQuery(); // executeNonQuery

                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }
        protected void Upload2(object sender, EventArgs e)
        {
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload2.PostedFile.FileName);
            FileUpload2.SaveAs(csvPath);
            string csvData = File.ReadAllText(csvPath);
            Boolean headerRowHasBeenSkipped = false;
            foreach (string row in csvData.Split('\n'))
            {
                if (headerRowHasBeenSkipped)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        string[] Grupy = row.Split(';');

                        insertGrupy(Grupy);
                    }
                }
                headerRowHasBeenSkipped = true;
            }
        }

        protected void Upload4(object sender, EventArgs e)
        {
            //Upload and save the file
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload4.PostedFile.FileName);
            FileUpload4.SaveAs(csvPath);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Id", typeof(int)),
            new DataColumn("Email",typeof(string)), new DataColumn("UserName",typeof(string)),new DataColumn("Imie",typeof(string)),
            new DataColumn("Nazwisko",typeof(string)),new DataColumn("Rola",typeof(string)),new DataColumn("IsActivated",typeof(int)),
            new DataColumn("ImieNazwisko",typeof(string)),new DataColumn("PasswordHash",typeof(string))});


            string csvData = File.ReadAllText(csvPath);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }

            string consString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.Test_Nauczyciele";
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    SqlCommand cmd = new SqlCommand("INSERT INTO AspNetUsers (ID,Email,UserName,Imie,Nazwisko,Rola,ImieNazwisko,IsActivated,PasswordHash) Select ID,Email,UserName,Imie,Nazwisko,Rola,ImieNazwisko,IsActivated,PasswordHash from Test_Nauczyciele", con);
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO AspNetUserRoles (UserId,RoleId) Select ID,Rola From Test_Nauczyciele", con);
                    SqlCommand cmd1 = new SqlCommand("Delete From Test_Nauczyciele", con);
                    SqlCommand cmd2 = new SqlCommand("Update AspNetUsers Set EmailConfirmed = 'FALSE',PhoneNumberConfirmed='False',TwoFactorEnabled='False',LockoutEnabled='TRUE',AccessFailedCount=0,SecurityStamp=1", con);
                    cmd.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }

}