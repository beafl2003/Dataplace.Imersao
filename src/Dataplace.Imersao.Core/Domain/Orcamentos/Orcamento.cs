using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class Orcamento
    {
        private Orcamento(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoCliente cliente, 
            OrcamentoUsuario usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)

        // Implementar o ValueObject do usuário do Orçamento >> OrcamentoUsuario.cs
        {

            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            Cliente = cliente;
            NumOrcamento = numOrcamento;
            Usuario = usuario;
            Vendedor = vendedor;
            TabelaPreco = tabelaPreco;

            // default
            Situacao = OrcamentoStatusEnum.Aberto;
            DtOrcamento = DateTime.Now;
            ValorTotal = 0;
            Itens = new List<OrcamentoItem>();

        }

        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public int NumOrcamento { get; private set; }
        public OrcamentoCliente Cliente { get; private set; }
        public DateTime DtOrcamento { get; private set; }
        public decimal ValorTotal { get; private set; }
        public OrcamentoValidade Validade { get; private set; }
        public OrcamentoTabelaPreco TabelaPreco { get; private set; }
        public DateTime? DtFechamento { get; private set; }
        public OrcamentoVendedor Vendedor { get; private set; }
        public OrcamentoUsuario Usuario { get; private set; }
        public OrcamentoStatusEnum Situacao { get; private set; }
        public ICollection<OrcamentoItem> Itens { get; private set; }


        public void FecharOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Fechado;
            DtFechamento = DateTime.Now.Date;
        }

        public void ReabrirOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Aberto)
            {
                throw new DomainException("Orçamento já está Aberto!"); 
            }
            else if (Situacao == OrcamentoStatusEnum.Fechado)
            {
                Situacao = OrcamentoStatusEnum.Aberto;
                DtFechamento = null;
            }
            else if (Situacao == OrcamentoStatusEnum.Cancelado)
            {
                MessageBox.Show("Este orçamento foi cancelado, não é possível reabrí-lo");
            }
        }

        public void DefinirValidade(int diasValidade)
        {
            this.Validade = new OrcamentoValidade(this, diasValidade);
        }
        public void CancelarOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Cancelado)
            {
                throw new DomainException("Orçamento já Cancelado");
            }

            else if (Situacao == OrcamentoStatusEnum.Fechado)
            {

                throw new DomainException("Não é possível cancelar orçamentos já fechados. Tente reabrí-lo antes");
             }

            else if (Situacao == OrcamentoStatusEnum.Aberto)
            {
                Situacao = OrcamentoStatusEnum.Cancelado;
                DtFechamento = null;
            }


           
        }

        public void InserirItemOrcamento()
        {
            if (Situacao != OrcamentoStatusEnum.Aberto)
                throw new DomainException("Não é possível inserir itens em orçamentos que não estão abertos");

            var produto = new OrcamentoProduto(TpRegistroEnum.ProdutoFinal, "0000");

            var precoItem = new OrcamentoItemPrecoTotal(24, 07);

            var item = new OrcamentoItem(this.CdEmpresa, this.CdFilial, this.NumOrcamento, produto, 1, precoItem );

            // inserção de itens na lista

            Itens.Add(item);




        
        }

        #region validations

        public List<string> Validations;
        public bool IsValid()
        {
            Validations = new List<string>();

            if (string.IsNullOrEmpty(CdEmpresa))
                Validations.Add("Código da empresa é requirido!");

            if (string.IsNullOrEmpty(CdFilial))
                Validations.Add("Código da filial é requirido!");

            if (string.IsNullOrEmpty(Cliente.Codigo))
                Validations.Add("Código do cliente é requirido!");

            if (string.IsNullOrEmpty(Vendedor.Codigo))
                Validations.Add("Código do vendedor é requirido");

            if (string.IsNullOrEmpty(Usuario.User))
                Validations.Add("Código do usuário é requirido");

            if (NumOrcamento < 0)
                Validations.Add("O número do orçamento é requirido");

            // validações acresentadas 18/07/22 : se o cód do cliente, o cd do vendedor, cd do usuário estão inseridos e se o orçamento possúi seu número






                if (Validations.Count > 0)
                return false;
            else
                return true;
        }

        #endregion

        #region factory methods
        public static class Factory
        {

            public static Orcamento Orcamento(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoCliente cliente , OrcamentoUsuario usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, cliente, usuario, vendedor, tabelaPreco);
            }
            public static Orcamento OrcamentoRapido(string cdEmpresa, string cdFilial, int numOrcamento, OrcamentoUsuario usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, null, usuario, vendedor, tabelaPreco);
            }
        }

        #endregion
    }
}
