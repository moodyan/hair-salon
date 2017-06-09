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

   [Fact]
   public void Test_Update_UpdatesClientDetailsInDatabase()
   {
     string clientName = "Minnie Mouse";
     string details = "Getting married to Mickey Mouse, practice updo for wedding";
     int stylistId = 1;
     Client testClient = new Client(clientName, details, stylistId);
     testClient.Save();
     string newDetails = "Post wedding hair cut. Cut 5 inches off.";

     testClient.UpdateDetails(newDetails);
     string result = testClient.GetDetails();

     Assert.Equal(newDetails, result);
   }

   [Fact]
   public void Test_Delete_DeletesClientFromDatabase()
   {
     //Arrange
     string clientName1 = "Beau Blue";
     string specialty1 = "Trim";
     int stylistId1 = 1;
     Client testClient1 = new Client(clientName1, specialty1, stylistId1);
     testClient1.Save();

     string clientName2 = "Darla Darling";
     string specialty2 = "Cut and color";
     int stylistId2 = 1;
     Client testClient2 = new Client(clientName2, specialty2, stylistId2);
     testClient2.Save();

     //Act
     testClient1.Delete();
     List<Client> resultClients = Client.GetAll();
     List<Client> testClientList = new List<Client> {testClient2};

     //Assert
     Assert.Equal(testClientList, resultClients);
   }

   public void Dispose()
   {
     Client.DeleteAll();
     Stylist.DeleteAll();
   }
 }
}
