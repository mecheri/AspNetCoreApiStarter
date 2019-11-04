using System.Data;

namespace AspNetCoreApiStarter.Dal.Base
{
    public class BaseDal
    {
        static BaseDal()
        {
            Dapper.SqlMapper.AddTypeHandler(new TimeStampTypeHandler());
        }

        public BaseDal()
        {
        }

        public IDbConnection Connection { get; set; }
    }
}
