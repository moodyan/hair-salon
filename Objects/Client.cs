using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Salon;

namespace Salon.Objects
{
  public class Client
  {
    private int _id;
    private string _clientName;
    private string _details;
    private int _stylistId;

    public Client(string ClientName, string Details, int StylistId, int Id = 0)
    {
      _id = Id;
      _clientName = ClientName;
      _details = Details;
      _stylistId = StylistId;
    }
    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool clientNameEquality = (this.GetClientName() == newClient.GetClientName());
        bool detailsEquality = (this.GetDetails() == newClient.GetDetails());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        return (clientNameEquality && detailsEquality && stylistIdEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public string GetClientName()
    {
      return _clientName;
    }
    public void SetClientName(string newClientName)
    {
      _clientName = newClientName;
    }
    public string GetDetails()
    {
      return _details;
    }
    public void SetDetails(string newDetails)
    {
      _details = newDetails;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        string clientDetails = rdr.GetString(2);
        int stylistId = rdr.GetInt32(3);
        Client newClient = new Client(clientName, clientDetails, stylistId, clientId);
        allClients.Add(newClient);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allClients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (client_name, details, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @ClientDetails, @StylistId)", conn);

      SqlParameter clientNameParameter = new SqlParameter();
      clientNameParameter.ParameterName = "@ClientName";
      clientNameParameter.Value = this.GetClientName();

      SqlParameter detailsParameter = new SqlParameter();
      detailsParameter.ParameterName = "@ClientDetails";
      detailsParameter.Value = this.GetDetails();

      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetStylistId();

      cmd.Parameters.Add(clientNameParameter);
      cmd.Parameters.Add(detailsParameter);
      cmd.Parameters.Add(stylistIdParameter);
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

    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundClientId = 0;
      string foundClientName = null;
      string foundClientDetails = null;
      int foundStylistId = 0;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundClientDetails = rdr.GetString(2);
        foundStylistId = rdr.GetInt32(3);
      }

      Client foundClient = new Client(foundClientName, foundClientDetails, foundStylistId, foundClientId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
