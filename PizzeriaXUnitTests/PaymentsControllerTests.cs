using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Moq;
using Pizzeria.Controllers;
using Pizzeria.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Pizzeria.Services;
using Pizzeria.Data;

namespace PizzeriaXUnitTests
{
    public class PaymentsControllerTests : BasePizzeriaTests
    {
        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.SaveChanges();
        }

        [Fact]
        public void Receipt_Log()
        {
            //Arrange
            //var viewContext = new ViewContext()
            //{
            //    HttpContext = new DefaultHttpContext()
            //};
            var controller = serviceProvider.GetService<PaymentsController>();
;
            var loggerMock = new Mock<ILogger>();
            PaymentViewModel user = new PaymentViewModel()
            {
                Email = "admin@test.com",
                //CustomerName = "",
                //PhoneNumber = "",
                //Street = "",
                //PostalCode = "",
                //City = "",
                //CreditCardNumber = "",
                //NameOnCard = "",
                //YYMM = "",
                //CCV = "",
                //CardId = 1
            };

            //Act
            //controller.Receipt(1, user);

            ////Assert
            //loggerMock.Verify(x => x.LogCritical("To: admin@test.com, Subject: Confirmation of payment, Message: Thank you for your order!"), Times.Exactly(1));
        }
    }
}
