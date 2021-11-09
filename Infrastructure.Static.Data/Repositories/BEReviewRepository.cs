using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Models;
using Domain.IRepositories;
using Newtonsoft.Json;

namespace Infrastructure.Static.Data.Repositories
{
    public class BEReviewRepository : IBEReviewRepository
    {

        private readonly List<BEReview> _allReviews;

        public BEReviewRepository(TextReader textReader)
        {
            string jsonAsString = textReader.ReadToEnd();
            _allReviews = JsonConvert.DeserializeObject<List<BEReview>>(jsonAsString);
        }
        
        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            int numberOfReviews = _allReviews.Count(review => review.Reviewer == reviewer);
            if (numberOfReviews == 0)
            {
                throw new InvalidOperationException("No reviewer was found");
            }

            return numberOfReviews;
        }
        public double GetAverageRateFromReviewer(int reviewer)
        {
            int numberOfReviews = GetNumberOfReviewsFromReviewer(reviewer);
            IEnumerable<int> totalReviewsArray = _allReviews.Where(r => r.Reviewer == reviewer).Select(r => r.Grade);
            int totalReviews = 0;

            if (totalReviewsArray.Count() == null)
            {
                throw new InvalidOperationException("No reviewer was found");
            }

            foreach (var review in totalReviewsArray)
            {
                totalReviews += review;
            }

            double averageRate = (double) totalReviews / numberOfReviews;
            return averageRate;
        }
        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            var correctReviewer = GetReviewer(reviewer);
            var correctRate = RateBelowOrAbove(rate);

            var numberOfRatesByReviewer = _allReviews
                .Where(r => r.Reviewer == correctReviewer).Count(rt => rt.Grade == correctRate);

            if (numberOfRatesByReviewer == 0)
            {
                throw new InvalidOperationException("No rates where found for reviewer");
            }
            return numberOfRatesByReviewer;
        }
        public int GetNumberOfReviews(int movie)
        {
            return _allReviews.Count(r => r.Movie == movie);
        }
        public double GetAverageRateOfMovie(int movie)
        {
            int numberOfReviews = GetNumberOfReviews(movie);
            IEnumerable<int> totalReviewsArray = _allReviews.Where(r => r.Movie == movie).Select(r => r.Grade);
            int totalReviews = 0;

            foreach (var review in totalReviewsArray)
            {
                totalReviews += review;
            }

            double averageRate = (double) totalReviews / numberOfReviews;
            return averageRate;
        }
        public int GetNumberOfRates(int movie, int rate)
        {
            var correctMovie = MovieBelowOrAbove(movie);
            var correctRate = RateBelowOrAbove(rate);
            return _allReviews
                .Where(r => r.Movie == correctMovie).Count(rt => rt.Grade == correctRate);
        }
        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            int highestId = _allReviews.Max(m => m.Movie);
            List<int> highestTopRates = new List<int>(5);

            for (int i = 1; i <= highestId; i++)
            {
                int numberOfTopScores = _allReviews.Where(m => m.Movie == i).Count(m => m.Grade == 5);
                if (i < 6)
                {
                    highestTopRates.Add(i);
                }
                else if (highestTopRates.Min() < numberOfTopScores)
                {
                    highestTopRates.Remove(highestTopRates.Min());
                    highestTopRates.Add(i);
                }
            }

            return highestTopRates;
        }
        public List<int> GetMostProductiveReviewers()
        {
            int highestReviewerId = _allReviews.Max(m => m.Reviewer);
            Dictionary<int, int> reviewerProductivity = new Dictionary<int, int>();

            for (int i = 1; i < highestReviewerId; i++)
            {
                int id = i;
                int count = _allReviews.Count(r => r.Reviewer == id);
                reviewerProductivity.Add(i, count);
            }

            return reviewerProductivity.OrderByDescending(key => key.Value).Select(value => value.Key).ToList();
        }
        private int MovieBelowOrAbove(int movie)
        {
            if (movie < 0)
            {
                throw new InvalidOperationException("Movie Id must be above zero");
            }

            if (movie > _allReviews.Max(m => m.Movie))
            {
                throw new InvalidOperationException("Movie Id is above range");
            }

            return movie;
        }
        public List<int> GetTopRatedMovies(int amount)
        {
            var correctAmount = AmountBelowOrAbove(amount);
            return _allReviews.OrderByDescending(review => review.Grade).Select(review => review.Movie).Distinct()
                .Take(correctAmount).ToList();
        }
        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            var rev = GetReviewer(reviewer);
            {
                return _allReviews.OrderByDescending(review => review.Grade)
                    .ThenByDescending(review => review.ReviewDate)
                    .Where(review => review.Reviewer == rev).Select(review => review.Movie).ToList();
            }
        }
        public List<int> GetReviewersByMovie(int movie)
        {
            var correctMovie = MovieBelowOrAbove(movie);
            return _allReviews.OrderByDescending(review => review.Grade).ThenByDescending(review => review.ReviewDate)
                .Where(review => review.Movie == correctMovie).Select(review => review.Reviewer).ToList();
        }
        private int GetReviewer(int reviewer)
        {
            return reviewer switch
            {
                <= 0 => throw new InvalidOperationException("The reviewer Id must be above zero"),
                > 999 => throw new InvalidOperationException("Id is above range"),
                _ => reviewer
            };
        }
        private int RateBelowOrAbove(int rate)
        {
            return rate switch
            {
                < 0 => throw new InvalidOperationException("The rating is bellow range"),
                > 5 => throw new InvalidOperationException("The rating is above range"),
                _ => rate
            };
        }
        private int AmountBelowOrAbove(int amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount must be above zero");
            }

            if (amount > _allReviews.Max(m => m.Movie))
            {
                throw new InvalidOperationException("Amount is outside the range");
            }
            return amount;
        }

    }
}