using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using Transaction.API.Application.Models;
using Transaction.Infrastructure.Database;

namespace Transaction.API.Application.Queries
{
    public class BuyerQueries:IBuyerQueries
    {
        private string _connectionString = string.Empty;
    
        public BuyerQueries(string constr)
        {
               _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        public async Task<Buyer> GetTransactionAsync(TransactionModel transactionModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var priceResult = await connection.QueryAsync<dynamic>("select * from Buyers where Price <= " + transactionModel.Price);

                if (priceResult.AsList().Count != 0)                
                {
                    var check = await connection.QueryAsync<dynamic>("select * from Buyers where Quantity >=" +transactionModel.Quantity);
                    if (check.AsList().Count != 0)
                    {
                        //Console.WriteLine("settlement done");   //add event

                    }
                    else
                    {
                        //Console.WriteLine("partial hold "); //add event
                    }
                }
                else
                {
                    // throw new KeyNotFoundException("Not Available"); //add event

                }
                return MapBuyerItems(priceResult);
            }
           
        }

        private Buyer MapBuyerItems(dynamic result)
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
