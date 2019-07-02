using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Web.Api.Infrastructure.Guards;
using Web.Api.Infrastructure.Repositories;
using Web.Api.Modules.Auth;
using Web.Api.Modules.Auth.Domain;
using Web.Api.src.Infrastructure.Guards;
using Xunit;

namespace Tests
{
    public class Tests
    {
        private  Mock<TokenConfigurations> _tokenConfigurations;
        private  Mock<AuthService> _authService;
        private  Mock<IMapper> _mapper;
        private  Mock<IDistributedCache> _IDistributedCache;

        private  Mock<Jwt> _Jwt;
        private IdentityResult awaiy;

        public Tests ()
        {
            _tokenConfigurations = new Mock<TokenConfigurations>();
        }

        [Test]
        public void ReturnIfTokenIsValid_Success()
        {
            var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImhlbGxvQHdvcmxkMi5jb20iLCJuYmYiOjE1NjIwMzY4NDYsImV4cCI6MTU2MjAzNjkwNiwiaWF0IjoxNTYyMDM2ODQ2fQ.-XzUhLSWAvdg2r_yotfz4cD9Tty3byTs_6jkUyS0kQY";

            var jwtMock = new Mock<Jwt>(_tokenConfigurations.Object);

            var payload = new JwtDto();
            payload.unique_name = "hello@world2.com";

            Xunit.Assert.Equal(payload.unique_name, jwtMock.Object.DecodeToken(token).unique_name);

        }

        [Test]
        public void ReturnIfTokenIsInvalid_Error()
        {
            var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImhlbGxvQHdvcmxkMi5jb20iLCJuYmYiOjE1NjIwMzY4NDYsImV4cCI6MTU2MjAzNjkwNiwiaWF0IjoxNTYyMDM2ODQ2fQ.-XzUhLSWAvdg2r_yotfz4cD9Tty3byTs_6jkUyS0kQY";

            var jwtMock = new Mock<Jwt>(_tokenConfigurations.Object);

            var payload = new JwtDto();
            payload.unique_name = "2";

            Xunit.Assert.NotEqual(payload.unique_name, jwtMock.Object.DecodeToken(token).unique_name);
        }

        [Test]
        public void GenerateTokenUser_Success()
        {

            var jwtMock = new Mock<Jwt>(_tokenConfigurations.Object);

            var userMock = new User();
            userMock.Email = "hello@world2.com";
            userMock.UserName = "hello@world2.com";


            Xunit.Assert.Equal("OK", jwtMock.Object.GenerateToken(userMock).Message);
        }

      
        [Test]

        public async Task ReturnIfCurrentUserIsValid_SuccessAsync()
        {
          
        
        } 


        [Test]
        public async Task ReturnIfUserIsLogger_Success()
        {
            var userMock = new Mock<User>();
            userMock.Object.UserName = "hello@world2.com";
            userMock.Object.Email = "hello@world2.com";
            userMock.Object.FirstName = "Luiz";
            userMock.Object.Created_At = DateTime.Now;
            userMock.Object.Last_Login = DateTime.Now;
            userMock.Object.Password = "teste123";

      
            var _jwtMock = new Mock<Jwt>(_tokenConfigurations.Object);

            var _signinManager = new Mock<FakeSignInManager>();

            var _userManager = new Mock<FakeUserManager>();
            IdentityResult result = await _userManager.Object.CreateAsync(userMock.Object, userMock.Object.Password);

            var signResult = _signinManager.Object.PasswordSignInAsync("hello@world2.com", "teste123", true, false);

            Xunit.Assert.True(signResult.IsCompleted);
        }

        [Test]
        public async Task ReturnIfUserCreated_Success()
        {
            var userMock = new Mock<User>();
            userMock.Object.UserName = "hello@world2.com";
            userMock.Object.Email = "hello@world2.com";
            userMock.Object.FirstName = "Luiz";
            userMock.Object.Created_At = DateTime.Now;
            userMock.Object.Last_Login = DateTime.Now;
            userMock.Object.Password = "teste123";


            var _userManager = new Mock<FakeUserManager>();
            var result =  _userManager.Object.CreateAsync(userMock.Object, userMock.Object.Password);
            Xunit.Assert.True(result.IsCompleted);
        }
      
       
    }

    public class FakeSignInManager : SignInManager<User>
    {
        public FakeSignInManager()
         : base(new Mock<FakeUserManager>().Object,
              new Mock<IHttpContextAccessor>().Object,
              new Mock<IUserClaimsPrincipalFactory<User>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<ILogger<SignInManager<User>>>().Object,
              new Mock<IAuthenticationSchemeProvider>().Object)
        { }
    }

    public class FakeUserManager : UserManager<User>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<User>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<User>>().Object,
              new IUserValidator<User>[0],
              new IPasswordValidator<User>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<User>>>().Object)
        { }
    }
    }