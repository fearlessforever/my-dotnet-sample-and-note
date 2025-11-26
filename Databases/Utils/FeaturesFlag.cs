using Fearlessforever.Shared.Configs;

namespace Fearlessforever.Databases.Utils;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class FeatureFlagAttribute(Features featureSelected) : Attribute
{
  private static FeaturesConfig FeaturesConfig = new();
  public Features FeatureSelected { get; set; } = featureSelected;

  // This method will be called to set active features configuration.
  public static void SetFeaturesConfig(FeaturesConfig featuresConfig)
  {
    FeaturesConfig = featuresConfig;
  }

  public bool IsFeatureEnabled()
  {
    bool result = FeatureSelected switch
    {
      Features.UseDatabaseInMemory => FeaturesConfig.UseDatabaseInMemory,
      Features.UseDatabaseSqlite => FeaturesConfig.UseDatabaseSqlite,
      Features.UseDatabaseMsSql => FeaturesConfig.UseDatabaseMsSql,
      Features.UseDatabasePostgreSql => FeaturesConfig.UseDatabasePostgreSql,
      _ => false
    };

    return result;
  }
}

internal enum Features
{ 
  UseDatabaseSqlite,
  UseDatabaseInMemory,
  UseDatabaseMsSql,
  UseDatabasePostgreSql,
}