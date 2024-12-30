using AutoMapper;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.EwrRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.DataAccess.Repositories.ApplicationTypeRepository
{
    public class MpsApplicationTypeRepository: IMpsApplicationTypeRepository
    {
        private readonly MpsDbContext _dbContext;
        private readonly IMapper _mapper;

        public MpsApplicationTypeRepository(MpsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApplicationType> GetApplicationTypeByTypeCode(string applicationTypeCode)
        {
            return await _dbContext.ApplicationTypes.Where(t => t.ApplicationTypeCode == applicationTypeCode).FirstAsync();
        }
    }
}
