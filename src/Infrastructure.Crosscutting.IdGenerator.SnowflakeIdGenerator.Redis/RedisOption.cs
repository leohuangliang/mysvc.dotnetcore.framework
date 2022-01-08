namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis
{
    public class RedisOption: SnowflakeOption
    {
        public int Database { get; set; }
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
    }
}