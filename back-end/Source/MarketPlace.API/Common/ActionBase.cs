using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.API.Common
{
    public class ActionBase : ControllerBase
    {
        protected async Task<IActionResult> ExecutaActionResult<T>(Func<Task<T>> function) where T : class
        {

            var result = await function();
            return Ok();
        }
    }
}
