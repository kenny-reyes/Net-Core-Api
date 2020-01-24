using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApiExercise.Application.Shared.Request;
using ApiExercise.Infrastructure.Queries;

namespace ApiExercise.Infrastructure.Shared.Queries
{
    public class SqlWhereBuilder
    {
        private readonly IFilterRequest _request;
        private readonly Dictionary<string, object> _parameters;
        private readonly Dictionary<string, string> _columns;

        public SqlWhereBuilder(IFilterRequest request, Dictionary<string, string> columns)
        {
            _request = request;
            _columns = columns;
            _parameters = new Dictionary<string, object>();
        }

        public ParametrizedSql Build()
        {
            var sql = new StringBuilder();

            if (_request.Filter == null || !_request.Filter.HasFilters() || !AtLeastOneFilterIsValid(_request.Filter))
            {
                return ParametrizedSql.CreateEmpty();
            }

            sql.Append(" WHERE");
            sql.Append("(");
            sql.Append(AddFilter(_request.Filter));
            sql.Append(")");

            return ParametrizedSql.Create(sql.ToString(), _parameters);
        }

        private string AddFilter(Filter filter, string filterNumber = null)
        {
            var sql = new StringBuilder();

            if (!string.IsNullOrEmpty(filter.Field) && !string.IsNullOrEmpty(filter.Operator))
            {
                if (Constants.Filter.Operations.TryGetValue(filter.Operator, out string operationsFormat))
                {
                    var sqlFilterFieldParam = filter.Field.Replace(".", string.Empty);

                    if (_parameters.ContainsKey(sqlFilterFieldParam) && !string.IsNullOrEmpty(filterNumber))
                    {
                        sqlFilterFieldParam = $"{sqlFilterFieldParam}{filterNumber}";
                    }

                    var operation = string.Format(operationsFormat, _columns[filter.Field], $"@{sqlFilterFieldParam}");
                    sql.Append($" {operation}");

                    if (filter.Value is DateTimeOffset)
                    {
                        filter.Value = ((DateTimeOffset)filter.Value).LocalDateTime;
                    }

                    var filterOperationIsTypeLike = operationsFormat.Contains("LIKE");
                    _parameters.Add(sqlFilterFieldParam, filterOperationIsTypeLike ? GetLikeFilterValue(filter.Value.ToString(), filter.Operator) : filter.Value ?? string.Empty);
                }
            }

            if (!filter.HasFilters())
            {
                return sql.ToString();
            }

            for (var i = 0; i < filter.Filters.Count; i++)
            {
                if (i == 0)
                {
                    sql.Append(" (");
                }

                var requestFilter = filter.Filters.ElementAt(i);
                sql.Append(AddFilter(requestFilter, $"{filterNumber}{i}"));

                if (!string.IsNullOrEmpty(filter.Logic) && filter.Filters.Count > i + 1)
                {
                    sql.Append($" {Constants.Filter.Logics[filter.Logic]}");
                }

                if (i + 1 == filter.Filters.Count)
                {
                    sql.Append(")");
                }
            }

            return sql.ToString();
        }

        private string GetLikeFilterValue(string filterValue, string filterOperator)
        {
            var result = string.Empty;

            switch (filterOperator)
            {
                case Constants.FilterOperatorOptions.StartsWith:
                    result = $"{filterValue}%";
                    break;
                case Constants.FilterOperatorOptions.EndsWith:
                    result = $"%{filterValue}";
                    break;
                case Constants.FilterOperatorOptions.Contains:
                    result = $"%{filterValue}%";
                    break;
                case Constants.FilterOperatorOptions.DoesNotContains:
                    result = $"%{filterValue}%";
                    break;
            }

            return result;
        }

        private static bool AtLeastOneFilterIsValid(Filter filter)
        {
            return filter.GetFiltersWithoutChildrenFilters().Any(f => Constants.Filter.Operations.TryGetValue(f.Operator, out string _));
        }
    }
}
