using System.ComponentModel;

namespace TestQuestion5.Domain.Enumerators;

public enum TipoMovimento
{
    [Description("Crédito")]
    C = 1,
    [Description("Débito")]
    D = 2
}