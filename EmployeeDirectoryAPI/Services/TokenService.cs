using EmployeeDirectoryAPI.Models;
using EmployeeDirectoryAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly EmpContext _dbContext;
        public TokenService(EmpContext dbContext)
        {
            _dbContext = dbContext;


        }

        public async Task<List<Token>> GetAsync()
        {
            List<Token> tokens;
            try
            {
                tokens = await _dbContext.Tokens.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return tokens;
        }

        public async Task<ResponseModel> AddAsync(Token token)
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                var result = await _dbContext.AddAsync<Token>(token);

                if (await _dbContext.SaveChangesAsync() < 0)
                {
                    return new ResponseModel()
                    {
                        IsSuccess = false,
                        Message = "Token Blacklisting Failed"
                    };
                }
                responseModel.IsSuccess = true;
                responseModel.Message = "Token Blacklisted Succesfully";

            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = $"Error:{ex.Message}";
            }

            return responseModel;
        }
    }
}
