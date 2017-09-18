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
using Microsoft.AspNetCore.Identity;
using Pizzeria.Models;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging.Internal;

namespace PizzeriaXUnitTests
{
    public class PaymentsControllerTests : BasePizzeriaTests
    {
        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Carts.Add(new Cart { CartId = 3 });
            context.SaveChanges();
        }

        [Fact]
        public void Show_Log_After_Payment()
        {
            //Arrange
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var controller = serviceProvider.GetService<PaymentsController>();
            var loggerMock = new Mock<ILogger<ManageController>>();
            var emailMock = new Mock<IEmailSender>();
            controller = new PaymentsController(context, null, null, emailMock.Object, loggerMock.Object, null, null);

            PaymentViewModel user = new PaymentViewModel()
            {
                Email = "admin@test.com",
            };

            //Act
            controller.Receipt(3, 100, user);

            ////Assert
            //loggerMock.Verify(x => x.Log("To: admin@test.com, Subject: Confirmation of payment, Message: Thank you for your order!"), Times.Exactly(1));
            loggerMock.Verify(
             m => m.Log(
             LogLevel.Critical,
             It.IsAny<EventId>(),
             It.Is<FormattedLogValues>(v => v.ToString().Contains("To: admin@test.com, Subject: Confirmation of payment, Message: Thank you for your order!")),
             It.IsAny<Exception>(),
             It.IsAny<Func<object, Exception, string>>()
             )
             );
        }
    }
}
