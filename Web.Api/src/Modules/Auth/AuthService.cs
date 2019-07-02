using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Infrastructure.Guards;
using Web.Api.Infrastructure.Repositories;
using Web.Api.Modules.Auth.Domain;
using Web.Api.Modules.Auth.Dtos;
using Web.Api.src.Infrastructure.Guards;

namespace Web.Api.Modules.Auth {
    public class AuthService : IAuthService {

        private readonly IMapper _mapper;
        private readonly IRepositoryFacade _repositoryFacade;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly Jwt _jwt;

        public AuthService(
                IMapper mapper,
                IRepositoryFacade repositoryFacade,
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                Jwt jwt

         ) {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _repositoryFacade = repositoryFacade;
            _jwt = jwt;

        }

        public async Task<SignInResult> AuthenticateAsync(SigninDto dto)
        {
            return await _signInManager.PasswordSignInAsync(dto.email, dto.password, true, false);
        }

        public async Task<IdentityResult> CreateAsync(SignupDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.UserName = user.Email;

            IdentityResult result = await _userManager.CreateAsync(user, user.Password);

            if (result.Succeeded)
            {

                object data = new {
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    created_at = DateTime.Now,
                    last_login = DateTime.Now,
                    phones = user.Phones
                };
                _repositoryFacade.SetCache(
                    user.Email.ToString(), JsonConvert.SerializeObject(
                        data, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            }

            return result;

        }
   
        public async Task<User> FindAsync(SigninDto dto)
        {
            var cache = _repositoryFacade.GetCache(dto.email.ToString());
            UserDto userDto = JsonConvert.DeserializeObject<UserDto>(cache);

            if (userDto.email.ToString() == dto.email.ToString())
            {
                User map = _mapper.Map<User>(userDto);
                return map;
            }

             User user = await _userManager.FindByEmailAsync(dto.email);
             return user;
        }

        public async Task<UserDto> GetUserToken(string token)
        {
            JwtDto payload =_jwt.DecodeToken(token);

            var cache = _repositoryFacade.GetCache(payload.unique_name.ToString());

            if (cache == null)
            {
                User user = await _userManager.FindByEmailAsync(payload.unique_name.ToString());
                UserDto map = _mapper.Map<UserDto>(user);

                return map;
            }

            UserDto userDto = JsonConvert.DeserializeObject<UserDto>(cache);
            return userDto;

        }
    }
}