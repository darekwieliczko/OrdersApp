using Xunit;
using FluentValidation.TestHelper;
using OrdersApp.Models;
using System.Data;
using OrdersApp.Constans;
using OrdersAppTests.Helpers;

namespace OrdersApp.Validator.Tests;

public class OrderModelValidatorTests
{
    private OrderModelValidator validator;

    public OrderModelValidatorTests()
    {
        validator = new OrderModelValidator();
    }

    [Fact()]
    public void OrderModelValidatorShouldNotHaveErrors()
    {
        var model = new OrderModel 
        { 
            ProductName = TestHelper.GetRandomString(25), 
            DeliveryAddress = TestHelper.GetRandomString(45), 
            Price = TestHelper.GetRandomDecimal(), 
            PaymentType = Enums.PaymentType.Transfer,
            ClientType = Enums.ClientType.Company,
            Status = Enums.OrderStatus.New,
            OrderDate = DateTime.Now

        };
        var result = validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(order => order.ProductName);
        result.ShouldNotHaveValidationErrorFor(order => order.DeliveryAddress);
        result.ShouldNotHaveValidationErrorFor(order => order.Price);
        result.ShouldNotHaveValidationErrorFor(order => order.PaymentType);
        result.ShouldNotHaveValidationErrorFor(order => order.Status);
        result.ShouldNotHaveValidationErrorFor(order => order.ClientType);
        result.ShouldNotHaveValidationErrorFor(order => order.OrderDate);
   }

    [Fact()]
    public void OrderModelValidatorShouldHaveErrorsForEmptyValues()
    {
        var model = new OrderModel { };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(order => order.ProductName);
        result.ShouldHaveValidationErrorFor(order => order.DeliveryAddress);
        result.ShouldHaveValidationErrorFor(order => order.Price);
    }



    [Fact()]
    public void OrderModelValidatorShouldHaveErrorsForProductAndAdressTooShort()
    {
        var model = new OrderModel { ProductName = TestHelper.GetRandomString(2), DeliveryAddress = TestHelper.GetRandomString(4), Price = 0 };
        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(order => order.ProductName).WithErrorMessage(Messages.ERROR_PRODUCTNAME_TOOSHORT);

        result.ShouldHaveValidationErrorFor(order => order.DeliveryAddress).WithErrorMessage(Messages.ERROR_ADRESS_TOOSHORT);
        result.ShouldHaveValidationErrorFor(order => order.Price).WithErrorMessage(Messages.ERROR_PRICE);
    }

    [Fact()]
    public void OrderModelValidatorShouldHaveErrorsForProductAndAdressTooLong()
    {
        var model = new OrderModel { ProductName = TestHelper.GetRandomString(31), DeliveryAddress = TestHelper.GetRandomString(51), Price = TestHelper.GetRandomDecimal() };
        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(order => order.ProductName).WithErrorMessage(Messages.ERROR_PRODUCTNAME_TOOLONG);
        result.ShouldHaveValidationErrorFor(order => order.DeliveryAddress).WithErrorMessage(Messages.ERROR_ADRESS_TOOLONG);
    }

}