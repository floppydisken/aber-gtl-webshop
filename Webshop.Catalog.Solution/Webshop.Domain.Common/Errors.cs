﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Domain.Common;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string valueName, string why) => new Error("value.is.invalid", $"Value '{valueName}' is invalid. Reason: {why}");
        public static Error ValueIsNull(string valueName) => new Error("value.is.null", $"Value '{valueName}' is null");
        public static Error UnspecifiedError(string message) => new Error("unspecified.error", message);
        public static Error NotFound<T>(T id) where T : struct => new Error("entity.not.found", $"Could not find entity with ID {id}.", statusCode: 404);
        public static Error ValueIsRequired(string valueName) => new Error("value.is.required", $"Value '{valueName}' is required.");
        public static Error ValueTooSmall(string valueName, int minValue, string? comment = null) => new Error("value.too.small", $"Value '{valueName}' should be at least {minValue}.{(!string.IsNullOrWhiteSpace(comment) ? $" {comment}" : "")}");
        public static Error ValueTooSmall(string valueName, int minValue, int value, string? comment = null) => new Error("value.too.small", $"Value '{valueName}={value}' should be at least {minValue}.{(!string.IsNullOrWhiteSpace(comment) ? $" {comment}" : "")}");
        public static Error ValueTooSmall(string valueName, decimal minValue, string? comment = null) => new Error("value.too.small", $"Value '{valueName}' should be at least {minValue}.{(!string.IsNullOrWhiteSpace(comment) ? $" {comment}" : "")}");
        public static Error ValueTooSmall(string valueName, decimal minValue, decimal value, string? comment = null) => new Error("value.too.small", $"Value '{valueName}={value}' should be at least {minValue}.{(!string.IsNullOrWhiteSpace(comment) ? $" {comment}" : "")}");
        public static Error ValueTooLarge(string valueName, int maxValue) => new Error("value.too.large", $"Value '{valueName}' should not exceed {maxValue}.");
        public static Error ValueTooLarge(string valueName, decimal maxValue) => new Error("value.too.large", $"Value '{valueName}' should not exceed {maxValue}.");
        public static Error UnexpectedValue(string value) => new Error("unexpected.value", $"Value '{value}' is not valid in this context");
        public static Error Unauthorized() => new Error("unauthorizaed", $"Could not authorize access to entity");
        public static Error ValueIsEmpty(string value) => new Error("value.empty", $"The value cannot be empty: {value} ");
        public static Error ValueOutOfRange(string valueName, int minValue, int maxValue) =>
            new Error("value.out.of.Range", $"Value '{valueName}' should be between {minValue} and {maxValue}.");
        public static Error ValueOutOfRange(string valueName, decimal minValue, decimal maxValue, string? message = null) =>
            new Error("value.out.of.Range", $"Value '{valueName}' should be between {minValue} and {maxValue}.{(message is not null ? $" Inner Message: {message}" : "")}");
    }
}