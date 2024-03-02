using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data.DataSeed
{
    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder.HasData(SeedStatistic());
        }

        private Statistic SeedStatistic()
        {
            return new Statistic()
            {
                Id = 1,
                ReportedPostsDeletedCount = 0,
                AllTimeUsersCount = 0
            };
        }
    }
}
