namespace RoyalShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevenuesStatisticSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("sp_GetRevenuesStatistic",
                p=>new
                {
                    fromDate = p.String(),
                    toDate = p.String()
                },
                @"
                    SELECT 
                    CONVERT(NVARCHAR,o.CreatedDate,103),
                    SUM(od.Quantity*od.Price) AS Revenues,
                    SUM((od.Quantity*od.Price)-(od.Quantity*p.OriginalPrice)) AS Benefit
                    FROM dbo.Orders o
                    INNER JOIN dbo.OrderDetails od
                    ON o.ID = od.OrderID
                    INNER JOIN dbo.Products p 
                    ON od.ProductID = p.ID
                    WHERE o.CreatedDate BETWEEN CONVERT(DATE,@fromDate,103) AND CONVERT(DATE,@toDate,103)
                    GROUP BY o.CreatedDate    
                "
                );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.sp_GetRevenuesStatistic");
        }
    }
}
