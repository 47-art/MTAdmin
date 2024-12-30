using Dapper;
using MTAdmin.Shared.Models;
using MTClient.Application.Services.Comman;
using MTClient.Application.Services.Interfaces;
using System.Data;
using System.Net;

namespace MTClient.Application.Services.Implementations
{
    public class SiteAnalyticsService : ISiteAnalyticsService
    {
        private readonly DapperContext _context;
        public SiteAnalyticsService(DapperContext context)
        {
            _context = context;
        }
        public async Task<Response<int>> SaveSiteRequest(SiteRequest request)
        {
            try
            {
                using (IDbConnection connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@StartTimestamp", request.StartTimestamp, DbType.DateTime2);
                    parameters.Add("@EndTimestamp", request.EndTimestamp, DbType.DateTime2);
                    parameters.Add("@Identity", request.Identity, DbType.String);
                    parameters.Add("@RemoteIpAddress", request.RemoteIpAddress.ToString(), DbType.String);
                    parameters.Add("@Method", request.Method, DbType.String);
                    parameters.Add("@UserAgent", request.UserAgent, DbType.String);
                    parameters.Add("@Path", request.Path, DbType.String);
                    parameters.Add("@Referer", request.Referer, DbType.String);
                    parameters.Add("@IsWebSocket", request.IsWebSocket, DbType.Boolean);
                    parameters.Add("@CountryCode", request.CountryCode, DbType.Int32);
                    parameters.Add("@RowsAffected", 0, DbType.Int32, ParameterDirection.Output);
                    parameters.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
                    await connection.QueryAsync("[dbo].[SAVE_SITE_REQUEST]", parameters, commandType: CommandType.StoredProcedure);
                    return Response<int>.Success(parameters.Get<int>("Id"), "saved");
                }
            }
            catch (Exception ex)
            {
                return Response<int>.Error(ex);
            }
        }
        internal async Task SaveGeoIpRange(IPAddress from, IPAddress to, int countryCode)
        {
            var bytesFrom = from.GetAddressBytes();
            var bytesTo = to.GetAddressBytes();

            Array.Resize(ref bytesFrom, 16);
            Array.Resize(ref bytesTo, 16);

            //using (var db = GetContext())
            //{
            //    await db.GeoIpRange.AddAsync(new SqlServerGeoIpRange
            //    {
            //        FromDown = BitConverter.ToInt64(bytesFrom, 0),
            //        FromUp = BitConverter.ToInt64(bytesFrom, 8),

            //        ToDown = BitConverter.ToInt64(bytesTo, 0),
            //        ToUp = BitConverter.ToInt64(bytesTo, 8),
            //        CountryCode = countryCode
            //    });
            //    await db.SaveChangesAsync();
            //}
        }
        //public async Task<long> CountUniqueIndentitiesAsync(DateTime day)
        //{
        //    var from = day.Date;
        //    var to = day + TimeSpan.FromDays(1);
        //    return 0;
        //}
        //public async Task<long> CountUniqueIndentitiesAsync(DateTime from, DateTime to)
        //{
        //    //using (var db = GetContext())
        //    //{
        //    //    return await db.WebRequest.Where(x => x.Timestamp >= from && x.Timestamp <= to).GroupBy(x => x.Identity).CountAsync();
        //    //}
        //    return 0;
        //}
        //public async Task<long> CountAsync(DateTime from, DateTime to)
        //{
        //    //using (var db = GetContext())
        //    //{
        //    //    return await db.WebRequest.Where(x => x.Timestamp >= from && x.Timestamp <= to).CountAsync();
        //    //}
        //    return 0;
        //}
        //public Task<IEnumerable<IPAddress>> IpAddressesAsync(DateTime day)
        //{
        //    //var from = day.Date;
        //    //var to = day + TimeSpan.FromDays(1);
        //    //return IpAddressesAsync(from, to);
        //    return null;
        //}

        //public async Task<IEnumerable<IPAddress>> IpAddressesAsync(DateTime from, DateTime to)
        //{
        //    //using (var db = GetContext())
        //    //{
        //    //    var ips = await db.WebRequest.Where(x => x.Timestamp >= from && x.Timestamp <= to)
        //    //        .Select(x => x.RemoteIpAddress)
        //    //        .Distinct()
        //    //        .ToListAsync();

        //    //    return ips.Select(IPAddress.Parse).ToArray();
        //    //}
        //    return null;
        //}
        //public async Task<IEnumerable<SiteRequest>> RequestByIdentityAsync(string identity)
        //{
        //    //using (var db = GetContext())
        //    //{
        //    //    return await db.WebRequest.Where(x => x.Identity == identity).Select(x => Mapper.Map<WebRequest>(x)).ToListAsync();
        //    //}
        //    return null;
        //}

        //public async Task PurgeRequestAsync()
        //{
        //    //using (var db = GetContext())
        //    //{
        //    //    await db.Database.EnsureCreatedAsync();
        //    //    db.WebRequest.RemoveRange(db.WebRequest);
        //    //    await db.SaveChangesAsync();
        //    //}
        //}

        //public async Task PurgeGeoIpAsync()
        //{
        //    //using (var db = GetContext())
        //    //{
        //    //    await db.Database.EnsureCreatedAsync();
        //    //    db.GeoIpRange.RemoveRange(db.GeoIpRange);
        //    //    await db.SaveChangesAsync();
        //    //}
        //}

        //public async Task<IEnumerable<SiteRequest>> InTimeRange(DateTime from, DateTime to)
        //{
        //    //using (var db = GetContext())
        //    //{
        //    //    return (await db.WebRequest.Where(x => x.Timestamp >= from && x.Timestamp <= to)
        //    //        .ToListAsync())
        //    //        .Select(x => Mapper.Map<WebRequest>(x))
        //    //        .ToList();
        //    //}
        //    return null;
        //}
    }
}
