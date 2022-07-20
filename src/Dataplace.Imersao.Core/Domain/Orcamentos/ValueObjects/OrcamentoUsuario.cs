using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects
{
    public class OrcamentoUsuario
    {
        public OrcamentoUsuario(string user)
        {
            this.User = user;
        }

        public string User { get; private set; }
    }
}
