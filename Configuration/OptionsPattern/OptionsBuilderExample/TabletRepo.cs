namespace OptionsBuilderExample;

public interface ITabletRepo
{
    Tablet GetBestModel();
}
public class TabletRepo: ITabletRepo
{
    public Tablet GetBestModel()
    {
        return new Tablet()
        {
            Name = "iPad",
            Model = "1234"
        };
    }
}