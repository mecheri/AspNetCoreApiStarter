using AspNetCoreApiStarter.Controllers;
using AspNetCoreApiStarter.Controllers.Core;
using AspNetCoreApiStarter.ViewModels;
using AspNetCoreApiStarter.ViewModels.Core;
using AspNetCoreApiStarter.ViewModels.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static AspNetCoreApiStarter.ViewModels.Core.ErrorVm;

namespace AspNetCoreApiStarter.Tests.Validation
{
    public class Validation_Tests
    {
        [Fact]
        public async void Validate_Ko()
        {
            // valide un user vide
            UserVm userVm = new UserVm();
            var validator = new UserValidator();
            var results = validator.Validate(userVm);

            // simul modelstate d'un controller
            ModelStateDictionary mockModelState = new ModelStateDictionary();
            results.AddToModelState(mockModelState, null);

            // simul ValidationFailedResult 
            // TODO test ValidationFailedResult possible ?
            var errorVm = new ErrorVm()
            {
                Code = ErrorCode.ValidationFailed,
                ValidationDictionnay = ValidationFailedResult.ToValidation(mockModelState)
            };

            // compare avec format du json attendu
            string json = JsonConvert.SerializeObject(errorVm);
            string jsonCheck =
                @"{ ""code"":6,
                    ""validation"":
                    {
                        ""Email"":[""Email cannot be empty""],
                        ""LastName"":[""LastName cannot be empty""],
                        ""Password"":[""Password cannot be empty""],
                        ""UserName"":[""User Name cannot be empty""],
                        ""FirstName"":[""FirstName cannot be empty""]
                    }
                }";

            bool jsonOk = JToken.DeepEquals(JToken.Parse(json), JToken.Parse(jsonCheck));

            Assert.False(mockModelState.IsValid);
            Assert.True(jsonOk);
        }
    }
}
