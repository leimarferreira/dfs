﻿using ProjetoDFS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> ListAsync();
    }
}