using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace minimal_api_dotnet.Migrations
{
  /// <inheritdoc />
  public partial class AddUserEmail : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {

      migrationBuilder.AddColumn<string?>(
          name: "Email",
          table: "Users",
          nullable: true);

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Users");
    }
  }
}
