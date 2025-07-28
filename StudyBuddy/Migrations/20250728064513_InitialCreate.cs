using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionsAndAnswers",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Questions = table.Column<string>(type: "TEXT", maxLength: 3000, nullable: true),
                    Answers = table.Column<string>(type: "TEXT", maxLength: 3000, nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__0DC06F8C31A4CDE3", x => x.QuestionID);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavoriteID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<string>(type: "TEXT", maxLength: 3000, nullable: true),
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: true),
                    AnswerID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavoriteID);
                    table.ForeignKey(
                        name: "FK__Favorites__Answe__70DDC3D8",
                        column: x => x.AnswerID,
                        principalTable: "QuestionsAndAnswers",
                        principalColumn: "QuestionID");
                    table.ForeignKey(
                        name: "FK__Favorites__Quest__6FE99F9F",
                        column: x => x.QuestionID,
                        principalTable: "QuestionsAndAnswers",
                        principalColumn: "QuestionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AnswerID",
                table: "Favorites",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_QuestionID",
                table: "Favorites",
                column: "QuestionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "QuestionsAndAnswers");
        }
    }
}
