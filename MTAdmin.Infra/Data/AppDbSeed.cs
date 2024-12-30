using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MTAdmin.Core.Entities.Country;
using MTAdmin.Core.Entities.PageMetaTags;
using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;

namespace MTAdmin.Infra.Data
{
    public static class AppDbSeed
    {
        public static async Task AddDefaultPageMetaTagData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var scopedProvider = scope.ServiceProvider;
                var context = scopedProvider.GetRequiredService<AppDbContext>();

                var pageValues = Enum.GetValues(typeof(ClientModule)).Cast<int>();
                List<PageMetaTag> pages = new List<PageMetaTag>();
                foreach (var item in pageValues)
                {
                    if(!await context.PageMetaTags.AnyAsync(x => x.PageId == item))
                    {
                        pages.Add(new PageMetaTag()
                        {
                            PageId = item,
                            CreatedAt = DateTime.UtcNow,
                            IsActive = true,
                            IsDeleted = false,
                            MetaTitle = null,
                            MetaKeywords = null,
                            MetaDesc = null,
                            CreatedBy = Guid.Empty.ToString()
                        });
                    }
                }
                if(pages.Count > 0)
                {
                    await context.PageMetaTags.AddRangeAsync(pages);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task AddCountriesInfo(IServiceProvider serviceProvider,IReadOnlyCollection<CountryModel> countries)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var scopedProvider = scope.ServiceProvider;
                var context = scopedProvider.GetRequiredService<AppDbContext>();
                List<Country> countryList = new List<Country>();
                
                foreach (var item in countries)
                {
                    if(!await context.Countries.AnyAsync(x => x.CountryCode == item.CountryCode && x.Alpha2Code == item.Alpha2Code))
                    {
                        countryList.Add(new Country()
                        {
                            Name = item.Name,
                            Alpha2Code = item.Alpha2Code,
                            Alpha3Code = item.Alpha3Code,
                            CountryCode = item.CountryCode,
                            OfficialName = item.OfficialName,
                            TopLevelDomain = item.TopLevelDomain
                        });
                    }
                }
                if(countryList.Count > 0)
                {
                    await context.Countries.AddRangeAsync(countryList);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
