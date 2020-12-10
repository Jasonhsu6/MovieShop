using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetReviewsByUserId(int userid)
        {
            var reviews = await _userService.GetAllReviewsByUser(userid);
            if (reviews == null)
            {
                return BadRequest("No favorites found");
            }
            return Ok(reviews);
        }
        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> AddNewReview(ReviewRequestModel reviewRequest)
        {
            await _userService.AddMovieReview(reviewRequest);
            return Ok();
        }
        [HttpPut("review")]
        public async Task<ActionResult> UpdateReview([FromBody] ReviewRequestModel reviewRequest)
        {
            await _userService.UpdateMovieReview(reviewRequest);
            return Ok();
        }


        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetPurchasesByUserId(int userid)
        {
            var purchases = await _userService.GetAllPurchasesForUser(userid);
            if (purchases == null)
            {
                return BadRequest("No reviews found");
            }
            return Ok(purchases);
        }
        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> AddNewPurchase(PurchaseRequestModel purchaseRequest)
        {
            await _userService.PurchaseMovie(purchaseRequest);
            return Ok();
        }


        [HttpGet]
        [Route("{id:int}/favorites")]
        public async Task<IActionResult> GetFavoritesByUserId(int userid)
        {
            var favorites = await _userService.GetAllFavoritesForUser(userid);
            if (favorites == null)
            {
                return BadRequest("No reviews found");
            }
            return Ok(favorites);
        }
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddNewFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _userService.AddFavorite(favoriteRequest);
            return Ok();
        }
        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _userService.RemoveFavorite(favoriteRequest);
            return Ok();
        }
        [HttpGet]
        [Route("{id:int}/movie/{movieId:int}/favorite")]
        public async Task<IActionResult> CheckFavorite(int id, int movieId)
        {
            var favorite = await _userService.FavoriteExists(id, movieId);
            return Ok(favorite);
        }
    }
}
