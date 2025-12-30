namespace cYo.Projects.ComicRack.Engine.IO.Provider.XmlInfo;

[XmlInfoFile("ComicInfo.xml", 0)]
public class ComicInfoProvider : XmlInfoProvider<ComicInfo>
{
    public override ComicInfo ToComicInfo(ComicInfo xmlInfo) => xmlInfo;
}
