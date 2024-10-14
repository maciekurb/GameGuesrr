namespace GameGuessr.Shared.Common;

public interface IBuilder<out TBuildingType> where TBuildingType : class
{
    TBuildingType Build();
}

