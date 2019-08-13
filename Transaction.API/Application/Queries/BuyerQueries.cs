using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.API.Application.Queries
{
    public class BuyerQueries: IBuyerQueries
    {
        private string _connectionString = string.Empty;
        public BuyerQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        public async Task<Buyer> GetBuyerAsync()
        {
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();

            //    //var result = await connection.QueryAsync<dynamic>("select * from");

            //    if (result.AsList().Count == 0)
            //        throw new KeyNotFoundException();

            //    return MapOrderItems(result);
            //}
            return null;
        }
        private Buyer MapOrderItems(dynamic result)
        {
            var buyer = new Buyer
            {
                buyeritems = new List<Buyeritem>(),
                total=0
            };

            foreach (dynamic item in result)
            {
                var buyeritem = new Buyeritem
                {
                    price = item.price,
                    quantity = item.quantity
                };

                buyer.total += item.quantity * item.price;
                buyer.buyeritems.Add(buyeritem);
            }

            return buyer;
        }
    }
}
