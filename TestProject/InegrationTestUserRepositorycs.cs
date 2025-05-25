using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class InegrationTestUserRepositorycs: IClassFixture<DatabaseFixture>
    {
        private readonly _326059268_ShopApiContext _dbContext;
        private readonly UserRepository _userRepository;
        public InegrationTestUserRepositorycs()
        {
            
        }
    }
}
