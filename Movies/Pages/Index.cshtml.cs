using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms 
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }

        [BindProperty]
        public int? TomMin { get; set; }
        [BindProperty]
        public int? TomMax { get; set; }
        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet()
        {
            //MPAARatings = Request.Query["MPAARatings"];
            //SearchTerms = Request.Query["SearchTerms"];
            //Genres = Request.Query["Genres"];
            //Movies = MovieDatabase.Search(SearchTerms);
            //Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            //Movies = MovieDatabase.FilterByRottenTomatoes(Movies, TomMin, TomMax);
            //Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            //Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.All;
    // Search movie titles for the SearchTerms
            if (SearchTerms != null)
            {
                Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase));
            }
            // Filter by MPAA Rating 
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie =>
                    movie.MPAARating != null &&
                    MPAARatings.Contains(movie.MPAARating)
                    );
            }
            if (TomMax != null || TomMin != null)
            {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating != null 
                && (movie.RottenTomatoesRating < TomMax && movie.RottenTomatoesRating > TomMin));
            }
            if (IMDBMax != null || IMDBMin != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating != null
                && (movie.IMDBRating < IMDBMax && movie.IMDBRating > IMDBMin));
            }
        }
    }
}
