using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {

        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="Beneficiario">Objeto de Beneficiario</param>
        private long Incluir(Beneficiario Beneficiario)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();

            return daoBeneficiario.Incluir(Beneficiario);
        }

        /// <summary>
        /// Inclui uma lista de beneficiários
        /// </summary>
        /// <param name="Beneficiarios">Lista de beneficiários</param>
        public void Incluir(List<Beneficiario> Beneficiarios)
        {

            foreach (var item in Beneficiarios)
            {
                Incluir(item);
            }
        }

        public void Alterar(List<Beneficiario> beneficiarios, long ClienteId)
        {
            ExcluirPorCliente(ClienteId);

            Incluir(beneficiarios);
        }

        /// <summary>
        /// Consulta o Beneficiario pelo id
        /// </summary>
        /// <param name="id">id do Beneficiario</param>
        /// <returns></returns>
        public Beneficiario Consultar(long id)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            DaoCliente daoCliente = new DaoCliente();
            var beneficiario = daoBeneficiario.Consultar(id);
            beneficiario.Cliente = daoCliente.Consultar(beneficiario.Cliente.Id);
            return beneficiario;
        }

        /// <summary>
        /// Consulta o Beneficiario pelo id do cliente
        /// </summary>
        /// <param name="id">id do Cliente</param>
        /// <returns>Uma lista de beneficiários</returns>
        public List<Beneficiario> ConsultarPorClienteId(long clienteId)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
         
            
            return daoBeneficiario.ConsultarPorClienteId(clienteId);
            
        }

        /// <summary>
        /// Excluir o Beneficiario pelo id
        /// </summary>
        /// <param name="id">id do Beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            daoBeneficiario.Excluir(id);
        }

        public void ExcluirPorCliente (long id)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            daoBeneficiario.ExcluirPorCliente(id);
        }

     
    }

}
