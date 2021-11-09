using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.IRepositories;
using Infrastructure.Static.Data.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace Test
{
    public class Test
    {
         private readonly TextReader _mockReader = new StreamReader(@"../../../../Test/rating.json");

         #region GetNumberOfReviewsFromReviewer
        [Fact]
        public void GetNumberOfReviewsFromReviewer()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            int result = repository.GetNumberOfReviewsFromReviewer(1);
            
            Assert.Equal(10 ,result);
        }
        
        [Fact]
        public void GetNumberOfReviewsFromReviewerZero()
        {
            int reviewer = 0;
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetNumberOfReviewsFromReviewer(reviewer));
            Assert.Equal("No reviewer was found" ,ex.Message);
        }
        #endregion

        #region GetAverageRateFromReviewer
        [Fact]
        public void GetAverageRateFromReviewer()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            double result = repository.GetAverageRateFromReviewer(1);
            
            Assert.Equal(3.6,result);
        }
        
        [Fact]
        public void GetAverageRateFromReviewerZero()
        {
            int reviewer = 0;
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetAverageRateFromReviewer(reviewer));
            Assert.Equal("No reviewer was found" ,ex.Message);
        }
        #endregion

        #region GetNumberOfRatesByReviewer
        [Fact]
        public void GetNumberOfRatesByReviewer()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            double result = repository.GetNumberOfRatesByReviewer(1, 5);
            Assert.Equal(1,result);
        }

        [Fact]
        public void GetNumberOfRatesByReviewerNoReviewer()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetNumberOfRatesByReviewer(100, 5));
            
            Assert.Equal("No rates where found for reviewer" ,ex.Message);
        }
        
        [Fact]
        public void GetNumberOfRatesByReviewerRatingAboveRange()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetNumberOfRatesByReviewer(1, 15));
            Assert.Equal("The rating is above range" ,ex.Message);
        }
        
        [Fact]
        public void GetNumberOfRatesByReviewerRatingBellowRange()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetNumberOfRatesByReviewer(1, -5));
            Assert.Equal("The rating is bellow range" ,ex.Message);
        }
        
        [Fact]
        public void GetNumberOfRatesByReviewerRatingNotFound()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetNumberOfRatesByReviewer(29, 5));
            Assert.Equal("No rates where found for reviewer" ,ex.Message);
        }
        #endregion
        
        #region GetNumberOfReviews
        [Fact]
        public void GetNumberOfReviews()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            double result = repository.GetNumberOfReviews(1488844);
            
            Assert.Equal(4,result);
        }
        #endregion

        #region GetAverageRateOfMovie
        [Fact]
        public void GetAverageRateOfMovie()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            double result = repository.GetAverageRateOfMovie(1488844);
            
            Assert.Equal(4,result);
        }
        #endregion

        #region GetNumberOfRates
        [Fact]
        public void GetNumberOfRates()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            double result = repository.GetNumberOfRates(1488844,4);
            
            Assert.Equal(2,result);
        }
        
        [Fact]
        public void GetNumberOfRatesMovieBellowZero()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetNumberOfRates(-1,1));
            Assert.Equal("Movie Id must be above zero" ,ex.Message);
        }
        
        [Fact]
        public void GetNumberOfRatesRateOutsideTheRangeBellowZero()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetNumberOfRates(1,-11));
            Assert.Equal("The rating is bellow range" ,ex.Message);
        }
        
        [Fact]
        public void GetNumberOfRatesRateAboveRange()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetNumberOfRates(1,10));
            Assert.Equal("The rating is above range" ,ex.Message);
        }
        #endregion

        #region GetMoviesWithHighestNumberOfTopRates
        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            List<int> result = repository.GetMoviesWithHighestNumberOfTopRates();
            
            Assert.Equal(new List<int>{1,2,3,4,5},result);
        }
        #endregion

        #region GetMostProductiveReviewers
        [Fact]
        public void GetMostProductiveReviewers()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            List<int> result = repository.GetMostProductiveReviewers();
            
            Assert.Equal(result, result);
        }
        #endregion

        #region GetTopRatedMovies
        [Fact]
        public void GetTopRatedMovies()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            List<int> result = repository.GetTopRatedMovies(4).Take(4).ToList();
            
            Assert.Equal(new List<int>{1488844, 1358911, 1507649, 845529}, result);
        }
        
        [Fact]
        public void GetTopRatedMoviesAmountBellowZero()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetTopRatedMovies(-1));
            Assert.Equal("Amount must be above zero" ,ex.Message);
        }
        
        [Fact]
        public void GetTopRatedMoviesAmountOutsideTheRange()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetTopRatedMovies(50000000));
            Assert.Equal("Amount is outside the range" ,ex.Message);
        }
        #endregion

        #region GetTopMoviesByReviewer
        [Fact]
        public void GetTopMoviesByReviewer()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            List<int> result = repository.GetTopMoviesByReviewer(2).Take(5).ToList();
            
            Assert.Equal(new List<int>{1358911,1507649, 845529, 1479907, 1636093}, result);
        }
        
        [Fact]
        public void GetTopMoviesByReviewerIdBellowZero()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetTopMoviesByReviewer(-2));
            Assert.Equal("The reviewer Id must be above zero" ,ex.Message);
        }
        
        [Fact]
        public void GetTopMoviesByReviewerIdAboveTheRange()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetTopMoviesByReviewer(1000));
            Assert.Equal("Id is above range" ,ex.Message);
        }
        #endregion

        #region GetReviewersByMovie
        [Fact]
        public void GetReviewersByMovie()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);
            List<int> result = repository.GetReviewersByMovie(4);
            
            Assert.Equal(result, result);
        }
        
        [Fact]
        public void GetReviewersByMovieIdBellowZero()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetReviewersByMovie(-2));
            Assert.Equal("Movie Id must be above zero" ,ex.Message);
        }
        
        [Fact]
        public void GetReviewersByMovieIdAboveTheRange()
        {
            IBEReviewRepository repository = new BEReviewRepository(_mockReader);

            var ex = Assert.Throws<InvalidOperationException>(() => repository.GetReviewersByMovie(10000000));
            Assert.Equal("Movie Id is above range" ,ex.Message);
        }



        #endregion
    }
}