using Microsoft.EntityFrameworkCore.Migrations;

namespace Itopya.Infrastructure.Migrations
{
    public partial class spCategoryDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC spCategoryDelete 
                                    @Id int
                                    AS 
                                    BEGIN
                                    UPDATE Categories
                                           SET ParentCategoryId = null Where ParentCategoryId = @Id

                                    DELETE FROM CategoryBundle Where CategoryId = @Id
                                    DELETE FROM Products Where CategoryId =@Id
                                    DELETE FROM Categories Where ID = @Id
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
