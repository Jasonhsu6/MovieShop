using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieShop.Infrastructure.Migrations
{
    public partial class MovieGenreRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trailer_Movie_MovieId1",
                table: "Trailer");

            migrationBuilder.DropIndex(
                name: "IX_Trailer_MovieId1",
                table: "Trailer");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "Trailer");

            migrationBuilder.DropColumn(
                name: "TrailerId",
                table: "Movie");

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trailer_MovieId",
                table: "Trailer",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailer_Movie_MovieId",
                table: "Trailer",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trailer_Movie_MovieId",
                table: "Trailer");

            migrationBuilder.DropTable(
                name: "GenreMovie");

            migrationBuilder.DropIndex(
                name: "IX_Trailer_MovieId",
                table: "Trailer");

            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "Trailer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrailerId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trailer_MovieId1",
                table: "Trailer",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Trailer_Movie_MovieId1",
                table: "Trailer",
                column: "MovieId1",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
