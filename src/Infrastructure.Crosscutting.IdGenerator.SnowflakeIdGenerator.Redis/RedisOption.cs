namespace MySvc.Framework.Infrastructure.Crosscutting.SnowflakeIdGenerator.Redis
{
    public class RedisOption: SnowflakeOption
    {
        public int Database { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
        public string InstanceName { get; set; } = string.Empty;
    }
}