using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Salon;

namespace Salon.Objects
{
  public class Stylist
  {
    private int _id;
    private string _stylistName;
    private string _specialty;

    public Stylist(string StylistName, string Specialty, int Id=0)
    {
      _id = Id;
      _stylistName = StylistName;
      _specialty = Specialty;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId() == newStylist.GetId();
        bool stylistNameEquality = this.GetStylistName() == newStylist.GetStylistName();
        bool specialtyEquality = this.GetSpecialty() == newStylist.GetSpecialty();
        return (idEquality && stylistNameEquality && specialtyEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetStylistName()
    {
      return _stylistName;
    }
    public void SetStylistName(string newStylistName)
    {
      _stylistName = newStylistName;
    }
    public string GetSpecialty()
    {
      return _specialty;
    }
    public void SetSpecialty(string newSpecialty)
    {
      _specialty = newSpecialty;
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistSpecialty = rdr.GetString(2);
        Stylist newStylist = new Stylist(stylistName, stylistSpecialty, stylistId);
        allStylists.Add(newStylist);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStylists;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (stylist_name, specialty) OUTPUT INSERTED.id VALUES (@StylistName, @StylistSpecialty)", conn);

      SqlParameter stylistNameParameter = new SqlParameter();
      stylistNameParameter.ParameterName = "@StylistName";
      stylistNameParameter.Value = this.GetStylistName();
      SqlParameter specialtyParameter = new SqlParameter();
      specialtyParameter.ParameterName = "@StylistSpecialty";
      specialtyParameter.Value = this.GetSpecialty();
      cmd.Parameters.Add(stylistNameParameter);
      cmd.Parameters.Add(specialtyParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Stylist Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = id.ToString();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStylistId = 0;
      string foundStylistName = null;
      string foundStylistSpecialty = null;

      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
        foundStylistSpecialty = rdr.GetString(2);
      }

      Stylist foundStylist = new Stylist(foundStylistName, foundStylistSpecialty, foundStylistId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundStylist;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
