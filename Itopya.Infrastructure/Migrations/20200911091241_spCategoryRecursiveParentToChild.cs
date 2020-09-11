using Microsoft.EntityFrameworkCore.Migrations;

namespace Itopya.Infrastructure.Migrations
{
    public partial class spCategoryRecursiveParentToChild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC spCategoryRecursiveParentToChild 
                                    @Id int
                                    AS 
                                    BEGIN
                                    WITH RCTE AS
                                    (
                                        SELECT Id, Categories.ParentCategoryId,Name
                                        FROM Categories WHERE Id = @Id
    
                                        UNION ALL
    
                                        SELECT nextDepth.Id  as ItemId, nextDepth.ParentCategoryId as ItemParentId, nextDepth.Name
                                        FROM Categories nextDepth
                                        INNER JOIN RCTE recursive ON nextDepth.ParentCategoryId = recursive.Id
                                    )
                                    
                                    SELECT Id, ParentCategoryId,Name
                                    FROM RCTE as hierarchie
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
