using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBlog.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE VIEW post_detail_view AS
                SELECT p.id                                   AS post_id
                     , p.title                                AS post_title
                     , p.text                                 AS post_text
                     , CONCAT(u.first_name, ' ', u.last_name) AS author
                FROM posts p
                         LEFT JOIN users u ON u.id = p.user_id
                """);
            
            migrationBuilder.Sql(
                """
                CREATE VIEW user_post_stats_view AS
                SELECT u.id                                   AS user_id
                     , CONCAT(u.first_name, ' ', u.last_name) AS author
                     , COUNT(p.id) AS posts_count
                FROM posts p
                         LEFT JOIN users u ON u.id = p.user_id
                GROUP BY u.id, author
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW post_detail_view");
            migrationBuilder.Sql("DROP VIEW user_post_stats_view");
        }
    }
}
