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
