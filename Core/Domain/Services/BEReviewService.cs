using System.Collections.Generic;
using Core.IServices;
using Domain.IRepositories;

namespace Domain.Services
{
    public class BEReviewService:IBEReviewService
    {

        private readonly IBEReviewRepository _BEReviewRepository;

        public BEReviewService(IBEReviewRepository reviewRepository)
        {
            _BEReviewRepository = reviewRepository;
        }


        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            return _BEReviewRepository.GetNumberOfReviewsFromReviewer(reviewer);
        }

        public double GetAverageRateFromReviewer(int reviewer)
        {
            return _BEReviewRepository.GetAverageRateFromReviewer(reviewer);
        }

        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            return _BEReviewRepository.GetNumberOfRatesByReviewer(reviewer, rate);
        }

        public int GetNumberOfReviews(int movie)
        {
            return _BEReviewRepository.GetNumberOfReviews(movie);
        }

        public double GetAverageRateOfMovie(int movie)
        {
            return _BEReviewRepository.GetAverageRateOfMovie(movie);
        }

        public int GetNumberOfRates(int movie, int rate)
        {
            return _BEReviewRepository.GetNumberOfRates(movie, rate);
        }

        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            return _BEReviewRepository.GetMoviesWithHighestNumberOfTopRates();
        }

        public List<int> GetMostProductiveReviewers()
        {
            return _BEReviewRepository.GetMostProductiveReviewers();
        }

        public List<int> GetTopRatedMovies(int amount)
        {
            return _BEReviewRepository.GetTopRatedMovies(amount);
        }

        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            return _BEReviewRepository.GetTopMoviesByReviewer(reviewer);
        }

        public List<int> GetReviewersByMovie(int movie)
        {
            return _BEReviewRepository.GetReviewersByMovie(movie);
        }
    }
}