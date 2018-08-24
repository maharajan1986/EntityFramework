using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer.Models;
using System.Linq;

namespace FluentConfigurationTest
{
    [TestClass]
    public class AddressTest
    {
        //If the Test fails, please check weather the database exists or not. I used existing database to 
        // concentrate only FluentConfiguration.
        string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Address;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private FluentContext dbContext = null;
        public AddressTest()
        {
            FluentContext.ConnectionString = ConnectionString;            
        }
        
        [TestMethod]
        public void ReadHomeAddress()
        {
            //HomeAddress table to be used.
            dbContext = FluentContext.PrepareModelBuilder("dbo", "HomeAddress");
            var HomeAddressCity = "Aundipatti";
            var HomeAddress = dbContext.Addresss.Where(x => x.City == HomeAddressCity).FirstOrDefault();
            Assert.IsNotNull(HomeAddress.City);
        }

        [TestMethod]
        public void ReadOfficeAddress()
        {
            //OfficeAddress table to be used
            dbContext = FluentContext.PrepareModelBuilder("dbo", "OfficeAddress");
            var HomeAddressCity = "Yokohama";
            var HomeAddress = dbContext.Addresss.Where(x => x.City == HomeAddressCity).FirstOrDefault();
            Assert.IsNotNull(HomeAddress.City);
        }
    }
}
