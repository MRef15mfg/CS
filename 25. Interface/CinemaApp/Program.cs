using System;
using System.Collections;
using System.Collections.Generic;

namespace CinemaApp
{
    public enum Genre
    {
        Action,
        Comedy,
        Drama,
        SciFi,
        Horror,
        Documentary
    }

    public class Director : ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public object Clone()
        {
            return new Director
            {
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public class Movie : IComparable<Movie>, ICloneable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Director MovieDirector { get; set; }
        public string Country { get; set; }
        public Genre MovieGenre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }

        public int CompareTo(Movie other)
        {
            if (other == null) return 1;
            return string.Compare(this.Title, other.Title, StringComparison.OrdinalIgnoreCase);
        }

        public object Clone()
        {
            return new Movie
            {
                Title = this.Title,
                Description = this.Description,
                MovieDirector = (Director)this.MovieDirector?.Clone(),
                Country = this.Country,
                MovieGenre = this.MovieGenre,
                Year = this.Year,
                Rating = this.Rating
            };
        }

        public override string ToString()
        {
            return $"\"{Title}\" ({Year}) | {MovieGenre} | Рейтинг: {Rating:F1} | Режисер: {MovieDirector}";
        }
    }

    public class MovieYearComparer : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.Year.CompareTo(y.Year);
        }
    }

    public class MovieRatingComparer : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            return y.Rating.CompareTo(x.Rating);
        }
    }

    public class Cinema : IEnumerable<Movie>
    {
        private List<Movie> _movies = new List<Movie>();

        public void AddMovie(Movie movie)
        {
            if (movie != null)
            {
                _movies.Add(movie);
            }
        }

        public void Sort(IComparer<Movie> comparer)
        {
            _movies.Sort(comparer);
        }

        public void Sort()
        {
            _movies.Sort();
        }

        public IEnumerator<Movie> GetEnumerator()
        {
            return _movies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"Кількість фільмів у кінотеатрі: {_movies.Count}";
        }
    }

    class Program
    {
        static void Main()
        {
            Cinema cinema = new Cinema();

            cinema.AddMovie(new Movie
            {
                Title = "Inception",
                Description = "Sci-fi heist film",
                Country = "USA",
                Year = 2010,
                Rating = 8.8,
                MovieGenre = Genre.SciFi,
                MovieDirector = new Director { FirstName = "Christopher", LastName = "Nolan" }
            });

            cinema.AddMovie(new Movie
            {
                Title = "The Matrix",
                Description = "Dystopian sci-fi",
                Country = "USA",
                Year = 1999,
                Rating = 8.7,
                MovieGenre = Genre.SciFi,
                MovieDirector = new Director { FirstName = "Lana", LastName = "Wachowski" }
            });

            cinema.AddMovie(new Movie
            {
                Title = "Interstellar",
                Description = "Space exploration drama",
                Country = "USA",
                Year = 2014,
                Rating = 8.6,
                MovieGenre = Genre.SciFi,
                MovieDirector = new Director { FirstName = "Christopher", LastName = "Nolan" }
            });

            Console.WriteLine("Сортування за назвою (IComparable за замовчуванням):");
            cinema.Sort();
            foreach (var movie in cinema)
            {
                Console.WriteLine(movie);
            }

            Console.WriteLine("\nСортування за роком (IComparer):");
            cinema.Sort(new MovieYearComparer());
            foreach (var movie in cinema)
            {
                Console.WriteLine(movie);
            }

            Console.WriteLine("\nСортування за рейтингом (IComparer):");
            cinema.Sort(new MovieRatingComparer());
            foreach (var movie in cinema)
            {
                Console.WriteLine(movie);
            }
        }
    }
}
