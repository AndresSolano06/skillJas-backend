using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skillJas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseUrlToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseUrl",
                table: "Courses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseUrl",
                table: "Courses");
        }
    }
}
