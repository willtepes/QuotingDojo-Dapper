using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using quotingDojo2.Models;
using Microsoft.Extensions.Options;


namespace quotingDojo2.Factory
{
    public class QuoteFactory : IFactory<Quote>
    {
       private readonly IOptions<MySqlOptions> mysqlConfig;
    
        public QuoteFactory(IOptions<MySqlOptions> conf) {
            mysqlConfig = conf;
    }

        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }
        public void Add(Quote item, long user)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  $"INSERT INTO quotes (Users_id, quote, created_at, updated_at) VALUES ({user}, @quote, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Quote> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Quote>("SELECT * FROM quotes ORDER BY created_at");
            }
        }
        public IEnumerable<Quote> QuotesWithUser()
        {
            using (IDbConnection dbConnection = Connection)
            {
                var query = $"SELECT * FROM quotes JOIN users ON quotes.Users_id WHERE users.Id = quotes.Users_id";
                dbConnection.Open();
                var quoteswusers = dbConnection.Query<Quote, User, Quote>(query, (quote, user) => { quote.user = user; return quote; });
                return quoteswusers;
            }
        }
        public void delete(long quoteId)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  $"DELETE FROM quotes WHERE quotes.Id = {quoteId}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }

    }
}