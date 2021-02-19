using System.Collections.Generic;

namespace NetCoreApiScaffolding.Infrastructure.Queries
{
    public sealed class Constants
    {
        public static class SortOptions
        {
            public const string Ascendant = "asc";
            public const string Descendant = "desc";
        }

        public static class Sort
        {
            public static readonly Dictionary<string, string> Directions = new Dictionary<string, string>
            {
                {SortOptions.Ascendant, "ASC" },
                {SortOptions.Descendant, "DESC" }
            };
        }

        public static class FilterOperatorOptions
        {
            public const string Equal = "eq";
            public const string NotEqual = "neq";
            public const string IsNull = "isnull";
            public const string IsNotNull = "isnotnull";
            public const string LowerThan = "lt";
            public const string LowerThanOrEqual = "lte";
            public const string GreaterThan = "gt";
            public const string GreaterThanOrEqual = "gte";
            public const string StartsWith = "startswith";
            public const string EndsWith = "endswith";
            public const string Contains = "contains";
            public const string DoesNotContains = "doesnotcontain";
            public const string IsEmpty = "isempty";
            public const string IsNotEmpty = "isnotempty";

        }

        public static class FilterLogicOptions
        {
            public const string And = "and";
            public const string Or = "or";
        }

        public static class Filter
        {
            public static Dictionary<string, string> Logics = new Dictionary<string, string>
            {
                {FilterLogicOptions.And, "AND" },
                {FilterLogicOptions.Or, "OR" }
            };

            public static Dictionary<string, string> Operations = new Dictionary<string, string>
            {
                {FilterOperatorOptions.Equal, "{0} = {1}" },
                {FilterOperatorOptions.NotEqual, "{0} != {1}"},
                {FilterOperatorOptions.IsNull, "{0} IS NULL" },
                {FilterOperatorOptions.IsNotNull, "{0} IS NOT NULL" },
                {FilterOperatorOptions.LowerThan, "{0} < {1}" },
                {FilterOperatorOptions.LowerThanOrEqual, "{0} <= {1}" },
                {FilterOperatorOptions.GreaterThan, "{0} > {1}" },
                {FilterOperatorOptions.GreaterThanOrEqual, "{0} >= {1}" },
                {FilterOperatorOptions.StartsWith, "{0} LIKE {1}" },
                {FilterOperatorOptions.EndsWith, "{0} LIKE {1}" },
                {FilterOperatorOptions.Contains, "{0} LIKE {1}" },
                {FilterOperatorOptions.DoesNotContains, "{0} NOT LIKE {1}" },
                {FilterOperatorOptions.IsEmpty, "datalength({0})=0" },
                {FilterOperatorOptions.IsNotEmpty, "datalength({0})>0" }
            };
        }
    }
}
