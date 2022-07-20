using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.Enums
{
    public enum OrcamentoItemStatusEnum
    {
        Pendente,
        Entregue,

    }

    public static class OrcamentoItemStatusEnumExtensions
    {
        public static string ToDataValue(this OrcamentoItemStatusEnum value)
        {
            return value == OrcamentoItemStatusEnum.Entregue ? "E" : "P";
        }
        public static OrcamentoItemStatusEnum ToOrcamentoStatusEnum(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return OrcamentoItemStatusEnum.Pendente;

            if (value == "P")
                return OrcamentoItemStatusEnum.Pendente;
            else
                return OrcamentoItemStatusEnum.Entregue;
        }
    }
}
