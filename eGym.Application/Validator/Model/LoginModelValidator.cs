using eGym.Application.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Application.Validator
{
    public class LoginModelValidator : AbstractValidator<SignInModel>
    {
        public LoginModelValidator()
        {

        }
    }
}
