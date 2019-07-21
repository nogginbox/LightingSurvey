﻿using LightingSurvey.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LightingSurvey.Data
{
    public interface IDataContext
    {
        DbSet<SurveyResponse> Responces { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}