using AcademyProjectModels;
using AcademyProjectSL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet(nameof(ViewShoppingCart))]
        public async Task<IActionResult> ViewShoppingCart(int userId)
        {
            var result = await _shoppingCartService.GetContent(userId);
            return Ok(result);
        }

        [HttpPost(nameof(AddToShoppingCart))]
        public async Task<IActionResult> AddToShoppingCart(int userId, Book book)
        {
            var result = _shoppingCartService.AddToCart(userId, book);
            return Ok(result);
        }

        [HttpGet(nameof(DeleteFromShoopingCart))]
        public async Task<IActionResult> DeleteFromShoopingCart(int userId, Book book)
        {
            var result = _shoppingCartService.RemoveFromCart(userId, book);
            return Ok(result);
        }

        [HttpGet(nameof(DeleteAllFromShoppingCart))]
        public async Task<IActionResult> DeleteAllFromShoppingCart(int userId)
        {
            var result = _shoppingCartService.EmptyCart(userId);
            return Ok(result);
        }
    }
}
