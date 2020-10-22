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
        public long Incluir(Beneficiario Beneficiario)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();

            return daoBeneficiario.Incluir(Beneficiario);
        }


        public void Incluir(List<Beneficiario> Beneficiarios)
        {

            foreach (var item in Beneficiarios)
            {
                Incluir(item);
            }
        }


        /// <summary>
        /// Altera um Beneficiario
        /// </summary>
        /// <param name="Beneficiario">Objeto de Beneficiario</param>
        public void Alterar(Beneficiario Beneficiario)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            daoBeneficiario.Alterar(Beneficiario);
        }

        public void Alterar(List<Beneficiario> Beneficiarios)
        {
            foreach (var item in Beneficiarios)
            {
                if (item.Id > 0)
                    Alterar(item);

                else
                    Incluir(item);
            }
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

        /// <summary>
        /// Lista os Beneficiarios
        /// </summary>
        public List<Beneficiario> Listar()
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            return daoBeneficiario.Listar();
        }

        /// <summary>
        /// Lista os Beneficiarios
        /// </summary>
        public List<Beneficiario> Pesquisa(int iniciarEm, int quantidade, bool crescente, out int qtd)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            return daoBeneficiario.Pesquisa(iniciarEm, quantidade, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long idCliente)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            return daoBeneficiario.VerificarExistencia(CPF, idCliente);
        }
    }

}
