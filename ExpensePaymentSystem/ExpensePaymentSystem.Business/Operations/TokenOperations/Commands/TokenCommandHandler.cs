using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpensePaymentSystem.Base.Encryption;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Base.Token;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace ExpensePaymentSystem.Business.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly JwtConfig jwtConfig;

    public TokenCommandHandler(ExpensePaymentSystemDbContext dbContext,IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
    }
    
    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<Employee>().Where(x => x.UserName == request.Model.UserName)
            .FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        string hash = Md5Extension.GetHash(request.Model.Password.Trim());
        if (hash != user.Password)
        {
            user.LastActivityDate = DateTime.UtcNow;
            user.PasswordRetryCount++;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        if (user.Status != 1)
        {
            return new ApiResponse<TokenResponse>("Invalid user status");
        }
        if (user.PasswordRetryCount > 3)
        {
            return new ApiResponse<TokenResponse>("Invalid user status");
        }
        
        user.LastActivityDate = DateTime.UtcNow;
        user.PasswordRetryCount = 0;
        await dbContext.SaveChangesAsync(cancellationToken);

        string token = Token(user);

        return new ApiResponse<TokenResponse>( new TokenResponse()
        {
            Token = token,
            ExpireDate =  DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
        });
    }
    
    private string Token(Employee user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
    
    private Claim[] GetClaims(Employee user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("UserName", user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

        return claims;
    }
}