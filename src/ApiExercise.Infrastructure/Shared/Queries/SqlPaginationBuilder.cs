using System.Collections.Generic;
using System.Text;
using ApiExercise.Application.Shared.Request;
using ApiExercise.Infrastructure.Queries;

namespace ApiExercise.Infrastructure.Shared.Queries
{
    public class SqlPaginationBuilder
    {
        private readonly IPaginatedRequest _request;
        private readonly Dictionary<string, object> _parameters;
        private const string SkipQueryParam = "Skip";
        private const string TakeQueryParam = "Take";
        private const string Skip = " OFFSET {0} ROWS";
        private const string Take = " FETCH NEXT {0} ROWS ONLY";

        public SqlPaginationBuilder(IPaginatedRequest request)
        {
            _request = request;
            _parameters = new Dictionary<string, object>();
        }

        public ParametrizedSql Build()
        {
            var sql = new StringBuilder();

            sql.AppendFormat(Skip, $"@{SkipQueryParam}");
            _parameters.Add(SkipQueryParam, _request.Skip);

            sql.AppendFormat(Take, $"@{TakeQueryParam}");
            _parameters.Add(TakeQueryParam, _request.Take);

            return ParametrizedSql.Create(sql.ToString(), _parameters);
        }
    }
}
