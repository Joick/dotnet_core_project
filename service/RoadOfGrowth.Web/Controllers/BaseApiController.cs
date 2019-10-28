using Microsoft.AspNetCore.Mvc;
using RoadOfGrowth.Web.Models;
using System;
using System.Linq;
using System.Net;

namespace RoadOfGrowth.Web.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected ServiceReponse Result = new ServiceReponse();
        private static readonly ObjectResult FailObj = new ObjectResult(null) { StatusCode = (int)HttpStatusCode.InternalServerError };
        private static readonly int[] StatusCodes = {
            (int)HttpStatusCode.BadRequest,
            (int)HttpStatusCode.NotFound,
            (int)HttpStatusCode.Conflict,
            (int)HttpStatusCode.UnprocessableEntity };

        protected IActionResult Success(string message = null)
        {
            Result.Success(message);
            return Ok(Result);
        }

        protected IActionResult Success<T>(T data)
        {
            Result.Success(data);
            return Ok(Result);
        }

        protected IActionResult Success<T>(string message, T data)
        {
            Result.Success(message, data);
            return Ok(Result);
        }


        protected IActionResult Fail(int? statusCode = null, string message = null)
        {
            Result.Fail(message);

            if (statusCode == null || !StatusCodes.Contains(statusCode.Value))
            {
                return FailObj;
            }

            return new ObjectResult(null) { StatusCode = statusCode };
        }
    }
}