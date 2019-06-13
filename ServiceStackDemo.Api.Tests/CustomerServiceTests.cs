using NUnit.Framework;
using ServiceStack;
using ServiceStackDemo.Api.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceStackDemo.Api.Tests
{
    class CustomerServiceTests
    {
        const string BaseUri = "http://localhost:2000/";

        ServiceStackHost appHost;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            appHost = new AppSelfHost()
               .Init()
               .Start(BaseUri);
        }

        [OneTimeSetUp]
        public void TestFixtureTearDown()
        {
            //Dispose it on TearDown
            appHost.Dispose();
        }
        [Test]
        public void Run_Customer_REST_Example()
        {
            var client = new JsonServiceClient(BaseUri);

            //GET /customers
            var all = client.Get(new GetCustomers());
            Assert.That(all.Results.Count, Is.EqualTo(0));

            //POST /customers
            var customer = client.Post(new CreateCustomer { Name = "Foo" });
            Assert.That(customer.Id, Is.EqualTo(1));
            //GET /customer/1
            customer = client.Get(new GetCustomer { Id = customer.Id });
            Assert.That(customer.Name, Is.EqualTo("Foo"));

            //GET /customers
            all = client.Get(new GetCustomers());
            Assert.That(all.Results.Count, Is.EqualTo(1));

            //PUT /customers/1
            customer = client.Put(new UpdateCustomer { Id = customer.Id, Name = "Bar" });
            Assert.That(customer.Name, Is.EqualTo("Bar"));

            //DELETE /customers/1
            client.Delete(new DeleteCustomer { Id = customer.Id });
            //GET /customers
            all = client.Get(new GetCustomers());
            Assert.That(all.Results.Count, Is.EqualTo(0));
        }

    }
}
