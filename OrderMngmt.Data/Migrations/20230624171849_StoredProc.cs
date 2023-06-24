using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderMngmt.Data.Migrations
{
    /// <inheritdoc />
    public partial class StoredProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var storedProcedure = @"
                CREATE PROCEDURE CreateOrder
                    @UserId INT,
                    @ProductId INT,
                    @Quantity INT
                AS
                BEGIN
                    DECLARE @Price DECIMAL(18, 2);
                    DECLARE @Total DECIMAL(18, 2);

                    SELECT @Price = Price FROM Products WHERE Id = @ProductId;
                    
                    SET @Total = @Price * @Quantity;
                    
                    INSERT INTO Orders (UserId, ProductId, Quantity, Total)
                    VALUES (@UserId, @ProductId, @Quantity, @Total);
                    
                    SELECT SCOPE_IDENTITY() AS NewOrderId;
                END;";

            migrationBuilder.Sql(storedProcedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
