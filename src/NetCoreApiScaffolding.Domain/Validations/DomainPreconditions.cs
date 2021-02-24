using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetCoreApiScaffolding.Domain.Exceptions;
using NetCoreApiScaffolding.Tools.Exceptions;

namespace NetCoreApiScaffolding.Domain.Validations
{
    public static class DomainPreconditions
    {
        public static void NotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException(DomainPreconditionMessages.GetNotEmpty(parameterName));
            }
        }

        public static void NotEmpty(byte[] value, string parameterName)
        {
            if (value.Length <= 0)
            {
                throw new DomainException(DomainPreconditionMessages.GetNotEmpty(parameterName));
            }
        }

        public static void LongerThan(string value, int maxValue, string parameterName) =>
            LongerThan(value?.Length ?? 0, maxValue, parameterName);

        public static void LongerThan(int value, int maxValue, string parameterName)
        {
            if (value > maxValue)
            {
                throw new DomainException(DomainPreconditionMessages.GetLongerThan(maxValue, parameterName));
            }
        }

        public static void ShorterThan(string value, int minValue, string parameterName) =>
            ShorterThan(value?.Length ?? 0, minValue, parameterName);

        public static void ShorterThan(int value, int minValue, string parameterName)
        {
            if (minValue > value)
            {
                throw new DomainException(DomainPreconditionMessages.GetShorterThan(minValue, parameterName));
            }
        }

        public static void LengthEqualTo(string value, int requiredLength, string parameterName)
        {
            if (value?.Length != requiredLength)
            {
                throw new DomainException(DomainPreconditionMessages.GetLengthEqualTo(requiredLength, parameterName));
            }
        }

        public static void RegexMatch(string value, Regex regex, string parameterName)
        {
            var match = regex.Match(value);
            if (!match.Success)
            {
                throw new DomainException(DomainPreconditionMessages.GetSuccessMatch(parameterName));
            }
        }

        public static T NotNull<T>(T value, string parameterName)
            where T : class
        {
            if (value is null)
            {
                throw new DomainException(DomainPreconditionMessages.GetNotNull(parameterName));
            }

            return value;
        }

        public static T IntNotNull<T>(T value, int parameterName)
            where T : class
        {
            if (value is null)
            {
                throw new DomainException(DomainPreconditionMessages.GetNotNull(parameterName));
            }

            return value;
        }

        public static IEnumerable<T> NotEmpty<T>(IEnumerable<T> values, string parameterName) where T : class
        {
            if (values != null && !values.Any())
            {
                throw new DomainException(DomainPreconditionMessages.GetNotEmptyCollection(parameterName));
            }

            return values;
        }

        public static void EarlierThan(DateTime? value, DateTime dateToCompare, string parameterName)
        {
            if (value.HasValue && value.Value >= dateToCompare)
            {
                throw new DomainException(DomainPreconditionMessages.GetEarlierThan(dateToCompare, parameterName));
            }
        }

        public static void EarlierOrEqualThan(DateTime start, DateTime end, string parameterName)
        {
            if (start > end)
            {
                throw new DomainException(DomainPreconditionMessages.GetEarlierOrEqualThan(end, parameterName));
            }
        }

        public static void LaterThan(DateTime? value, DateTime dateToCompare, string parameterName)
        {
            if (value.HasValue && value.Value <= dateToCompare)
            {
                throw new DomainException(DomainPreconditionMessages.GetLaterThan(dateToCompare, parameterName));
            }
        }

        public static void LaterOrEqualThan(DateTime? value, DateTime dateToCompare, string parameterName)
        {
            if (value.HasValue && value.Value < dateToCompare)
            {
                throw new DomainException(DomainPreconditionMessages.GetLaterOrEqualThan(dateToCompare, parameterName));
            }
        }

        public static void GreaterThan(int quantity, int min, string parameterName)
        {
            if (quantity <= min)
            {
                throw new DomainException(DomainPreconditionMessages.GreaterThan(min, parameterName));
            }
        }

        public static void GreaterThan(decimal quantity, decimal min, string parameterName)
        {
            if (quantity <= min)
            {
                throw new DomainException(DomainPreconditionMessages.GreaterThan(min, parameterName));
            }
        }

        public static void LessThanOrEqualTo(int quantity, int max, string parameterName)
        {
            if (quantity > max)
            {
                throw new DomainException(DomainPreconditionMessages.LessThanOrEqualTo(max, parameterName));
            }
        }

        public static void IsIntInIntList(IEnumerable<int> intList, int value, string parameterName)
        {
            if (intList.All(i => i != value))
            {
                throw new DomainException(DomainPreconditionMessages.IsIntInIntList(parameterName));
            }
        }
    }
}