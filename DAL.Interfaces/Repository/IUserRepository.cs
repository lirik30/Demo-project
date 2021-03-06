﻿using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetUserByLogin(string login);
    }
}
