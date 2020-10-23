using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.UTIL;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var cliente = ConstoiCliente(model);

                if (bo.VerificarExistencia(cliente.CPF))
                {
                    Response.StatusCode = 400;
                    return Json("CPF já cadastrado");
                }


                cliente.Id = bo.Incluir(cliente);

                if (model.Beneficiarios != null)
                {
                    var beneficiarios = AjusteBeneficiario(model.Beneficiarios, cliente);

                    BoBeneficiario beneficiarioBo = new BoBeneficiario();

                    beneficiarioBo.Incluir(beneficiarios);
                }


                return Json("Cadastro efetuado com sucesso");
            }
        }



        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
             BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var cliente = ConstoiCliente(model);
                bo.Alterar(cliente);
                BoBeneficiario beneficiarioBo = new BoBeneficiario();
                if (model.Beneficiarios != null)
                {
                    var beneficiarios = AjusteBeneficiario(model.Beneficiarios, cliente);
                    beneficiarioBo.Alterar(beneficiarios, cliente.Id);
                }
                else
                {
                    beneficiarioBo.ExcluirPorCliente(cliente.Id);
                }
                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            ClienteModel model = null;

            BoBeneficiario beneficiarioBo = new BoBeneficiario();

            if (cliente != null)
            {
                model = ConstroiClientModel(cliente);
                model.Beneficiarios = beneficiarioBo.ConsultarPorClienteId(cliente.Id);
            }

            return View(model);
        }



        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private ClienteModel ConstroiClientModel(Cliente cliente)
        {
            return new ClienteModel()
            {
                Id = cliente.Id,
                CEP = cliente.CEP,
                Cidade = cliente.Cidade,
                Email = cliente.Email,
                Estado = cliente.Estado,
                Logradouro = cliente.Logradouro,
                Nacionalidade = cliente.Nacionalidade,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                Telefone = cliente.Telefone,
                CPF = cliente.CPF.FormatCPF()
            };
        }

        private Cliente ConstoiCliente(ClienteModel model)
        {
            return new Cliente()
            {
                Id = model.Id,
                CEP = model.CEP,
                Cidade = model.Cidade,
                Email = model.Email,
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone,
                CPF = model.CPF.OnlyNumerics(),

            };
        }

        private List<Beneficiario> AjusteBeneficiario(List<Beneficiario> beneficiarios, Cliente cliente)
        {
            foreach (var beneficiario in beneficiarios)
            {
                beneficiario.Cliente = cliente;
            }
            return beneficiarios;
        }
    }
}