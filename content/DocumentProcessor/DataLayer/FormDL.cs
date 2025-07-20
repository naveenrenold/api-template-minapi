using Dapper;
using DocumentProcessor.DataLayer.Interface;
using DocumentProcessor.Model;
using DocumentProcessor.Startup;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Query = DocumentProcessor.Constants.Query;

namespace DocumentProcessor.DataLayer
{
    public class FormDL(IDbConnection dbconnection, IOptions<AppSettings> appSettings) : BaseRepository, IFormDL
    {
        private readonly IDbConnection conn = dbconnection;

        public async Task<IEnumerable<FormResponse>> GetForm(QueryFilter<FormResponse> filter)
        {
            var query = Query.Form.getForm;
            var form = new DynamicParameters();
            query = BuildQuery(filter, query, ref form);
            return await conn.QueryAsync<FormResponse>(query, form);            
        }
    }
}
