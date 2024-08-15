﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_Project_cw2.Models;
using Microsoft.IdentityModel.Tokens;

namespace API_Project_cw2;

public static class JwtGenerator
{
    public static string GenerateJwt(User user, string token, DateTime expiryDate)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            expires: expiryDate,
            signingCredentials: creds);
        
        var jwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return jwt;
    }
}