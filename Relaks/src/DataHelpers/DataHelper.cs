using Relaks.Models;

namespace Relaks.DataHelpers;

public static partial class DataHelper
{
    public static readonly List<string> EntryDiscriminators = new()
    {
        nameof(EPerson), nameof(ECompany), nameof(EMeet)
    };
    
    public static readonly List<string> EntryInfoDiscriminators = new()
    {
        nameof(EiPhone), nameof(EiEmail), nameof(EiDate), nameof(EiUrl), nameof(EiDataset)
    };
    
    public static readonly List<string> EntryInfoContactDiscriminators = new()
    {
        nameof(EiPhone), nameof(EiEmail), nameof(EiUrl), nameof(EiDate)
    };
}