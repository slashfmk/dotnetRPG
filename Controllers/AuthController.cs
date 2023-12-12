using dotnetRPG.Data;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRPG.Controllers;

public class AuthController : BaseApiController
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
    {
        var response = await _authRepository.Register(
            new User { Username = request.Username },
            request.Password
        );

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserRegisterDto request)
    {
        var response = await _authRepository.Login(request.Username, request.Password);

        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }
}