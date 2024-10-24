namespace LazyLoad.Ghost;

public enum LoadStatus
{
    Ghost,      // Ghost - not loaded yet
    Loading,    // Loading - in loading process
    Loaded      // Loaded - loaded from database/external resource
}