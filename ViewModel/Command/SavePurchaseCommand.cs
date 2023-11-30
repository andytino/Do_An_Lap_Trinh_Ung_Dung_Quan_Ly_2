using Microsoft.Data.SqlClient;
using PosApp.Helper;
using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.ViewModel.Command
{
    public class SavePurchaseCommand : CommandBase
    {
        private readonly PurchaseFormViewModel _purchaseFormViewModel;
        private readonly GlobalStore _globalStore;
        private readonly INavigationService _navigationService;
        private SqlConnection _connection;

        public SavePurchaseCommand(PurchaseFormViewModel purchaseFormViewModel,
            INavigationService navigationService,
            GlobalStore globalStore)
        {
            _purchaseFormViewModel = purchaseFormViewModel;
            _navigationService = navigationService;
            _connection = globalStore.CurrentGlobal.Connection;
        }

        public override void Execute(object parameter)
        {
            DateTime currentDateTime = DateTime.Now;
            var purchaseID = Guid.NewGuid().ToString();
            string insertSql = """
                        INSERT INTO Purchases (PurchaseID, SupplierID, PurchaseDate, TotalAmount, PaymentID, Description, CreatedAt, UpdatedAt)
                        VALUES (@PurchaseID, @SupplierID, @PurchaseDate, @TotalAmount, @PaymentID, @Description, @CreatedAt, @UpdatedAt)
                    """;
            var insertCommand = new SqlCommand(insertSql, _connection);

            insertCommand.Parameters.Add("@PurchaseID", System.Data.SqlDbType.VarChar, 50).Value = purchaseID;
            insertCommand.Parameters.Add("@SupplierID", System.Data.SqlDbType.VarChar, 50).Value = _purchaseFormViewModel.SupplierID;
            insertCommand.Parameters.Add("@PurchaseDate", System.Data.SqlDbType.Date).Value = _purchaseFormViewModel.Date;
            insertCommand.Parameters.Add("@TotalAmount", System.Data.SqlDbType.Float).Value = _purchaseFormViewModel.Total;
            insertCommand.Parameters.Add("@PaymentID", System.Data.SqlDbType.Int).Value = _purchaseFormViewModel.PaymentID;
            insertCommand.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 200).Value = _purchaseFormViewModel.PurchaseID;
            insertCommand.Parameters.Add("@CreatedAt", System.Data.SqlDbType.DateTime).Value = currentDateTime;
            insertCommand.Parameters.Add("@UpdatedAt", System.Data.SqlDbType.DateTime).Value = currentDateTime;

            int rows = insertCommand.ExecuteNonQuery();
            if(rows > 0 )
            {
                var purchaseDetailList = _purchaseFormViewModel.PurchaseDetails;
                foreach (var purchaseDetail in purchaseDetailList)
                {
                    string insertPurchaseDetailSql = """
                        INSERT INTO PurchaseDetails (PurchaseDetailID, PurchaseID, ProductID, Quantity, Price) 
                        VALUES (@PurchaseDetailID, @PurchaseID, @ProductID, @Quantity, @Price)
                    """;
                    var insertPurchaseDetailCommand = new SqlCommand(insertPurchaseDetailSql, _connection);

                    insertPurchaseDetailCommand.Parameters.Add("@PurchaseDetailID", System.Data.SqlDbType.VarChar, 50).Value = purchaseDetail.PurchaseDetailID;
                    insertPurchaseDetailCommand.Parameters.Add("@PurchaseID", System.Data.SqlDbType.VarChar, 50).Value = purchaseID;
                    insertPurchaseDetailCommand.Parameters.Add("@ProductID", System.Data.SqlDbType.VarChar, 50).Value = purchaseDetail.ProductID;
                    insertPurchaseDetailCommand.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = purchaseDetail.Quantity;
                    insertPurchaseDetailCommand.Parameters.Add("@Price", System.Data.SqlDbType.Float).Value = purchaseDetail.Price;

                    // Thực hiện lệnh SQL
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
