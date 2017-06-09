using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Salon.Objects;

namespace Salon
{
  [Collection("Salon")]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Stylist.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Stylist firstStylist = new Stylist("Betty Jean", "Long hair, ombre coloring");
      Stylist secondStylist = new Stylist("Betty Jean", "Long hair, ombre coloring");
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact] //Spec 1
    public void Test_Save_ToStylistDatabase()
    {
      Stylist testStylist = new Stylist("Emmylou Earnest", "Men's cuts and short hair styles.");
      testStylist.Save();

      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};
      Assert.Equal(testList, result);
    }

    [Fact] //Spec 2
    public void Test_FindStylistInDatabase()
    {
      Stylist testStylist = new Stylist("Emmylou Earnest", "Men's cuts and short hair styles.");
      testStylist.Save();

      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      Assert.Equal(testStylist, foundStylist);
    }

    [Fact] //Spec 4
    public void Test_GetClients_RetrievesAllClientsForStylist()
    {
      Stylist testStylist = new Stylist("Emmylou Earnest", "Men's cuts and short hair styles.");
      testStylist.Save();

      Client firstClient = new Client("Darla Darling", "On her last visit (1/2/2013), we trimmed her hair and touched up her roots with dark drown.", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Beau Blue", "On his last visit, we shaved his sides and trimmed the top.", testStylist.GetId());
      secondClient.Save();

      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      Assert.Equal(testClientList, resultClientList);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
  }
}
