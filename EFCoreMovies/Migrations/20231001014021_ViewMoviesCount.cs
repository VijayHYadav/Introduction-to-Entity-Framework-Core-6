using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreMovies.Migrations
{
    /// <inheritdoc />
    public partial class ViewMoviesCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW dbo.MoviesWithCounts

            as
            
            select Id, Title,
            (select count(*) from GenreMovie where MoviesId = movies.Id) as AmountGenres,
            (select count(distinct moviesID) from CinemaHallMovie
                inner join CinemaHalls
                on CinemaHalls.Id = CinemaHallMovie.cinemaHallsId
                where MoviesId = movies.Id) as AmountCinemas,
            (select count(*) from MoviesActors where MovieId = movies.Id) as AmountActors
            from Movies;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.MoviesWithCounts");
        }
    }
}
