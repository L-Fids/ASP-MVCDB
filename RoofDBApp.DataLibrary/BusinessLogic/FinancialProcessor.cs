using Dapper;
using RoofDBApp.DataLibrary.DataAccess;
using RoofDBApp.DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofDBApp.DataLibrary.BusinessLogic
{
    public static class FinancialProcessor
    {
        public static int ModifyFinancial(int id, decimal? quote, decimal? finalPrice, decimal? commission)
        {
            var parameter = new DynamicParameters();
            parameter.Add("CustomerID", id);

            FinancialDataModel data = new FinancialDataModel // mapping the Customer UI model to the Financial DataLibrary model
            {
                CustomerID = id,
                Quote = quote,
                FinalPrice = finalPrice,
                Commission = commission
            };

            string sql = @"IF EXISTS(SELECT * FROM Financials WHERE CustomerID = @CustomerID)
                                UPDATE Financials
                                SET Quote = @Quote, FinalPrice = @FinalPrice, Commission = @Commission
                                WHERE CustomerID = @CustomerID
                            ELSE
                                INSERT INTO Financials
                                (CustomerID, Quote, FinalPrice, Commission)
                                VALUES
                                (@CustomerID, @Quote, @FinalPrice, @Commission)";

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}
