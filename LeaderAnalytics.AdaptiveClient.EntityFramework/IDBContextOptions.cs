using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework
{
    public interface IDbContextOptions<T> where T : DbContext
    {
        DbContextOptions<T> Options { get; }
    }

    public interface IDbContextOptions
    {
        DbContextOptions Options { get; }
    }
}
