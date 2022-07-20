using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class OrcamentoItem
    {

        public OrcamentoItem(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoProduto produto, decimal quantidade, OrcamentoItemPreco preco)
        {
            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            NumOrcamento = numOrcamento;
            Produto = produto;
            Quantidade = quantidade;
            Situacao = OrcamentoItemStatusEnum.Pendente;
            AtrubuirPreco(preco);

        }

        public int Seq { get; private set; }
        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public int NumOrcamento { get; private set; }
        public OrcamentoProduto Produto { get; private set; }
        public decimal Quantidade { get; private set; }

        public OrcamentoItemPreco PrecoItem { get; private set; }
        public decimal PercAltPreco { get; private set; }
        public decimal PrecoVenda { get; private set; }
        public decimal Total { get; private set; }
        public OrcamentoItemStatusEnum Situacao { get; private set; }

        // Associar o ValueObject OrcamentoItemPreco a entidade OrcamentoItem

        #region setters
        private void AtrubuirPreco(OrcamentoItemPreco preco)
        {
            PrecoItem = preco;
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            if (Quantidade < 0)
                throw new ArgumentOutOfRangeException(nameof(Quantidade));

            if (PrecoVenda < 0)
                new ArgumentOutOfRangeException(nameof(PrecoVenda));

            Total = Quantidade * PrecoVenda;
        }
        #endregion


        #region validations

        public List<string> Validations;
        public bool IsValid()
        {
            Validations = new List<string>();

            if (string.IsNullOrEmpty(CdEmpresa))
                Validations.Add("Código da empresa é requirido!");

            if (string.IsNullOrEmpty(CdFilial))
                Validations.Add("Código da filial é requirido!");

            if (Seq < 0)
                Validations.Add("A sequência do item é requirida");

            if (NumOrcamento < 0)
                Validations.Add("O número do orçamento é requirido");

            if (string.IsNullOrEmpty(Produto.CdProduto))
                Validations.Add("O tipo e código do produto é requirido");

            if (Quantidade < 0)
                Validations.Add("A quantidade não pode ser igual ou menor que zero");
         

            //NumOrcamento
            // validações acresentadas 19/07/22 :  se a sequência do item não estiver informada, número do orçamento, se estiver sem cdproduto e se a qtd é <=0

                if (Validations.Count > 0)
                return false;
            else
                return true;
        }
        #endregion
    }
}