using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Movies.Pages;
namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                HashSet<string> genreSet = new HashSet<string>();
                foreach (Movie movie in movies)
                {
                    if (movie.MajorGenre != null)
                    {
                        genreSet.Add(movie.MajorGenre);
                    }
                }
                genres = genreSet.ToArray();
            }
        }

        // The genres represented in the database
        private static string[] genres;

        /// <summary>
        /// Gets the movie genres represented in the database 
        /// </summary>
        public static string[] Genres => genres;

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } }

        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();
            // Return all movies if there are no search terms
            if (terms == null) return All;

            // return each movie in the database containing the terms substring
            foreach(Movie movie in All)
            {
                if(movie.Title != null && movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }
            return results;
        }
        /// <summary>
        /// Gets the possible MPAARatings
        /// </summary>
        public static string[] MPAARatings
        {
            get => new string[]
            {
            "G",
            "PG",
            "PG-13",
            "R",
            "NC-17"
            };
        }

        /// <summary>
        /// Filters the provided collection of movies
        /// </summary>
        /// <param name="movies">The collection of movies to filter</param>
        /// <param name="ratings">The ratings to include</param>
        /// <returns>A collection containing only movies that match the filter</returns>
        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            // If no filter is specified, just return the provided collection
            if (min == null && max == null) return movies;

            // Filter the supplied collection of movies
            List<Movie> results = new List<Movie>();

            // only a maximum specified
            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating <= max) results.Add(movie);
                }
                return results;
            }

            // only a minimum specified 
            if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating >= min) results.Add(movie);
                }
                return results;
            }

            foreach (Movie movie in movies)
            {
                if (movie.MPAARating != null && (Convert.ToDouble(movie.IMDBRating) > min && Convert.ToDouble(movie.IMDBRating) < max))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public static IEnumerable<Movie> FilterByMPAARating(IEnumerable<Movie> movies, IEnumerable<string> ratings)
        {
            // If no filter is specified, just return the provided collection
            if (ratings == null || ratings.Count() == 0) return movies;

            // Filter the supplied collection of movies
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.MPAARating != null && ratings.Contains(movie.MPAARating))
                {
                    results.Add(movie);
                }
            }

            return results;
        }


        public static IEnumerable<Movie> FilterByGenre(IEnumerable<Movie> movies, IEnumerable<string> genres)
        {
            // If no filter is specified, just return the provided collection
            if (genres == null || genres.Count() == 0) return movies;

            // Filter the supplied collection of movies
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.MajorGenre != null && genres.Contains(movie.MajorGenre))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public static IEnumerable<Movie> FilterByRottenTomatoes(IEnumerable<Movie> movies, int? min, int? max)
        {
            // If no filter is specified, just return the provided collection
            if (min == null && max == null) return movies;

            // Filter the supplied collection of movies
            List<Movie> results = new List<Movie>();

            // only a maximum specified
            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating <= max) results.Add(movie);
                }
                return results;
            }

            // only a minimum specified 
            if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating >= min) results.Add(movie);
                }
                return results;
            }

            foreach (Movie movie in movies)
            {
                if (movie.MPAARating != null && (Convert.ToDouble(movie.IMDBRating) > min && Convert.ToDouble(movie.IMDBRating) < max))
                {
                    results.Add(movie);
                }
            }

            return results;
        }
    }
}
