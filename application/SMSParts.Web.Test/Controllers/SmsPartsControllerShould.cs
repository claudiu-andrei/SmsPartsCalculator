using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SMSParts.Business.Interfaces;
using SMSParts.Domain.Models;
using SMSParts.Domain.Models.Response;
using SMSParts.Web.Controllers;
using Xunit;

namespace SMSParts.Web.Test.Controllers
{
    public class SmsPartsControllerShould
    {
        private Mock<ISmsPartsService> SmsPartsServiceMock { get; }
        private SmsPartsController Controller { get; }

        private readonly string _defaultInput = "test input";
        private readonly Response<SmsPartsInformationDto> _defaultValidResponse = new()
        {
            Data = new SmsPartsInformationDto
            {
                SmsParts = new List<SmsPart>
                {
                    new()
                    {
                        Part = Guid.NewGuid().ToString()
                    }
                }
            },
            Problem = null
        };

        private readonly Response<SmsPartsInformationDto> _defaultInvalidResponse = new()
        {
            Data = null,
            Problem = "some problem"
        };

        public SmsPartsControllerShould()
        {
            SmsPartsServiceMock = new Mock<ISmsPartsService>();
            SetupSmsPartsServiceMock();
            Controller = new SmsPartsController(SmsPartsServiceMock.Object);
        }

        private void SetupSmsPartsServiceMock(bool validResponse = true)
        {
            var response = validResponse ? _defaultValidResponse : _defaultInvalidResponse;

            SmsPartsServiceMock
                .Setup(service => service
                    .GetSmsPartsInformation(It.IsAny<string>()))
                .Returns(response);
        }

        [Fact]
        public void CallSmsPartsServiceGetSmsPartsInformationOnce()
        {
            // act
            Controller.Get(_defaultInput);

            // assert
            SmsPartsServiceMock
                .Verify(service => service.GetSmsPartsInformation(_defaultInput),
                Times.Once);
        }

        [Fact]
        public void ReturnValidSmsInformationIfServiceResponseIsValid()
        {
            // act
            var response = Controller.Get(_defaultInput);

            // assert
            var responseObject = response.As<ObjectResult>();
            responseObject.Value.Should()
                .BeOfType<SmsPartsInformationDto>("because this is the expected type")
                .Which.Should()
                .BeEquivalentTo(_defaultValidResponse.Data, "because this is the expected data");
        }

        [Fact]
        public void ReturnInternalServerErrorIfServiceResponseIsInvalid()
        {
            // arrange
            SetupSmsPartsServiceMock(false);

            // act
            var response = Controller.Get(_defaultInput);

            // assert
            var responseObject = response.As<ObjectResult>();
            responseObject.StatusCode.Should()
                .Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
