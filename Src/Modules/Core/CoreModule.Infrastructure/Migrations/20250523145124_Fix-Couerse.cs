using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCouerse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseActionStatus",
                schema: "course",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseActionStatus",
                schema: "course",
                table: "Courses");
        }
    }
}
