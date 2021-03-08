using System;
using System.Collections.Generic;

namespace NetCoreApiScaffolding.Infrastructure.Common.Queries
{
    public class ParametrizedSql
    {
        public string Sql { get; }
        public Dictionary<string, object> Parameters { get; }

        private ParametrizedSql(string sql, Dictionary<string, object> parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }

        public static ParametrizedSql Create(string sql, Dictionary<string, object> parameters)
        {
            return new ParametrizedSql(sql, parameters);
        }

        public static ParametrizedSql CreateEmpty()
        {
            return new ParametrizedSql(" WHERE 1=1", new Dictionary<string, object>());
        }

        public bool ContainsWhere()
        {
            return Sql.IndexOf("WHERE", StringComparison.Ordinal) > -1;
        }
    }
}