﻿
namespace DDOT.MPS.Permit.DataAccess.Repositories.TestRepository
{
    public class TestRepository : ITestRepository
    {
        public Task<string> Create(string name)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAll(string filters)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(string name)
        {
            throw new NotImplementedException();
        }
    }
}
