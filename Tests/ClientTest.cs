using System;
using System.Collections.Generic;
using Xunit;
using System.Data;
using System.Data.SqlClient;
using Salon.Objects;

namespace Salon
{
 [Collection("Salon")]
 public class ClientTest : IDisposable
 {
   public ClientTest()
   {
     DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
   }

   [Fact]
   public void Test_DatabaseEmptyAtFirst()
   {
     int result = Client.GetAll().Count;
     Assert.Equal(0, result);
   }

   [Fact]
   public void Test_Equal_ReturnsTrueForSameName()
   {
     Client firstClient = new Client("Beau Blue", "None", 1);
     Client secondClient = new Client("Beau Blue", "None", 1);
     Assert.Equal(firstClient, secondClient);
   }

   [Fact] //Spec 3
   public void Test_Save_ToClientDatabase()
   {
     Client testClient = new Client("Beau Blue", "None", 1);
     testClient.Save();

     List<Client> result = Client.GetAll();
     List<Client> testList = new List<Client>{testClient};
     Assert.Equal(testList, result);
   }

   [Fact] //Spec 4
   public void Test_FindClientInDatabase()
   {
     Client testClient = new Client("Beau Blue", "None", 1);
     testClient.Save();

     Client foundClient = Client.Find(testClient.GetId());

     Assert.Equal(testClient, foundClient);
   }

   public void Dispose()
   {
     Client.DeleteAll();
     Stylist.DeleteAll();
   }
 }
}
