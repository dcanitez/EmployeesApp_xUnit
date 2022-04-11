using EmployeesApp.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesApp.Tests.Validation
{
    public class AccountNumberValidationTests
    {
        private readonly AccountNumberValidation validation;
        public AccountNumberValidationTests() =>validation = new AccountNumberValidation();

        [Fact]
        public void IsValid_ValidAccountNumber_ReturnsTrue()
        {
            Assert.True(validation.IsValid("123-4543234576-23"));
        }

        [Theory]
        [InlineData("1234-3454565676-23")]
        [InlineData("12-3454565676-23")]
        public void IsValid_AccountNumberFirstPartWrong_ReturnsFalse(string accountNumber)
            => Assert.False(validation.IsValid(accountNumber));

        [Theory]
        [InlineData("123-345456567-23")]
        [InlineData("123-345456567633-23")]
        public void IsValid_AccountNumberMiddlePartWrong_ReturnsFalse(string accountNumber)
            =>Assert.False(validation.IsValid(accountNumber));

        [Theory]
        [InlineData("123-3434545656-2")]
        [InlineData("123-3454565676-233")]
        public void IsValid_AccountNumberLastPartWrong_ReturnsFalse(string accountNumber)
            => Assert.False(validation.IsValid(accountNumber));

        [Theory]
        [InlineData("123-345456567633=23")]
        [InlineData("123+345456567633-23")]
        [InlineData("123+345456567633=23")]
        public void IsValid_InvalidDelimiters_ThrowsArgumentException(string accountNumber)
            => Assert.Throws<ArgumentException>(() => validation.IsValid(accountNumber));
    }
}
